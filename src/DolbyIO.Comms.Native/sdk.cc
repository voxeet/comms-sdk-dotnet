#include <iostream>

#include "sdk.h"
#include "handlers.h"

namespace dolbyio::comms::native {

std::map<std::string, dolbyio::comms::event_handler_id> handlers_map;

dolbyio::comms::sdk* sdk = nullptr;
std::string error = "";

extern "C" {

 EXPORT_API void SetOnSignalingChannelExceptionHandler(on_signaling_channel_exception::type handler) {
    handle<on_signaling_channel_exception>(*sdk, handler,
      [handler](const on_signaling_channel_exception::event& e) {
        handler(strdup(e.what()));
      }
    );
  }

  EXPORT_API void SetOnInvalidTokenExceptionHandler(on_invalid_token_exception::type handler) {
    handle<on_invalid_token_exception>(*sdk, handler,
      [handler](const on_invalid_token_exception::event& e) {
        handler(strdup(e.reason()), strdup(e.description()));
      }
    );
  }

  EXPORT_API int Init(const char* token) {
    return call { [&]() {
      sdk = dolbyio::comms::sdk::create(
        token,
        [](std::unique_ptr<dolbyio::comms::refresh_token>&& refresh_token) {
          (void)refresh_token;
        }
      ).release();
    }}.result();
  }

  EXPORT_API int SetLogLevel(uint32_t log_level) {
    return call { [&]() {
      sdk::set_log_level((dolbyio::comms::log_level) log_level);
    }}.result();
  }

  EXPORT_API int Release() {
    return call { [&]() {
      for (const auto& [key, value] : handlers_map)
        wait(value->disconnect());

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

} // end of export C block
} // dolbyio::comms::native
