#ifndef _VIDEO_SINK_H_
#define _VIDEO_SINK_H_

#include "sdk.h"

#include <dolbyio/comms/media_engine/video_frame_macos.h>
#include <dolbyio/comms/media_engine/video_utils.h>

#if defined(__APPLE__)
  #import <CoreVideo/CoreVideo.h>
#endif

namespace dolbyio::comms::native {

  enum pixel_format {
    ARGB8888 = 0x00,
  };

  struct native_video_frame {
    int width;
    int height;
    uint8_t* buffer;
  };

  class video_sink : public dolbyio::comms::video_sink {
  
  public:
    using delegate_type = void (*)(char*, char*, native_video_frame);

    video_sink(delegate_type delegate) {
      delegate_ = delegate;
    }

    void handle_frame(const std::string &stream_id, const std::string &track_id, std::unique_ptr<video_frame> frame) {
      native_video_frame result;

#if defined(__APPLE__)
      video_frame_macos *mac_frame = frame->get_native_frame();
      CVPixelBufferRef buffer = mac_frame->get_buffer();
      int res = CVPixelBufferLockBaseAddress(buffer, 0);

      size_t width = CVPixelBufferGetWidth(buffer);
      size_t height = CVPixelBufferGetHeight(buffer);

      uint8_t *y_addr = (uint8_t*)CVPixelBufferGetBaseAddressOfPlane(buffer, 0);
      int y_stride = CVPixelBufferGetBytesPerRowOfPlane(buffer, 0);

      uint8_t *uv_addr = (uint8_t*)CVPixelBufferGetBaseAddressOfPlane(buffer, 1);
      int uv_stide = CVPixelBufferGetBytesPerRowOfPlane(buffer, 1) / 2;

      result.buffer = (uint8_t *)malloc(sizeof(uint8_t) * width * height * 4);

      dolbyio::comms::video_utils::format_converter::nv12_to_argb(
        y_addr,
        y_stride,
        uv_addr,
        uv_stide,
        result.buffer,
        width * 4,
        width,
        height
      );
      
      result.width = width;
      result.height = height;

      CVPixelBufferUnlockBaseAddress(buffer, 0);
#else

#endif

      delegate_(strdup(stream_id), strdup(track_id), result);
    }

  private:
    delegate_type delegate_;
  };
}

#endif // _VIDEO_SINK_H_