using System;
using Godot;

public class MarioJumpState : MarioState {
    public MarioJumpState(Mario mario) : base(mario) {}

    public override void Begin() {
        Mario.Sprite.Play("jump");
        Mario.Velocity = new Vector2(Mario.Velocity.x, -Mario.JumpVelocity);
    }

    public override void Update() {
        Mario.FacingDirectionUpdate();
        if (Mario.MovementStateChangeUpdate()) { return; }

        Mario.AirHorizontalMovementUpdate();
    }

    public override void End() {
        Mario.Sprite.Stop();
    }
}