using System;
using Godot;

public class MarioIdleState : MarioState {
    public MarioIdleState(Mario mario) : base(mario) {}

    public override void Begin() {
        Mario.Sprite.Play("default");
    }

    public override void Update() {
        Mario.MovementUpdate(0, Mario.GroundIdleDampening);
    }

    public override void End() {
        Mario.Sprite.Stop();
    }
}