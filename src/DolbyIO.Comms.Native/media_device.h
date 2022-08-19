#ifndef _MEDIA_DEVICE_H
#define _MEDIA_DEVICE_H

#include "sdk.h"

namespace dolbyio::comms::native
{
  /**
   * @brief C# AudioDevice C struct.
   */
  struct device {
    char  uid[constants::DEVICE_GUID_SIZE];
    char* name;
    int32_t direction;
  };

  struct on_device_added {
    using event = dolbyio::comms::device_added;
    using type = void (*)(device dev);
    static constexpr const char* name = "on_device_added";
  };

  struct on_device_removed {
    using event = dolbyio::comms::device_removed;
    using type = void (*)(char uid[constants::DEVICE_GUID_SIZE]);
    static constexpr const char* name = "on_device_removed";
  };

  struct on_device_changed {
    using event = dolbyio::comms::device_changed;
    using type = void (*)(device dev, bool no_device);
    static constexpr const char* name = "on_device_changed";
  };

  /**
   * @brief Translator specialisation for dolbyio::comms::dvc_device.
   * 
   * @tparam Traits 
   */
  template<typename Traits> 
  struct translator<dolbyio::comms::native::device, dolbyio::comms::dvc_device, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      std::memcpy(dest->uid, &src.uid()[0], src.uid().size());
      dest->name = strdup(src.name());
      dest->direction = to_underlying(src.direction());
    }
  };
}

#endif // _MEDIA_DEVICE_H