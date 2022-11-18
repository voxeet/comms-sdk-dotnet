#ifndef _MEDIA_DEVICE_H_
#define _MEDIA_DEVICE_H_

#include "sdk.h"

namespace dolbyio::comms::native
{
  /**
   * @brief C# AudioDevice C struct.
   */
  struct audio_device {
    char  uid[constants::DEVICE_GUID_SIZE];
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
    using type = void (*)(char uid[constants::DEVICE_GUID_SIZE]);
    static constexpr const char* name = "on_audio_device_removed";
  };

  struct on_audio_device_changed {
    using event = dolbyio::comms::audio_device_changed;
    using type = void (*)(audio_device dev, bool no_device);
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
  struct translator<dolbyio::comms::native::audio_device, dolbyio::comms::dvc_device, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      std::memcpy(dest->uid, &src.uid()[0], src.uid().size());
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
  };
}

#endif // _MEDIA_DEVICE_H_