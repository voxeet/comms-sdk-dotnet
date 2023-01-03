/*
 * Copyright (c) 2020 Thomas Gourgues <thomas.gourgues@gmail.com>
 * All rights reserved.
 *
 * Based on https://github.com/descampsa/yuv2rgb
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 * 3. Neither the name of yuv_to_rgba nor the names of its
 *    contributors may be used to endorse or promote products derived from
 *    this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 */

#include <cstdint>

enum class ycbcr_type : int {
  ycbcr_jpeg = 0,
  ycbcr_601 = 1,
  ycbcr_709 = 2,
  ycbcr_709_full = 3,
  ycbcr_2020 = 4,
  ycbcr_2020_full = 5
};

struct yuv_params {
  uint8_t cb_factor;   // [(255*CbNorm)/CbRange]
  uint8_t cr_factor;   // [(255*CrNorm)/CrRange]
  uint8_t g_cb_factor; // [Bf/Gf*(255*CbNorm)/CbRange]
  uint8_t g_cr_factor; // [Rf/Gf*(255*CrNorm)/CrRange]
  uint8_t y_factor;    // [(YMax-YMin)/255]
  uint8_t y_offset;    // YMin
};

constexpr uint8_t clamp(uint16_t value) {
  return value < 0 ? 0 : (value > 255 ? 255 : value);
}

constexpr uint8_t fixed_point_value(float value, int precision) {
  return ((value) * (1 << precision)) + 0.5;
}

constexpr yuv_params make_yuv_params(float rf, float bf, float ymin, float ymax, float range) {
  return yuv_params {
    fixed_point_value(255.0 * (2.0 * (1 - bf)) / range, 6),
    fixed_point_value(255.0 * (2.0 * (1 - rf)) / range, 6),
    fixed_point_value(bf / (1.0 - bf - rf) * 255.0 * (2.0 * (1 - bf)) / range, 7),
    fixed_point_value(rf / (1.0 - bf - rf) * 255.0 * (2.0 * (1 - rf)) / range, 7),
    fixed_point_value(255.0 / (ymax - ymin), 7),
    (uint8_t)ymin
  };
}

static constexpr yuv_params yuv2rb[6] = {
  // ITU-T T.871 (JPEG)
	make_yuv_params(0.299, 0.114, 0.0, 255.0, 255.0),
	// ITU-R BT.601-7
	make_yuv_params(0.299, 0.114, 16.0, 235.0, 224.0),
	// ITU-R BT.709-6
	make_yuv_params(0.2126, 0.0722, 16.0, 235.0, 224.0),
  make_yuv_params(0.2126, 0.0722, 0.0, 255.0, 255.0),
  make_yuv_params(0.2627, 0.0593, 16.0, 235.0, 224.0),
  make_yuv_params(0.2627, 0.0593, 0.0, 255.0, 255.0),
};

void yuv420_rgb24_std(
	uint32_t width, uint32_t height, 
	const uint8_t* y_addr, const uint8_t *u_addr, const uint8_t *v_addr, uint32_t y_stride, uint32_t uv_stride, 
	uint8_t *rgba_addr, uint32_t rgb_stride, 
	ycbcr_type yuv_type
) {
	const yuv_params* const param = &(yuv2rb[(int)yuv_type]);
	uint32_t x, y;

	for(y=0; y<(height-1); y+=2) {
		const uint8_t* y_ptr1 = y_addr + y * y_stride;
		const uint8_t* y_ptr2 = y_addr + (y + 1) * y_stride;
		const uint8_t* u_ptr = u_addr + (y / 2) * uv_stride;
		const uint8_t* v_ptr = v_addr + (y / 2) * uv_stride;
		
		uint8_t* rgb_ptr1 = rgba_addr + y * rgb_stride;
		uint8_t* rgb_ptr2 = rgba_addr + (y + 1) * rgb_stride;
		
		for(x=0; x<(width - 1); x+=2) {
			int8_t u_tmp, v_tmp;
			u_tmp = u_ptr[0] - 128;
			v_tmp = v_ptr[0] - 128;
			
			//compute Cb Cr color offsets, common to four pixels
			int16_t b_cb_offset, r_cr_offset, g_cbcr_offset;
			b_cb_offset = (param->cb_factor*u_tmp)>>6;
			r_cr_offset = (param->cr_factor*v_tmp)>>6;
			g_cbcr_offset = (param->g_cb_factor * u_tmp + param->g_cr_factor * v_tmp) >> 7;
			
			int16_t y_tmp;
			y_tmp = (param->y_factor * (y_ptr1[0] - param->y_offset)) >> 7;
      rgb_ptr1[0] = 0xFF;
			rgb_ptr1[1] = clamp(y_tmp + r_cr_offset);
			rgb_ptr1[2] = clamp(y_tmp - g_cbcr_offset);
			rgb_ptr1[3] = clamp(y_tmp + b_cb_offset);
			
			y_tmp = (param->y_factor * (y_ptr1[1] - param->y_offset)) >> 7;
      rgb_ptr1[4] = 0xFF;
			rgb_ptr1[5] = clamp(y_tmp + r_cr_offset);
			rgb_ptr1[6] = clamp(y_tmp - g_cbcr_offset);
			rgb_ptr1[7] = clamp(y_tmp + b_cb_offset);
			
			y_tmp = (param->y_factor * (y_ptr2[0] - param->y_offset)) >> 7;
      rgb_ptr2[0] = 0xFF;
			rgb_ptr2[1] = clamp(y_tmp + r_cr_offset);
			rgb_ptr2[2] = clamp(y_tmp - g_cbcr_offset);
			rgb_ptr2[3] = clamp(y_tmp + b_cb_offset);
			
			y_tmp = (param->y_factor * (y_ptr2[1] - param->y_offset)) >> 7;
      rgb_ptr2[4] = 0xFF;
			rgb_ptr2[5] = clamp(y_tmp + r_cr_offset);
			rgb_ptr2[6] = clamp(y_tmp - g_cbcr_offset);
			rgb_ptr2[7] = clamp(y_tmp + b_cb_offset);
			
			rgb_ptr1 += 8;
			rgb_ptr2 += 8;
			y_ptr1 += 2;
			y_ptr2 += 2;
			u_ptr += 1;
			v_ptr += 1;
		}
	}
}
