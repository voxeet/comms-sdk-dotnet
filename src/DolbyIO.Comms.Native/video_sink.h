#ifndef _VIDEO_SINK_H_
#define _VIDEO_SINK_H_

#include <cmath>

#include "sdk.h"

#include <dolbyio/comms/media_engine/video_frame_macos.h>
#include <dolbyio/comms/media_engine/video_utils.h>

#if defined(__APPLE__)
  #import <CoreVideo/CoreVideo.h>
#endif

#define clamp(a) (a > 255 ? 255 : (a < 0 ? 0 : a))

namespace dolbyio::comms::native {

  enum pixel_format {
    ARGB8888 = 0x00,
  };

  class video_sink : public dolbyio::comms::video_sink {
  
  public:
    using delegate_type = void (*)(char*, char*, int, int, uint8_t*);

    video_sink(delegate_type delegate) {
      delegate_ = delegate;
    }

    void handle_frame(const std::string &stream_id, const std::string &track_id, std::unique_ptr<video_frame> frame) {
      int bytes_per_pixel = 4;

#if defined(__APPLE__)
      video_frame_macos *mac_frame = frame->get_native_frame();
      CVPixelBufferRef buffer = mac_frame->get_buffer();
      int res = CVPixelBufferLockBaseAddress(buffer, kCVPixelBufferLock_ReadOnly);

         // Sanity check for ensuring we are capturing NV12 from camera
      auto format_type = CVPixelBufferGetPixelFormatType(buffer);
      if (format_type != kCVPixelFormatType_420YpCbCr8BiPlanarVideoRange &&
          format_type != kCVPixelFormatType_420YpCbCr8BiPlanarFullRange)
        return;

      size_t width = CVPixelBufferGetWidth(buffer);
      size_t height = CVPixelBufferGetHeight(buffer);

      uint8_t *y_buffer = (uint8_t*)CVPixelBufferGetBaseAddressOfPlane(buffer, 0);
      int y_stride = CVPixelBufferGetBytesPerRowOfPlane(buffer, 0);

      uint8_t *uv_buffer = (uint8_t*)CVPixelBufferGetBaseAddressOfPlane(buffer, 1);
      int uv_stride = CVPixelBufferGetBytesPerRowOfPlane(buffer, 1);


      uint8_t *resbuffer = (uint8_t *)malloc(sizeof(uint8_t) * width * height * bytes_per_pixel);

      for(int y = 0; y < height; y++) {
        uint8_t *rgb_line = &resbuffer[y * width * bytes_per_pixel];
        uint8_t *y_addr = &y_buffer[y * y_stride];
        uint8_t *uv_addr = &uv_buffer[(y >> 1) * uv_stride];

        for(int x = 0; x < width; x++) {
          int16_t yy = y_addr[x];
          int16_t cb = uv_addr[x & ~1] - 128;
          int16_t cr = uv_addr[x | 1] - 128;

          uint8_t *rgb_output = &rgb_line[x * bytes_per_pixel];

          // BT.601 limited.
          int16_t r =  (yy - 16) * 1.164 + cr *  1.596 ;
          int16_t g = (yy - 16) * 1.164 + cb * -0.391 + cr * -0.813 ;
          int16_t b = (yy - 16) * 1.164 + cb *  2.018;

          rgb_output[0] = 0xff;
          rgb_output[1] = clamp(r);
          rgb_output[2] = clamp(g);
          rgb_output[3] = clamp(b);
        }
      }

      CVPixelBufferUnlockBaseAddress(buffer, kCVPixelBufferLock_ReadOnly);
#else
      auto frame_i420 = frame->get_i420_frame();

      size_t width = frame->width();
      size_t height = frame->height();

      uint8_t* resbuffer = (uint8_t *)malloc(sizeof(uint8_t) * width * height * bytes_per_pixel);

      const uint8_t* y_addr = frame_i420->get_y();
      const uint8_t* u_addr = frame_i420->get_u();
      const uint8_t* v_addr = frame_i420->get_v();

      int y_stride = frame_i420->stride_y();
      int u_stride = frame_i420->stride_u();
      int v_stride = frame_i420->stride_v();

      int16_t yy, cb, cr = 0;
      uint16_t r, g, b = 0;

      for (int j = 0; j < height; j++) {
        for (int i = 0; i < width; i++) {
          yy = y_addr[j * y_stride + i];
          cb = u_addr[(j / 2) * u_stride + (i / 2)] - 128;
          cr = v_addr[(j / 2) * v_stride + (i / 2)] - 128;

          r = (yy) * 1.164 + cr *  1.596 ;
          g = (yy) * 1.164 + cb * -0.391 + cr * -0.813 ;
          b = (yy) * 1.164 + cb *  2.018;

          int index = (j * width * bytes_per_pixel) + (i * bytes_per_pixel);
          resbuffer[index + 0] = 0xff;
          resbuffer[index + 1] = clamp(r);
          resbuffer[index + 2] = clamp(g);
          resbuffer[index + 3] = clamp(b);
        }
      }
#endif
      delegate_(strdup(stream_id), strdup(track_id), width, height, resbuffer);
    }

  private:
    delegate_type delegate_;
  };

} // namespace dolbyio::comms::native

#endif // _VIDEO_SINK_H_