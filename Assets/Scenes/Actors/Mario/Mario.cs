using Godot;
using System;

public class Mario : Node2D, IActor {
    public Stage Stage { get; set; }

    /// <Summary> The state machine of mario </Summary>
    private StateMachine stateMachine = new StateMachine();

    /// <Summary> The current velocity of mario </Summary>
    public Vector2 Velocity { get; set; } = new Vector2(0, 0);

    /// <Summary> The current acceleration of mario </Summary>
    public Vector2 Acceleration { get; set; } = new Vector2(0, 0);

    /// <Summary> The current facing direction of mario </Summary>
    public int FacingDirection { get; private set; } = 1;

    /// <Summary> The current moving direction of mario </Summary>
    public int MoveDirection { get; private set; } = 0;

    // Editor properties

    /// <Summary> The walking acceleration of mario </Summary>
    [Export] public float WalkAcceleration { get; set; } = 50;

    /// <Summary> The walking max speed of mario </Summary>
    [Export] public float WalkMaxSpeed { get; set; } = 50;

    public override void _Ready() {
        stateMachine.PushState(new MarioIdleState(this));
    }

    public override void _Process(float delta) {
        stateMachine.Update();

        // apply physics
        Velocity += Acceleration * delta;
        Acceleration = new Vector2(0, 0);
        Position += Velocity * delta;
    }

    /// <Summary> Handles movement logic for mario </Summary>
    public void MovementUpdate() {
        if (Input.IsActionPressed("move_right") || Input.IsActionPressed("move_left")) {
            FacingDirection = Input.IsActionPressed("move_right") ? 1 : -1;
            MoveDirection = FacingDirection;
            if (stateMachine.CurrentState is MarioIdleState) {
                stateMachine.PushState(new MarioWalkState(this));
            }
        } else {
            MoveDirection = 0;
            if (stateMachine.CurrentState is MarioWalkState) {
                stateMachine.PopState();
            }
        }

        if (Math.Abs(Velocity.x) < WalkMaxSpeed) {
            Acceleration += new Vector2(MoveDirection * WalkAcceleration, 0);
        }
    }
}
