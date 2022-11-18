#include "sdk.h"
#include <dispatch/dispatch.h>

namespace dolbyio::comms::native {
extern "C" {

  EXPORT_API int SetVideoSink(video_sink* sink) {
    return call { [&]() {
      wait(sdk->video().remote().set_video_sink(sink));
    }}.result();
  }

  EXPORT_API int StartVideo() {
    return call { [&]() {
      wait(sdk->video().local().start());
    }}.result();
  }

  EXPORT_API int StopVideo() {
    return call { [&]() {
      wait(sdk->video().local().stop());
    }}.result();
  }

} // extern "C"
} // namespace dolbyio::comms::native