using System;
using System.Collections.Generic;

public class StateMachine {
    /// <Summary> A stack of all current states </Summary>
    private Stack<IState> stateStack = new Stack<IState>();

    /// <Summary> The current state of the state machine </Summary>
    public IState CurrentState {
        get {
            if (stateStack.Count == 0) {
                return null;
            }
            return stateStack.Peek();
        }
    }

    /// <Summary> Pushes a state to the state stack </Summary>
    public void PushState(IState state) {
        if (CurrentState != null) {
            CurrentState.End();
        }
        stateStack.Push(state);
        state.Begin();
    }

    /// <Summary> Pops the topmost state from the state stack </Summary>
    public void PopState() {
        if (CurrentState != null) {
            CurrentState.End();
            stateStack.Pop();

            if (CurrentState != null) {
                CurrentState.Begin();
            }
        }
    }

    /// <Summary> Updates the current state </Summary>
    public void Update() {
        CurrentState.Update();
    }
}