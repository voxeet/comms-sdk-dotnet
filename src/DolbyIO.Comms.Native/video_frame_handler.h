//#ifndef _VIDEO_FRAME_HANDLER_H_
//#define _VIDEO_FRAME_HANDLER_H_
//
//#include "video_sink.h"
//
//namespace dolbyio::comms::native {
//
//class video_frame_handler : public dolbyio::comms::video_frame_handler {
//public:
//  void sink(video_sink* sink) {
//    // Aliasing constructor
//    _sink = std::shared_ptr<dolbyio::comms::native::video_sink>(std::shared_ptr<dolbyio::comms::native::video_sink>{}, sink);
//  }
//
//  virtual std::shared_ptr<dolbyio::comms::video_sink> sink() {
//    return _sink;
//  }
//
//  virtual std::shared_ptr<dolbyio::comms::video_source> source() {
//    return nullptr;
//  }
//
//private:
//  std::shared_ptr<dolbyio::comms::native::video_sink> _sink;
//};
//
//} // namespace dolbyio::comms::native
//
//#endif // _VIDEO_FRAME_HANDLER_H_