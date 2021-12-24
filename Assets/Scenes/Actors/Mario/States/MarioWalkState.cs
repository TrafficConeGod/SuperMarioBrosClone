using System;
using Godot;

public class MarioWalkState : MarioState {
    public MarioWalkState(Mario mario) : base(mario) {}

    public override void Update(StateMachine stateMachine) {
        Mario.MovementUpdate();
    }
}