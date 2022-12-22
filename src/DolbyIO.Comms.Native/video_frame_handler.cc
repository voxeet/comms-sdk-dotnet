#include "sdk.h"
#include "video_frame_handler.h"

namespace dolbyio::comms::native {
extern "C" {
  
  EXPORT_API video_frame_handler* CreateVideoFrameHandler() {
    return new video_frame_handler();
  }

  EXPORT_API bool DeleteVideoFrameHandler(video_frame_handler* p) {
    if (p) {
      delete p;
      return true;
    }

    return false;
  }

  EXPORT_API int SetVideoFrameHandlerSink(video_frame_handler* p, video_sink* sink) {
    if (p) {
      p->sink(sink);
      return  call<>::result_success;
    }

    return call<>::result_error;
  }
  
} // extern "C"
} // namespace dolbyio::comms::native
