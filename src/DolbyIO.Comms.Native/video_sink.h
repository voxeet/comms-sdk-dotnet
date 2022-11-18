#ifndef _VIDEO_SINK_H_
#define _VIDEO_SINK_H_

#include "sdk.h"

#include <dolbyio/comms/media_engine/video_frame_macos.h>

namespace dolbyio::comms::native {

  enum pixel_format {
    ARGB8888 = 0x00,
  };

  class native_video_frame {
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
      video_frame* f = frame.release();
      native_video_frame result;

#if defined(__APPLE__)
      video_frame_macos *mac_frame = f->get_native_frame();
#else

#endif

      delegate_(strdup(stream_id), strdup(track_id), result);

      delete f;
    }

  private:
    delegate_type delegate_;
  };
}

#endif // _VIDEO_SINK_H_