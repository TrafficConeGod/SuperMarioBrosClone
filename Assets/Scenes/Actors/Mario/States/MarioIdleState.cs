using System;
using Godot;

public class MarioIdleState : MarioState {
    public MarioIdleState(Mario mario) : base(mario) {}

    public override void Update(StateMachine stateMachine) {
        Mario.MovementUpdate();
    }
}