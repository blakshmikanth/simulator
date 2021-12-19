import React from "react";
import Message from "./Message";
import { LogMessage } from "./types";

type MessagesProps = {
  messages: LogMessage[];
  clear: () => void;
};

const Messages = (props: MessagesProps) => {
  const messages = props.messages.map((message, index) => {
    return <Message key={message.id} message={message} />;
  });

  return (
    <div className="col-sm-12" style={{ minHeight: "300px" }}>
      <div className="d-flex justify-content-between align-items center my-3">
        <h3>Messages</h3>
        <button className="btn btn-light me-3 btn-sm" onClick={props.clear}>
          Clear messages
        </button>
      </div>
      <div className="bg-light border border-0 pt-2">{messages}</div>
    </div>
  );
};

export default Messages;
