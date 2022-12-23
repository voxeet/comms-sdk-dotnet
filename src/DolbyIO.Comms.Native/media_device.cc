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
        char uid[constants::DEVICE_GUID_SIZE];
        std::memcpy(uid, &e.uid[0], e.uid.size());
        handler(uid);
      }
    );
  }

  EXPORT_API void SetOnAudioDeviceChangedHandler(on_audio_device_changed::type handler) {
    handle<on_audio_device_changed>(sdk->device_management(), handler, 
      [handler](const on_audio_device_changed::event& e) {
        audio_device dev;
        no_alloc_to_c(&dev, e.device);
        handler(dev, e.no_device);
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

  EXPORT_API int GetAudioDevices(int* size, dolbyio::comms::native::audio_device** dest) {
    return call { [&]() {
      auto devices = wait(sdk->device_management().get_audio_devices());
      (*dest) = (dolbyio::comms::native::audio_device*) malloc(sizeof(dolbyio::comms::native::audio_device) * devices.size());
      
      std::for_each(devices.begin(), devices.end(), [&devices, dest](const dvc_device& device) {
        int index = &device - &devices[0];
        no_alloc_to_c(&(*dest)[index], device);
      });

      (*size) = devices.size();
    }}.result();
  }

  EXPORT_API int SetPreferredAudioInputDevice(dolbyio::comms::native::audio_device dev) {
    return call { [&]() {
      auto devices = wait(sdk->device_management().get_audio_devices());
      auto result = std::find_if(devices.begin(), devices.end(), [&dev](const dvc_device& device) {
        return std::memcmp(&device.uid()[0], dev.uid, device.uid().size()) == 0; 
      });

      if (result != std::end(devices)) {
        wait(sdk->device_management().set_preferred_input_audio_device(*result));
      }
    }}.result();
  }

  EXPORT_API int SetPreferredAudioOutputDevice(dolbyio::comms::native::audio_device dev) {
    return call { [&]() {
      auto devices = wait(sdk->device_management().get_audio_devices());
      auto result = std::find_if(devices.begin(), devices.end(), [&dev](const dvc_device& device) {
        return std::memcmp(&device.uid()[0], dev.uid, device.uid().size()) == 0; 
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

} // extern "C"
} // namespace dolbyio::comms::native