#include <iostream>

#include "sdk.h"
#include "handlers.h"

namespace dolbyio::comms::native {

std::map<std::string, std::map<std::int32_t, dolbyio::comms::event_handler_id>> handlers_map;

dolbyio::comms::sdk* sdk = nullptr;
std::string error = "";

extern "C" {

  EXPORT_API void AddOnSignalingChannelExceptionHandler(std::int32_t hash, on_signaling_channel_exception::type handler) {
    handle<on_signaling_channel_exception>(*sdk, hash, handler,
      [handler](const on_signaling_channel_exception::event& e) {
        handler(strdup(e.what()));
      }
    );
  }

  EXPORT_API int RemoveOnSignalingChannelExceptionHandler(std::int32_t hash, on_signaling_channel_exception::type handler) {
    return call { [&]() {
      disconnect_handler<on_signaling_channel_exception>(hash, handler);
    }}.result();
  }

  EXPORT_API void AddOnInvalidTokenExceptionHandler(std::int32_t hash, on_invalid_token_exception::type handler) {
    handle<on_invalid_token_exception>(*sdk, hash, handler,
      [handler](const on_invalid_token_exception::event& e) {
        handler(strdup(e.reason()), strdup(e.description()));
      }
    );
  }
  
  EXPORT_API int RemoveOnInvalidTokenExceptionHandler(std::int32_t hash, on_invalid_token_exception::type handler) {
    return call { [&]() {
      disconnect_handler<on_invalid_token_exception>(hash, handler);
    }}.result();
  }

  EXPORT_API int SetLogLevel(uint32_t log_level) {
    return call { [&]() {
      dolbyio::comms::sdk::log_settings settings;
      
      settings.media_log_level = (dolbyio::comms::log_level)log_level;
      settings.sdk_log_level = (dolbyio::comms::log_level)log_level;

      sdk::set_log_settings(settings);
    }}.result();
  }

  EXPORT_API int Init(const char* token, refresh_delegate_type callback) {
    return call { [&]() {
      sdk = dolbyio::comms::sdk::create(
        token,
        [callback](std::unique_ptr<dolbyio::comms::refresh_token>&& refresh_token) {
          char* token = callback();
          (*refresh_token)(std::string(token));
        }
      ).release();
    }}.result();
  }

  EXPORT_API int RegisterComponentVersion(const char* name, const char* version) {
    return call { [&]() {
      wait(sdk->register_component_version(std::string(name), std::string(version)));
    }}.result();
  }

  EXPORT_API int Release() {
    return call { [&]() {
      for (const auto& [key, value] : handlers_map) {
        for (const auto& [key2, value2] : value) {
          wait(value2->disconnect());
        }
      }

      handlers_map.clear();

      // Releasing sdk
      if (sdk) {
        delete sdk;
        sdk = nullptr;
      }
    }}.result();
  }

  EXPORT_API char* GetLastErrorMsg() {
    return strdup(error);
  }

} // extern "C"
} // dolbyio::comms::native
