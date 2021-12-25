using System;
using Godot;

public class MarioWalkState : MarioState {
    public MarioWalkState(Mario mario) : base(mario) {}

    public override void Begin() {
        Mario.Sprite.Play("walk");
    }

    public override void Update() {
        Mario.FacingDirectionUpdate();
        if (Mario.MovementStateChangeUpdate()) { return; }
        if (Mario.MoveDirection == 0 || (!Input.IsActionPressed("move_left") && !Input.IsActionPressed("move_right"))) {
            Mario.MovementState = new MarioIdleState(Mario);
            return;
        } else if (Input.IsActionPressed("run")) {
            Mario.MovementState = new MarioRunState(Mario);
            return;
        }

        Mario.HorizontalMovementUpdate(Mario.GroundWalkAcceleration, Mario.GroundWalkDampening);
    }

    public override void End() {
        Mario.Sprite.Stop();
    }
}