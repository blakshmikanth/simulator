export type LogMessage = {
  id: string;
  text: string;
  timestamp: string;
};

export type AgentResource = {
  id: string;
  firstName: string;
  lastName: string;
  extension: string;
  state: AgentStateEnum;
};

export type RequestAgentStateChange = {
  agentId: string;
  state: AgentStateEnum;
  requestId: string;
  timestamp: number;
};

export type EventAgentStateChanged = {
  id: string;
  state: AgentStateEnum;
};

export enum AgentStateEnum {
  Unknown,
  Busy,
  LoggedOff,
  NotReady,
  Ready,
  Reserved,
}
