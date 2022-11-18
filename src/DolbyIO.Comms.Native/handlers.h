#ifndef _HANDLERS_H_
#define _HANDLERS_H_

namespace dolbyio::comms::native {  
  extern std::map<std::string, dolbyio::comms::event_handler_id> handlers_map;

  static void disconnect_handler(const char* name) {
    auto result = handlers_map.find(name);
    if (result != std::end(handlers_map)) {
      wait(result->second->disconnect());
    }
  }

  template<typename Handler, typename Service>
  void handle(Service& service, typename Handler::type handler, std::function<void(const typename Handler::event&)> f) {
#ifndef MOCK
    disconnect_handler(Handler::name);
    
    if(handler) {
      handlers_map.emplace(Handler::name, wait(service.add_event_handler(std::move(f))));
    }
#endif
  }
}

#endif // _HANDLERS_H_