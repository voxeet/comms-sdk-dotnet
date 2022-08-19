#ifndef _SESSION_H_
#define _SESSION_H_

#include "translators.h"
#include "handlers.h"

namespace dolbyio::comms::native {
  /**
   * @brief C# UserInfo C struct.
   */
  struct user_info {
    char* id;
    char* name;
    char* external_id;
    char* avatar_url;
  };

  /**
   * @brief Translator specialisation for dolbyio::comms::services::session::user_info.
   * 
   * @tparam Traits 
   */
  template<typename Traits>
  struct translator<dolbyio::comms::native::user_info, dolbyio::comms::services::session::user_info, Traits> {
    static void to_c(typename Traits::c_type* dest, const typename Traits::cpp_type& src) {
      dest->id = strdup(src.participant_id.value_or(""));
      dest->name = strdup(src.name);
      dest->external_id = strdup(src.externalId);
      dest->avatar_url = strdup(src.avatarUrl);
    }

    static void to_cpp(typename Traits::cpp_type& dest, typename Traits::c_type* src) {
      dest.name = std::string(src->name);
      dest.externalId = std::string(src->external_id);
      dest.avatarUrl = std::string(src->avatar_url);
      // Don't copy id (Return only value)
    }
  };

} // namespace dolbyio::comms::native


#endif // _SESSION_H_