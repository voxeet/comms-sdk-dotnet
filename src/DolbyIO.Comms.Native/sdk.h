#ifndef _PLUGIN_H_
#define _PLUGIN_H_

#if _MSC_VER 
#define EXPORT_API __declspec(dllexport)
#else
#define EXPORT_API 
#endif

#define DOLBYIO_COMMS_ENABLE_DEPRECATED_WAIT

#include <dolbyio/comms/sdk.h>
#include <map>
#include "utils.h"
#include "handlers.h"
#include "translators.h"

namespace dolbyio::comms::native {

  struct constants {
    static constexpr int DEVICE_GUID_SIZE = 24;
    static constexpr int MAX_PERMISSIONS = 12;
  };

  struct on_signaling_channel_exception {
    using event = dolbyio::comms::signaling_channel_exception;
    using type = void (*)(const char* message);
    static constexpr const char* name = "on_signaling_channel_exception";
  };

  struct on_invalid_token_exception {
    using event = dolbyio::comms::invalid_token_exception;
    using type = void (*)(const char* reason, const char* description);
    static constexpr const char* name = "on_invalid_token_exception";
  };

  using refresh_delegate_type = char* (*)();

  extern dolbyio::comms::sdk* sdk;

} // namespace dolbyio::comms::native

#endif // _PLUGIN_H_