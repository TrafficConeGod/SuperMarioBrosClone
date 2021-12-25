using System;

/// <Summary> This represents a state with it's lifetime bound to it's usage </Summary>
public interface IState {
    /// <Summary> Begins the state </Summary>
    void Begin();

    /// <Summary> Updates the state </Summary>
    void Update();

    /// <Summary> Ends the state </Summary>
    void End();
}