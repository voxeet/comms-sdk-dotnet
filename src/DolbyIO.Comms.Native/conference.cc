#include "conference.h"

namespace dolbyio::comms::native {
extern "C" {

 EXPORT_API void SetOnConferenceStatusUpdatedHandler(on_conference_status_updated::type handler) {
    handle<on_conference_status_updated>(sdk->conference(), handler,
      [handler](const on_conference_status_updated::event& e) {
        handler(to_underlying(e.status), strdup(e.id.c_str()));
      }
    );
  }

  EXPORT_API void SetOnParticipantAddedHandler(on_participant_added::type handler) {
    handle<on_participant_added>(sdk->conference(), handler,
      [handler](const on_participant_added::event& e) {
        handler(to_c<dolbyio::comms::native::participant>(e.participant));      
      }
    );
  }

  EXPORT_API void SetOnParticipantUpdatedHandler(on_participant_updated::type handler) {
    handle<on_participant_updated>(sdk->conference(), handler,
      [handler](const on_participant_updated::event& e) {
        handler(to_c<dolbyio::comms::native::participant>(e.participant));      
      }
    );
  }

  EXPORT_API void SetOnActiveSpeakerChangeHandler(on_active_speaker_change::type handler) {
    handle<on_active_speaker_change>(sdk->conference(), handler,
      [handler](const on_active_speaker_change::event& e) {
        char* conf_id = strdup(e.conference_id);
        std::vector<char*> speakers(e.active_speakers.size());

        for (int i = 0; i < e.active_speakers.size(); i++) {
          speakers[i] = strdup(e.active_speakers.at(i));
        }

        handler(conf_id, e.active_speakers.size(), &speakers[0]);    
      }
    );
  }

  EXPORT_API void SetOnConferenceMessageReceivedHandler(on_conference_message_received::type handler) {
    handle<on_conference_message_received>(sdk->conference(), handler,
      [handler](const on_conference_message_received::event& e) {
          auto info = to_c<dolbyio::comms::native::participant_info>(e.sender_info);
          handler(strdup(e.conference_id), strdup(e.user_id), info, strdup(e.message));
      }
    );
  }

  EXPORT_API void SetOnConferenceInvitationReceivedHandler(on_conference_invitation_received::type handler) {
    handle<on_conference_invitation_received>(sdk->conference(), handler,
      [handler](const on_conference_invitation_received::event& e) {
        handler(
          strdup(e.conference_id), 
          strdup(e.conference_alias), 
          to_c<dolbyio::comms::native::participant_info>(e.sender_info)
        );
      }
    );
  }

  EXPORT_API void SetOnDvcErrorExceptionHandler(on_dvc_error_exception::type handler) {
    handle<on_dvc_error_exception>(sdk->conference(), handler,
      [handler](const on_dvc_error_exception::event& e) {
        handler(strdup(e.what()));
      }
    );
  }

  EXPORT_API void SetOnPeerConnectionFailedExceptionHandler(on_peer_connection_failed_exception::type handler) {
    handle<on_peer_connection_failed_exception>(sdk->conference(), handler,
      [handler](const on_peer_connection_failed_exception::event& e) {
        handler(strdup(e.what()));
      }
    );
  }
  
  EXPORT_API int Create(dolbyio::comms::native::conference_options* opts, dolbyio::comms::native::conference* infos) {
    return call { [&]() {
      auto options = to_cpp<dolbyio::comms::services::conference::conference_options>(opts);
      auto result = wait(sdk->conference().create(options));
      no_alloc_to_c(infos, result);
    }}.result();
  }

  EXPORT_API int Join(dolbyio::comms::native::conference* ifs, dolbyio::comms::native::join_options* opts, dolbyio::comms::native::conference* res) {
    return call { [&]() {
      auto options = to_cpp<dolbyio::comms::services::conference::join_options>(opts);
      auto infos = to_cpp<dolbyio::comms::conference_info>(ifs);
      auto result = wait(sdk->conference().join(infos, options));
      no_alloc_to_c(res, result);
    }}.result();
  }

  EXPORT_API int Demo(bool spatial_audio, dolbyio::comms::native::conference* conf) {
    return call { [&]() {
      auto result = wait(sdk->conference().demo(spatial_audio));
      no_alloc_to_c(conf, result);
    }}.result();
  }

  EXPORT_API int Listen(dolbyio::comms::native::conference* ifs, dolbyio::comms::native::listen_options* opts, dolbyio::comms::native::conference* res) {
    return call { [&]() {
      auto options = to_cpp<dolbyio::comms::services::conference::listen_options>(opts);
      auto infos = to_cpp<dolbyio::comms::conference_info>(ifs);
      auto result = wait(sdk->conference().listen(infos, options));
      no_alloc_to_c(res, result);
    }}.result();
  }

  EXPORT_API int GetCurrentConference(dolbyio::comms::native::conference* res) {
    return call { [&]() {
      auto infos = wait(sdk->conference().get_current_conference());
      no_alloc_to_c(res, infos);
    }}.result();
  }

  EXPORT_API int SetSpatialEnvironment(float scale_x, float scale_y, float scale_z, 
                                      float forward_x, float forward_y, float forward_z,
                                      float up_x, float up_y, float up_z,
                                      float right_x, float right_y, float right_z) {
    return call { [&]() {
      dolbyio::comms::spatial_audio_batch_update conf;

      conf.set_spatial_environment({scale_x, scale_y, scale_z},
                                   {forward_x, forward_y, forward_z},
                                   {up_x, up_y, up_z},
                                   {right_x, right_y, right_z}
                                  );

      wait(sdk->conference().update_spatial_audio_configuration(std::move(conf)));
    }}.result();
  }

  EXPORT_API int SetSpatialDirection(float x, float y, float z) {
    return call { [&]() {
      dolbyio::comms::spatial_audio_batch_update conf;
      conf.set_spatial_direction({x, y, z});

      wait(sdk->conference().update_spatial_audio_configuration(std::move(conf)));
    }}.result();
  }

  EXPORT_API int SetSpatialPosition(const char* user_id, float x, float y, float z) {
    return call { [&]() {
      dolbyio::comms::spatial_audio_batch_update conf;
      conf.set_spatial_position(user_id, {x, y, z});

      wait(sdk->conference().update_spatial_audio_configuration(std::move(conf)));
    }}.result();
  }

  EXPORT_API int SendMessage(char* message) {
    return call { [&]() {
      wait(sdk->conference().send(std::string(message)));
    }}.result();
  }

  EXPORT_API int Leave() {
    return call { [&]() {
      wait(sdk->conference().leave());
    }}.result();
  }

  EXPORT_API int DeclineInvitation(char* conference_id) {
    return call { [&]() {
      wait(sdk->conference().decline_invitation(std::string(conference_id)));
    }}.result();
  }

} // extern "C"
} // namespace dolbyio::comms::native