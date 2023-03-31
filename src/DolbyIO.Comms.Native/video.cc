#include "sdk.h"
#include "media_device.h"
#include "video_frame_handler.h"
#include "conference.h"

namespace dolbyio::comms::native {
extern "C" {

  EXPORT_API int SetVideoSink(video_track track, video_sink* sink) {
    return call { [&]() {
      dolbyio::comms::video_track cpp_track;
      no_alloc_to_cpp(cpp_track, &track);

      auto shared = std::shared_ptr<dolbyio::comms::native::video_sink>(sink, null_deleter{});
      wait(sdk->video().remote().set_video_sink(cpp_track, shared));
    }}.result();
  }

  EXPORT_API int StartVideo(video_device device, dolbyio::comms::native::video_frame_handler* handler) {
    return call { [&]() {
      camera_device input;
      no_alloc_to_cpp(input, &device);
      
      auto shared = std::shared_ptr<dolbyio::comms::native::video_frame_handler>(handler, null_deleter{});
      wait(sdk->video().local().start(input, shared));
    }}.result();
  }

  EXPORT_API int StopVideo() {
    return call { [&]() {
      wait(sdk->video().local().stop());
    }}.result();
  }

} // extern "C"
} // namespace dolbyio::comms::native