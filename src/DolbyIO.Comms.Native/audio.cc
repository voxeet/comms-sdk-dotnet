#include "sdk.h"

namespace dolbyio::comms::native {
extern "C" {

  EXPORT_API int Mute(bool muted) {
    return call { [&]() {
      wait(sdk->conference().mute(muted));
    }}.result();
  }

  EXPORT_API int RemoteMute(bool muted, char* participant_id) {
    return call { [&]() {
      wait(sdk->conference().remote_mute(muted, std::string(participant_id)));
    }}.result();
  }

  EXPORT_API int StartAudio() {
    return call { [&]() {
      wait(sdk->audio().local().start());
    }}.result();
  }

  EXPORT_API int StopAudio() {
    return call { [&]() {
      wait(sdk->audio().local().stop());
    }}.result();
  }

    EXPORT_API int StartRemoteAudio(char* participant_id) {
    return call { [&]() {
      wait(sdk->audio().remote().start(std::string(participant_id)));
    }}.result();
  }

  EXPORT_API int StopRemoteAudio(char* participant_id) {
    return call { [&]() {
      wait(sdk->audio().remote().stop(std::string(participant_id)));
    }}.result();
  }

} // extern "C"
} // namespace dolbyio::comms::native