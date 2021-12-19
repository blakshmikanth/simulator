import React, { useState, useEffect, useRef } from "react";
import Messages from "./Messages";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { AgentResource, EventAgentStateChanged, LogMessage } from "./types";
import Agents from "./components/Agents";

const App = () => {
  const [connection, setConnection] = useState<any>(null);
  const [messages, setMessages] = useState<LogMessage[]>([]);
  const latestMessages = useRef<LogMessage[]>([]);
  const configuredAgents = useRef<AgentResource[]>([]);

  const [agents, setAgents] = useState<AgentResource[]>([]);

  latestMessages.current = messages;
  configuredAgents.current = agents;

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl("https://localhost:5001/hubs/message")
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);

    const fetchAgents = async () => {
      const response = await fetch("https://localhost:5001/api/agents");
      const data = await response.json();
      setAgents(data);
    };

    fetchAgents();
  }, []);

  useEffect(() => {
    const getStates = async () => {
      // Get latest agent states
      await fetch("https://localhost:5001/api/agents/latest");
    };

    if (connection) {
      connection
        .start()
        .then(() => {
          console.log("Connected to hub. Getting latest agent states...");
          getStates();

          // subscribe to the "ReceiveMessage" event
          connection.on("ReceiveMessage", (message: LogMessage) => {
            console.log("Received message: ", message);
            const updatedMessage = [...latestMessages.current];
            updatedMessage.push(message);
            setMessages(updatedMessage);
          });

          // subscribe to the "ResponseAgents" event
          connection.on("EventAgentStateChanged", (message: EventAgentStateChanged) => {
            console.log("Received EventAgentStateChanged event: ", message);
            const updatedMessage = [...latestMessages.current];
            const logMessage = {
              id: Math.round(Math.random() * 10000).toString(),
              timestamp: new Date().toLocaleString(),
              text: `Agent ${message.id} is now ${message.state}`,
            };
            updatedMessage.push(logMessage);
            setMessages(updatedMessage);

            // get agent from the collection
            const updatedAgents = [...configuredAgents.current];
            const selectedAgent = updatedAgents.find((x) => x.id === message.id);

            if (selectedAgent) {
              selectedAgent.state = message.state;
              console.log("Updated agents", updatedAgents);
              setAgents(updatedAgents);
            }
          });
        })
        .catch((err: any) => console.error(err));
    }

    return () => {
      if (connection) connection.stop();
    };
  }, [connection]);

  const handleClear = () => {
    setMessages([]);
  };

  return (
    <>
      <div className="d-flex justify-content-between align-items-center my-3"></div>
      <div className="container-fluid">
        <div className="row">
          <Agents agents={agents} />
          <Messages messages={messages} clear={handleClear} />
        </div>
      </div>
    </>
  );
};

export default App;
