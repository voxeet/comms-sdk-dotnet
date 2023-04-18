#include "sdk.h"
#include "media_device.h"
#include "handlers.h"

namespace dolbyio::comms::native {
extern "C" {

  EXPORT_API void SetOnAudioDeviceAddedHandler(on_audio_device_added::type handler) {
    handle<on_audio_device_added>(sdk->device_management(), handler, 
      [handler](const on_audio_device_added::event& e) {
        audio_device dev;
        no_alloc_to_c(&dev, e.device);
        handler(dev);
      }
    );
  }

  EXPORT_API void SetOnAudioDeviceRemovedHandler(on_audio_device_removed::type handler) {
    handle<on_audio_device_removed>(sdk->device_management(), handler, 
      [handler](const on_audio_device_removed::event& e) {
        device_identity id;
        id.value = (void*)new dolbyio::comms::audio_device::identity(e.device_id);
        handler(id);
      }
    );
  }

  EXPORT_API void SetOnAudioDeviceChangedHandler(on_audio_device_changed::type handler) {
    handle<on_audio_device_changed>(sdk->device_management(), handler, 
      [handler](const on_audio_device_changed::event& e) {
        device_identity dev;

        if (e.device.has_value()) {
          dev.value = new dolbyio::comms::audio_device::identity(e.device.value());
        }

        handler(dev, !e.device.has_value());
      }
    );
  }

  EXPORT_API void SetOnVideoDeviceAddedHandler(on_video_device_added::type handler) {
    handle<on_video_device_added>(sdk->device_management(), handler,
      [handler](const on_video_device_added::event& e) {
        video_device dev;
        no_alloc_to_c(&dev, e.device);
        handler(dev);
      }
    );
  }

  EXPORT_API void SetOnVideoDeviceChangedHandler(on_video_device_changed::type handler) {
    handle<on_video_device_changed>(sdk->device_management(), handler,
      [handler](const on_video_device_changed::event& e) {
        video_device dev;
        no_alloc_to_c(&dev, e.device);
        handler(dev);
      }
    );
  }

  EXPORT_API void SetOnVideoDeviceRemovedHandler(on_video_device_removed::type handler) {
    handle<on_video_device_removed>(sdk->device_management(), handler,
      [handler](const on_video_device_removed::event& e) {
        handler(strdup(e.uid));
      }
    );
  }

  EXPORT_API bool AudioDeviceEquals(dolbyio::comms::audio_device::identity* id1, dolbyio::comms::audio_device::identity* id2) {
    return (*id1) == (*id2);
  }

  EXPORT_API int GetAudioDevices(int* size, dolbyio::comms::native::audio_device** dest) {
    return call { [&]() {
      auto devices = wait(sdk->device_management().get_audio_devices());
      (*dest) = (dolbyio::comms::native::audio_device*) malloc(sizeof(dolbyio::comms::native::audio_device) * devices.size());
      
      std::for_each(devices.begin(), devices.end(), [&devices, dest](const dolbyio::comms::audio_device& device) {
        int index = &device - &devices[0];
        no_alloc_to_c(&(*dest)[index], device);
      });

      (*size) = devices.size();
    }}.result();
  }

  EXPORT_API int SetPreferredAudioInputDevice(dolbyio::comms::native::audio_device dev) {
    return call { [&]() {
      auto devices = wait(sdk->device_management().get_audio_devices());
      auto result = std::find_if(devices.begin(), devices.end(), [&dev](const dolbyio::comms::audio_device& device) {
        dolbyio::comms::audio_device::identity* identity = (dolbyio::comms::audio_device::identity*)dev.identity.value;
        return device.get_identity() == *identity;
      });

      if (result != std::end(devices)) {
        wait(sdk->device_management().set_preferred_input_audio_device(*result));
      }
    }}.result();
  }

  EXPORT_API int SetPreferredAudioOutputDevice(dolbyio::comms::native::audio_device dev) {
    return call { [&]() {
      auto devices = wait(sdk->device_management().get_audio_devices());
      auto result = std::find_if(devices.begin(), devices.end(), [&dev](const dolbyio::comms::audio_device& device) {
        dolbyio::comms::audio_device::identity* identity = (dolbyio::comms::audio_device::identity*)dev.identity.value;
        return device.get_identity() == *identity;
      });

      if (result != std::end(devices)) {
        wait(sdk->device_management().set_preferred_output_audio_device(*result));
      }
    }}.result();
  }

  EXPORT_API int GetCurrentAudioInputDevice(dolbyio::comms::native::audio_device* dev) {
    return call { [&]() {
      auto device = wait(sdk->device_management().get_current_audio_input_device());
      
      if (device.has_value()) {
        no_alloc_to_c(dev, device.value());
      }
    }}.result();
  }

  EXPORT_API int GetCurrentAudioOutputDevice(dolbyio::comms::native::audio_device* dev) {
    return call { [&]() {
      auto device = wait(sdk->device_management().get_current_audio_output_device());
      
      if (device.has_value()) {
        no_alloc_to_c(dev, device.value());
      }
    }}.result();
  }

  EXPORT_API int GetVideoDevices(int* size, dolbyio::comms::native::video_device** dest) {
    return call { [&]() {
      auto devices = wait(sdk->device_management().get_video_devices());
      (*dest) = (dolbyio::comms::native::video_device*) malloc(sizeof(dolbyio::comms::native::video_device) * devices.size());
      
      std::for_each(devices.begin(), devices.end(), [&devices, dest](const camera_device& device) {
        int index = &device - &devices[0];
        no_alloc_to_c(&(*dest)[index], device);
      });

      (*size) = devices.size();
    }}.result();
  }

  EXPORT_API int GetCurrentVideoDevice(dolbyio::comms::native::video_device* dev) {
    return call { [&]() {
      auto device = wait(sdk->device_management().get_current_video_device());
      
      if (device.has_value()) {
        no_alloc_to_c(dev, device.value());
      }
    }}.result();
  }

  EXPORT_API int GetScreenShareSources(int* size, dolbyio::comms::native::screen_share_source** dest) {
    return call { [&]() {
      auto sources = wait(sdk->device_management().get_screen_share_sources());
      (*dest) = (dolbyio::comms::native::screen_share_source*) malloc(sizeof(dolbyio::comms::native::screen_share_source) * sources.size());
      
      std::for_each(sources.begin(), sources.end(), [&sources, dest](const dolbyio::comms::screen_share_source& source) {
        int index = &source - &sources[0];
        no_alloc_to_c(&(*dest)[index], source);
      });

      (*size) = sources.size();
    }}.result();
  }

  EXPORT_API bool DeleteDeviceIdentity(dolbyio::comms::audio_device::identity* identity) {
    if (identity) {
      delete identity;
      return true;
    }

    return false;
  }

} // extern "C"
} // namespace dolbyio::comms::native