using System;

/// <Summary> This represents a state with it's lifetime bound to it's usage </Summary>
public interface IState {
    /// <Summary> Begins the state with a state machine </Summary>
    void Begin(StateMachine stateMachine);

    /// <Summary> Updates the state with a state machine </Summary>
    void Update(StateMachine stateMachine);

    /// <Summary> Ends the state with a state machine </Summary>
    void End(StateMachine stateMachine);
}