#ifndef _MEDIA_DEVICE_H_
#define _MEDIA_DEVICE_H_

#include "sdk.h"

namespace dolbyio::comms::native {

  /**
   * @brief C# DeviceIdentity C struct.
   */
  struct device_identity {
    void* value;
  };

  /**
   * @brief C# AudioDevice C struct.
   */
  struct audio_device {
    device_identity identity;
    char* name;
    int32_t direction;
  };

  /**
   * @brief C# VideoDevice C Struct
   */
  struct video_device {
    char* uid;
    char* name;
  };

  struct on_audio_device_added {
    using event = dolbyio::comms::audio_device_added;
    using type = void (*)(audio_device dev);
    static constexpr const char* name = "on_audio_device_added";
  };

  struct on_audio_device_removed {
    using event = dolbyio::comms::audio_device_removed;
    using type = void (*)(device_identity);
    static constexpr const char* name = "on_audio_device_removed";
  };

  struct on_audio_device_changed {
    using event = dolbyio::comms::audio_device_changed;
    using type = void (*)(device_identity id, bool no_device);
    static constexpr const char* name = "on_audio_device_changed";
  };

    struct on_video_device_added {
    using event = dolbyio::comms::video_device_added;
    using type = void (*)(video_device dev);
    static constexpr const char* name = "on_video_device_added";
  };

  struct on_video_device_removed {
    using event = dolbyio::comms::video_device_removed;
    using type = void (*)(char* uid);
    static constexpr const char* name = "on_video_device_removed";
  };

  struct on_video_device_changed {
    using event = dolbyio::comms::video_device_changed;
    using type = void (*)(video_device dev);
    static constexpr const char* name = "on_video_device_changed";
  };

  /**
   * @brief Translator specialisation for dolbyio::comms::dvc_device.
   * 
   * @tparam Traits 
   */
  template<typename Traits> 
  struct translator<dolbyio::comms::native::audio_device, dolbyio::comms::audio_device, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      dest->identity.value = (void *)new dolbyio::comms::audio_device::identity(src.get_identity());
      dest->name = strdup(src.name());
      dest->direction = to_underlying(src.direction());
    }
  };

  /**
   * @brief Translator specialisation for dolbyio::comms::camera_device.
   * 
   * @tparam Traits 
   */
  template<typename Traits> 
  struct translator<dolbyio::comms::native::video_device, dolbyio::comms::camera_device, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      dest->name = strdup(src.display_name);
      dest->uid = strdup(src.unique_id);
    }

    static void to_cpp(typename Traits::cpp_type& dest, typename Traits::c_type* src) {
      dest.display_name = std::string(src->name);
      dest.unique_id = std::string(src->uid);
    }
  };

} // namespace dolbyio::comms::native

#endif // _MEDIA_DEVICE_H_