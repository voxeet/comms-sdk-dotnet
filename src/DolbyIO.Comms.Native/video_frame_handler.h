#ifndef _VIDEO_FRAME_HANDLER_H_
#define _VIDEO_FRAME_HANDLER_H_

namespace dolbyio::comms::native {

class video_frame_handler : public dolbyio::comms::video_frame_handler {
public:
  void sink(video_sink* sink) {
    _sink = sink;
  }

  virtual dolbyio::comms::video_sink* sink() {
    return _sink;
  }

  virtual dolbyio::comms::video_source* source() {
    return nullptr;
  }

private:
  video_sink* _sink = nullptr;
};

} // namespace dolbyio::comms::native

#endif // _VIDEO_FRAME_HANDLER_H_