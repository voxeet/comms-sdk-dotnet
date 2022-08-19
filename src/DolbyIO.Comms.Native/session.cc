#include "sdk.h"
#include "session.h"

namespace dolbyio::comms::native {
extern "C" {

  EXPORT_API int Open(dolbyio::comms::native::user_info* u, dolbyio::comms::native::user_info** r) {
    return call { [&]() {
      auto user = to_cpp<dolbyio::comms::services::session::user_info>(u);
      auto info = wait(sdk->session().open(std::move(user)));
      (*r) = to_c<dolbyio::comms::native::user_info>(info);
    }}.result();
  }

  EXPORT_API int Close() {
    return call { [&]() {
      wait(sdk->session().close());
    }}.result();
  }

} // extern "C"
} // namespace dolbyio::comms::native