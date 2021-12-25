using System;
using Godot;

public class MarioWalkState : MarioState {
    public MarioWalkState(Mario mario) : base(mario) {}

    public override void Begin() {
        Mario.Sprite.Play("walk");
    }

    public override void Update() {
        Mario.MovementUpdate(Mario.GroundWalkAcceleration, Mario.GroundWalkDampening);
    }

    public override void End() {
        Mario.Sprite.Stop();
    }
}