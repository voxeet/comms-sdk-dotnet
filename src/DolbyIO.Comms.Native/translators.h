#ifndef _TRANSLATORS_H_
#define _TRANSLATORS_H_

#include <dolbyio/comms/sdk.h>

namespace dolbyio::comms::native {

  template<typename T>
  struct has_to_cpp_method {
    template <typename C> static char test(decltype(&C::to_cpp));
    template <typename C> static long test(...);

    static constexpr bool value = sizeof(test<T>(0)) == sizeof(char);
  };

  template<typename T>
  struct has_to_c_method {
    template <typename C> static char test(decltype(&C::to_c));
    template <typename C> static long test(...);

    static constexpr bool value = sizeof(test<T>(0)) == sizeof(char);
  };

  template<typename C, typename CPP> 
  struct translator_traits {
    using cpp_type = CPP;
    using c_type = C;
  };

  template<typename F, typename T, typename Traits = translator_traits<F, T>> 
  struct translator {
     // Duck typing method to implement in translators
     // static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src);
     // static void to_cpp(typename Traits::cpp_type& dest, typename Traits::c_type* src);
  };

  template<typename T, typename F> T to_cpp(F* f) {
    static_assert(has_to_cpp_method<translator<F, T>>::value, "Implement to_cpp method");
    T t;
    translator<F, T>::to_cpp(t, f);
    return t;
  }

  template<typename F, typename T> F* to_c(const T& t) {
    static_assert(has_to_c_method<translator<F, T>>::value, "Implement to_c method");
    F* f = (F*)malloc(sizeof(F));
    translator<F, T>::to_c(f, t);
    return f;
  }

  template<typename F, typename T> void no_alloc_to_c(F* f, const T& t) {
    static_assert(has_to_c_method<translator<F, T>>::value, "Implement to_c method");
    translator<F, T>::to_c(f, t);
  }

  template<typename F, typename T> void no_alloc_to_cpp(T& t, F* f) {
    static_assert(has_to_cpp_method<translator<F, T>>::value, "Implement to_c method");
    translator<F, T>::to_cpp(t, f);
  }

} // namespace dolbyio::comms::native

#endif // _TRANSLATORS_H_