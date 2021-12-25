using System;
using System.Collections.Generic;

public class StateMachine {
    private IState _state;

    /// <Summary> The current state of the state machine </Summary>
    public IState State {
        get {
            return _state;
        }
        set {
            if (_state != null) {
                _state.End();
            }
            _state = value;
            if (_state != null) {
                _state.Begin();
            }
        }
    }

    /// <Summary> Updates the current state </Summary>
    public void Update() {
        _state.Update();
    }
}