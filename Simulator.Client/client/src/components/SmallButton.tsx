import React from "react";
import { AgentStateEnum } from "../types";

type SmallButtonProps = {
  state: AgentStateEnum;
  text: string;
  onClick: (state: AgentStateEnum) => void;
};

const SmallButton = ({ state, text, onClick }: SmallButtonProps) => {
  return (
    <button className="btn btn-light btn-sm" onClick={() => onClick(state)}>
      {text}
    </button>
  );
};

export default SmallButton;
