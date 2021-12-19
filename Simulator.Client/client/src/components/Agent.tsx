import React from "react";
import { AgentResource, AgentStateEnum, RequestAgentStateChange } from "../types";
import { v4 as uuidv4 } from "uuid";
import SmallButton from "./SmallButton";

type AgentProps = {
  agent: AgentResource;
};

const Agent = ({ agent }: AgentProps) => {
  console.log("Rendering.." + JSON.stringify(agent));

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

  const getStateStyle = (state: AgentStateEnum) => {
    switch (state) {
      case AgentStateEnum.Unknown:
        return "bg-dark";
      case AgentStateEnum.Busy:
        return "bg-warning";
      case AgentStateEnum.LoggedOff:
        return "bg-secondary";
      case AgentStateEnum.NotReady:
        return "bg-danger";
      case AgentStateEnum.Ready:
        return "bg-success";
      case AgentStateEnum.Reserved:
        return "bg-info";
      default:
        return "bg-dark";
    }
  };

  const setAgentState = async (state: AgentStateEnum) => {
    const data: RequestAgentStateChange = {
      requestId: uuidv4().toString(),
      agentId: agent.id,
      state: state,
      timestamp: Date.now(),
    };

    const result = await fetch(`https://localhost:5001/api/agents/change-state`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });

    console.log("Result: " + result);
  };

  return (
    <tr>
      <td>{agent.id}</td>
      <td>{agent.firstName}</td>
      <td>{agent.lastName}</td>
      <td>{agent.extension}</td>
      <td style={{ minWidth: "128px" }}>
        <span className={`text-white ${getStateStyle(agent.state)} p-1`}>
          {getAgentState(agent.state)}
        </span>
      </td>
      <td>
        <SmallButton state={AgentStateEnum.Ready} onClick={setAgentState} text="Ready" />
      </td>
      <td>
        <SmallButton state={AgentStateEnum.NotReady} onClick={setAgentState} text="Not Ready" />
      </td>
      <td>
        <SmallButton state={AgentStateEnum.Busy} onClick={setAgentState} text="Busy" />
      </td>
      <td>
        <SmallButton state={AgentStateEnum.LoggedOff} onClick={setAgentState} text="Log off" />
      </td>
    </tr>
  );
};

export default Agent;
