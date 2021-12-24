using System;

public abstract class MarioState : IState {
    protected Mario Mario { get; private set; }

    public MarioState(Mario mario) {
        Mario = mario;
    }

    public virtual void Begin(StateMachine stateMachine) {}
    public virtual void Update(StateMachine stateMachine) {}
    public virtual void End(StateMachine stateMachine) {}
}