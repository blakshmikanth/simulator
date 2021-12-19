import React from "react";
import { AgentResource } from "../types";
import Agent from "./Agent";

type AgentsProps = {
  agents: AgentResource[];
};

const Agents = ({ agents }: AgentsProps) => {
  const agentsList = agents.map((agent) => {
    return <Agent key={agent.id} agent={agent} />;
  });

  return (
    <div className="bg-white col-sm-12 px-2">
      <h3 className="my-2">Agents</h3>
      <div className="border border-0">
        <table className="table table-striped>">
          <thead>
            <tr>
              <th scope="col">Id</th>
              <th scope="col">First Name</th>
              <th scope="col">Last Name</th>
              <th scope="col">Extension</th>
              <th scope="col">Status</th>
            </tr>
          </thead>
          <tbody>{agentsList}</tbody>
        </table>
      </div>
    </div>
  );
};

export default Agents;
