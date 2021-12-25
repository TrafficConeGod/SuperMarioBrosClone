using System;

public abstract class MarioState : IState {
    protected Mario Mario { get; private set; }

    public MarioState(Mario mario) {
        Mario = mario;
    }

    public virtual void Begin() {}
    public virtual void Update() {}
    public virtual void End() {}
}