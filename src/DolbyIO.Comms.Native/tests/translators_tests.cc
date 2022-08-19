#include "../sdk.h"
#include "../session.h"
#include "../conference.h"
#include "../media_device.h"

namespace dolbyio::comms::native::tests {
extern "C" {

  EXPORT_API void UserInfoTest(dolbyio::comms::native::user_info* src, dolbyio::comms::native::user_info** result) {
    auto tmp = to_cpp<dolbyio::comms::services::session::user_info>(src);
    tmp.participant_id = "anonymous";
    (*result) = to_c<dolbyio::comms::native::user_info>(tmp);
  }

  EXPORT_API void ConferenceOptionsTest(dolbyio::comms::native::conference_options* src, dolbyio::comms::native::conference_options** result) {
    auto tmp = to_cpp<dolbyio::comms::services::conference::conference_options>(src);
    (*result) = to_c<dolbyio::comms::native::conference_options>(tmp);
  }

  EXPORT_API void ConferenceInfosTest(dolbyio::comms::native::conference_infos* src, dolbyio::comms::native::conference_infos** result) {
    auto infos = to_cpp<dolbyio::comms::conference_info>(src);
    
    infos.is_new = true;
    infos.permissions.emplace_back(dolbyio::comms::conference_access_permissions::invite);
    infos.permissions.emplace_back(dolbyio::comms::conference_access_permissions::join);
    infos.spatial_audio_style = dolbyio::comms::spatial_audio_style::individual;
    infos.status = dolbyio::comms::conference_status::joining;
    
    (*result) = to_c<dolbyio::comms::native::conference_infos>(infos);
  }

  EXPORT_API void JoinOptionsTest(join_options* src, join_options** result) {
    auto options = to_cpp<dolbyio::comms::services::conference::join_options>(src);
    (*result) = to_c<dolbyio::comms::native::join_options>(options); 
  }

  EXPORT_API void ListenOptionsTest(listen_options* src, listen_options** result) {
    auto options = to_cpp<dolbyio::comms::services::conference::listen_options>(src);
    (*result) = to_c<dolbyio::comms::native::listen_options>(options);
  }

  EXPORT_API void ParticipantTest(participant** result) {
    struct dolbyio::comms::participant_info::info info { "Anonymous", "externalId", "http://avatar.url" };

    dolbyio::comms::participant_info participant (
      "userId",
      dolbyio::comms::participant_type::user,
      dolbyio::comms::participant_status::connecting,
      info,
      true,
      true
    );

    (*result) = to_c<dolbyio::comms::native::participant>(participant);
  }

  EXPORT_API void AudioDeviceTest(device** result) {
    dvc_device dev("UID", "dummy device", dvc_device::direction::output, dvc_device::platform::macos, "ID");
    (*result) = to_c<dolbyio::comms::native::device>(dev);
  }

}
} // namespace dolbyio::comms::native::tests
