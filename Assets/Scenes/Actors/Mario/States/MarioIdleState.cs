using System;
using Godot;

public class MarioIdleState : MarioState {
    public MarioIdleState(Mario mario) : base(mario) {}

    public override void Begin() {
        Mario.Sprite.Play("default");
    }

    public override void Update() {
        if (Mario.MovementStateChangeUpdate()) { return; }
        if (Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right")) {
            if (Input.IsActionPressed("run")) {
                Mario.MovementState = new MarioRunState(Mario);
            } else {
                Mario.MovementState = new MarioWalkState(Mario);
            }
            return;
        }

        Mario.HorizontalMovementUpdate(0, Mario.GroundIdleDampening);
    }

    public override void End() {
        Mario.Sprite.Stop();
    }
}