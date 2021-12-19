using System;
using System.Collections.Generic;
using System.Net.Mail;
using Akka.Actor;
using Microsoft.Extensions.Logging;
using Processor.Contracts;
using Processor.Instructions;
using Processor.Messages;
using Processor.Models;
using Processor.Resources;
using RestSharp;

namespace Processor.Actors
{
    public class AgentActor : ReceiveActor
    {
        private readonly ILogger<AgentActor> _logger;

        private const int Threshold = 100;
        private int _count = 0;
        private int _currentStep = 0;
        private Random _random = new Random();

        private readonly IList<string> _instructions = new List<string>()
        {
            Constants.LogOff,
            Constants.RandomWait,
            Constants.Login,
            Constants.RandomWait,
            Constants.Ready,
            Constants.RandomWait,
        };
        
        private string Id { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Extension { get; set; }

        private AgentStateEnum State { get; set; }

        public AgentActor(ILogger<AgentActor> logger)
        {
            _logger = logger;
            State = AgentStateEnum.Unknown;
            Receive<Agent>(AgentHandler);
            Receive<EventAgentStateChanged>(EventAgentStateChangedHandler);
            Receive<RequestInitiateSimulation>(RequestInitiateSimulationHandler);
        }

        private void AgentHandler(Agent message)
        {
            Id = message.Id;
            FirstName = message.FirstName;
            LastName = message.LastName;
            Extension = message.Extension;
        }

        private void EventAgentStateChangedHandler(EventAgentStateChanged message)
        {
            if (message.Id == Id) State = message.State;
        }

        private void RequestInitiateSimulationHandler(RequestInitiateSimulation message)
        {
            _logger.LogInformation($"Processing instructions message... Count {_count} - Threshold {Threshold} - Step {_currentStep}");
            
            // if threshold is reached, don't process instructions
            if (_count >= Threshold)
            {
                _logger.LogInformation("Threshold reached. Stopping the simulation...");
                return;
            }
                
            
            // Cycle through the instructions
            if (_currentStep >= _instructions.Count) _currentStep = 0;
            
            var selectedInstruction = _instructions[_currentStep];
            
            _logger.LogInformation($"Processing current instruction - {selectedInstruction} , Step - {_currentStep} ");
            
            switch (selectedInstruction)
            {
                case Constants.LogOff:
                    SendRequest(AgentStateEnum.LoggedOff);
                    Self.Tell(message);
                    break;
                case Constants.Login:
                    SendRequest(AgentStateEnum.NotReady);
                    Self.Tell(message);
                    break;
                case Constants.Ready:
                    SendRequest(AgentStateEnum.Ready);
                    Self.Tell(message);
                    break;
                case Constants.Wait5:
                    Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(5), Self, message, Self);
                    break;
                case Constants.Wait10:
                    Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(10), Self, message, Self);
                    break;
                case Constants.Wait15:
                    Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(15), Self, message, Self);
                    break;
                case Constants.Wait20:
                    Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(20), Self, message, Self);
                    break;
                case Constants.Wait30:
                    Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(30), Self, message, Self);
                    break;
                case Constants.Wait3:
                    Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(3), Self, message, Self);
                    break;
                case Constants.RandomWait:
                    var waitPeriod = _random.Next(1, 5);
                    _logger.LogInformation($"Agent {Id} will wait for {waitPeriod} seconds for next instruction");
                    Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(waitPeriod), Self, message, Self);
                    break;
                default:
                    _logger.LogError($"Unexpected instruction {selectedInstruction}");
                    break;
            }

            _count += 1;
            _currentStep += 1;
        }

        protected override void Unhandled(object message)
        {
            _logger.LogError($"Unexpected message arrived {message}");
        }

        private void SendRequest(AgentStateEnum agentState)
        {
            var client = new RestClient("https://localhost:5001");
            var request = new RestRequest("/api/agents/change-state", Method.POST);
            request.AddJsonBody(new RequestUpdateAgentState(Id, agentState));
            var response = client.ExecuteAsync(request).Result;
            _logger.LogInformation($"Update agent state response : {response.StatusCode}");
        }
    }
}