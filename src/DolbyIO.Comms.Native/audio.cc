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
      wait(sdk->conference().start_audio());
    }}.result();
  }

  EXPORT_API int StopAudio() {
    return call { [&]() {
      wait(sdk->conference().stop_audio());
    }}.result();
  }

    EXPORT_API int StartRemoteAudio(char* participant_id) {
    return call { [&]() {
      wait(sdk->conference().start_remote_audio(std::string(participant_id)));
    }}.result();
  }

  EXPORT_API int StopRemoteAudio(char* participant_id) {
    return call { [&]() {
      wait(sdk->conference().stop_remote_audio(std::string(participant_id)));
    }}.result();
  }

} // extern "C"
} // namespace dolbyio::comms::native