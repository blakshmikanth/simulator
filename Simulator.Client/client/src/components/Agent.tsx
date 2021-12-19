import React from "react";
import { AgentResource, AgentStateEnum } from "../types";

type AgentProps = {
  agent: AgentResource;
};

const Agent = ({ agent }: AgentProps) => {
  const getAgentState = (state: AgentStateEnum) => {
    switch (state) {
      case AgentStateEnum.Unknown:
        return "Unknown";
      case AgentStateEnum.Busy:
        return "Busy";
      case AgentStateEnum.LoggedOff:
        return "Logged Off";
      case AgentStateEnum.NotReady:
        return "Not Ready";
      case AgentStateEnum.Ready:
        return "Ready";
      case AgentStateEnum.Reserved:
        return "Reserved";
      default:
        return "Unknown";
    }
  };

  return (
    <tr>
      <td>{agent.id}</td>
      <td>{agent.firstName}</td>
      <td>{agent.lastName}</td>
      <td>{agent.extension}</td>
      <td>{getAgentState(agent.state)}</td>
    </tr>
  );
};

export default Agent;
