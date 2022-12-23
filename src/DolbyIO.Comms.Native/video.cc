#include "sdk.h"
#include "media_device.h"
#include "video_frame_handler.h"

namespace dolbyio::comms::native {
extern "C" {

  EXPORT_API int SetVideoSink(video_sink* sink) {
    return call { [&]() {
      wait(sdk->video().remote().set_video_sink(sink));
    }}.result();
  }

  EXPORT_API int StartVideo(video_device device, dolbyio::comms::native::video_frame_handler* handler) {
    return call { [&]() {
      camera_device input;
      no_alloc_to_cpp(input, &device);

      wait(sdk->video().local().start(input, handler));
    }}.result();
  }

  EXPORT_API int StopVideo() {
    return call { [&]() {
      wait(sdk->video().local().stop());
    }}.result();
  }

} // extern "C"
} // namespace dolbyio::comms::native