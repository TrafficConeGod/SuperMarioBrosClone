using System;
using Godot;

public class MarioRunState : MarioState {
    public MarioRunState(Mario mario) : base(mario) {}

    public override void Begin() {
        Mario.Sprite.Play("run");
    }

    public override void Update() {
        if (Mario.MovementStateChangeUpdate()) { return; }
        if (!Input.IsActionPressed("move_left") && !Input.IsActionPressed("move_right")) {
            Mario.MovementState = new MarioIdleState(Mario);
            return;
        } else if (!Input.IsActionPressed("run")) {
            Mario.MovementState = new MarioWalkState(Mario);
            return;
        }

        Mario.HorizontalMovementUpdate(Mario.GroundRunAcceleration, Mario.GroundRunDampening);
    }

    public override void End() {
        Mario.Sprite.Stop();
    }
}