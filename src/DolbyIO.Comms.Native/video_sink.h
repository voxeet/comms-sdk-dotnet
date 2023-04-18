#ifndef _VIDEO_SINK_H_
#define _VIDEO_SINK_H_

#include <cmath>

#include "sdk.h"

#include <dolbyio/comms/media_engine/video_frame_macos.h>
#include <dolbyio/comms/media_engine/video_utils.h>

#include "yuv_to_rgba.h"

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
    using delegate_type = void (*)(int, int, uint8_t*);

    video_sink(delegate_type delegate) {
      delegate_ = delegate;
    }

    void handle_frame(std::unique_ptr<video_frame> frame) {
      int bytes_per_pixel = 4;
      size_t width, height = 0;
      uint8_t* resbuffer = nullptr;

#if defined(__APPLE__)
      video_frame_macos *mac_frame = frame->get_native_frame();
      if (mac_frame) {
        CVPixelBufferRef buffer = mac_frame->get_buffer();
        int res = CVPixelBufferLockBaseAddress(buffer, kCVPixelBufferLock_ReadOnly);

        //Sanity check for ensuring we are capturing NV12 from camera
        auto format_type = CVPixelBufferGetPixelFormatType(buffer);
        if (format_type != kCVPixelFormatType_420YpCbCr8BiPlanarVideoRange &&
            format_type != kCVPixelFormatType_420YpCbCr8BiPlanarFullRange)
          return;

        width = CVPixelBufferGetWidth(buffer);
        height = CVPixelBufferGetHeight(buffer);

        uint8_t *y_buffer = (uint8_t*)CVPixelBufferGetBaseAddressOfPlane(buffer, 0);
        int y_stride = CVPixelBufferGetBytesPerRowOfPlane(buffer, 0);

        uint8_t *uv_buffer = (uint8_t*)CVPixelBufferGetBaseAddressOfPlane(buffer, 1);
        int uv_stride = CVPixelBufferGetBytesPerRowOfPlane(buffer, 1);

        resbuffer = (uint8_t *)malloc(sizeof(uint8_t) * width * height * bytes_per_pixel);

        nv12_rgb24_std(
          frame->width(),
          frame->height(),
          y_buffer,
          uv_buffer,  
          y_stride,
          uv_stride,
          resbuffer,
          width * bytes_per_pixel,
          ycbcr_type::ycbcr_jpeg);

        CVPixelBufferUnlockBaseAddress(buffer, kCVPixelBufferLock_ReadOnly);
      } else {
#endif

        auto frame_i420 = frame->get_i420_frame();

        width = frame->width();
        height = frame->height();

        resbuffer = (uint8_t *)malloc(sizeof(uint8_t) * width * height * bytes_per_pixel);

        const uint8_t* y_addr = frame_i420->get_y();
        const uint8_t* u_addr = frame_i420->get_u();
        const uint8_t* v_addr = frame_i420->get_v();

        int y_stride = frame_i420->stride_y();
        int u_stride = frame_i420->stride_u();
        int v_stride = frame_i420->stride_v();

        yuv420_rgb24_std(
          width,
          height,
          y_addr,
          u_addr,
          v_addr,
          y_stride,
          u_stride,
          resbuffer,
          width * bytes_per_pixel,
          ycbcr_type::ycbcr_jpeg
        );

#if defined(__APPLE__)
      }
#endif

      delegate_(width, height, resbuffer);
    }

  private:
    delegate_type delegate_;
  };

} // namespace dolbyio::comms::native

#endif // _VIDEO_SINK_H_