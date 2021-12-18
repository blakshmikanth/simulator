import React from "react";
import Message from "./Message";
import { LogMessage } from "./types";

type MessagesProps = {
  messages: LogMessage[];
};

const Messages = (props: MessagesProps) => {
  const messages = props.messages.map((message, index) => {
    return <Message key={message.id} message={message} />;
  });

  return (
    <div className="bg-light col-sm-6">
      <div className="border border-0">{messages}</div>
    </div>
  );
};

export default Messages;
