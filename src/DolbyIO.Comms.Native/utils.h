#ifndef _UTILS_H_
#define _UTILS_H_

namespace dolbyio::comms::native {

  extern std::string error;

  template <typename E> constexpr auto to_underlying(E e) noexcept {
      return static_cast<std::underlying_type_t<E>>(e);
  }

  static char* strdup(const std::string& s) {
    return std::strcpy((char*)malloc(s.size() + 1), s.c_str());
  }

  // No op deleter
  struct null_deleter {
    template<typename T> void operator()(T *t) { };
  };

  template<typename Exception = std::exception>
  struct call {
  public:
    static constexpr int result_success = 0;
    static constexpr int result_error   = -1;

  public:
    template<typename F> call(const F& f) {
#ifndef MOCK
      try {
        f();
        result_ = result_success;
      } catch (const Exception& e) {
        error = e.what();
        result_ = result_error;
      }
#else
      result_ = result_success;
#endif
    }

    int result() { return result_; }

  private:
    int result_;
  };

} // namespace dolbyio::comms::native

#endif // _UTILS_H_