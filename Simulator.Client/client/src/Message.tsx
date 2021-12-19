import React from "react";
import { LogMessage } from "./types";

type MessageProps = {
  message: LogMessage;
};

const Message = ({ message }: MessageProps) => {
  return (
    <div className="w-100">
      <p className="px-1 my-1 small">
        <span className="me-3">{message.timestamp}</span>
        {message.text}
      </p>
    </div>
  );
};

export default React.memo(Message);
