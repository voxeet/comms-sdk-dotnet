#ifndef _HANDLERS_H_
#define _HANDLERS_H_

#include <map>
#include <set>

namespace dolbyio::comms::native {  

  extern std::map<std::string, std::map<std::int32_t, dolbyio::comms::event_handler_id>> handlers_map;
  
  template<typename Handler>
  void disconnect_handler(std::int32_t hash, typename Handler::type handler) {
    auto result = handlers_map.find(Handler::name);
    
    if (result != std::end(handlers_map)) {
      auto event_handler = result->second.find(hash);
      if (event_handler != std::end(result->second)) {
        wait(event_handler->second->disconnect());
      }
    }
  }

  template<typename Handler, typename Service>
  void handle(Service& service, std::int32_t hash, typename Handler::type handler, std::function<void(const typename Handler::event&)> f) {
#ifndef MOCK
    if (handlers_map.find(Handler::name) == handlers_map.end()) {
      handlers_map.emplace(Handler::name, std::map<std::int32_t, dolbyio::comms::event_handler_id>{});
    }

    auto it = handlers_map.find(Handler::name);
    if (it != handlers_map.end()) {
      auto test = reinterpret_cast<std::intptr_t>(handler);
      it->second.emplace(hash, wait(service.add_event_handler(std::move(f))));
    }
#endif
  }
  
  template<typename Handler, typename Service>
  void handle(Service& service, typename Handler::type handler, std::function<void(const typename Handler::event&)> f) {
    handle<Handler, Service>(service, 0, handler, f);
  }
} // namespace dolbyio::comms::native

#endif // _HANDLERS_H_