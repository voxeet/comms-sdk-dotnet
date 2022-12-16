#include "video_sink.h"
#include "sdk.h"

namespace dolbyio::comms::native {
extern "C" {

  EXPORT_API video_sink* CreateVideoSink(video_sink::delegate_type delegate) {
    return new video_sink(delegate);
  }

  EXPORT_API bool DeleteVideoSink(video_sink* sink) {
    if (sink != nullptr) {
      delete sink;
      return true;
    }

    return false;
  }

  EXPORT_API bool DeleteVideoFrameBuffer(uint8_t* buffer) {
    if (buffer != nullptr) {
      free(buffer);
      return true;
    }
    
    return false;
  }

} // extern "C"
} // namespace dolbyio::comms::native
