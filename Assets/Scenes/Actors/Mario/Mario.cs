using Godot;
using System;

public class Mario : Node2D, IActor {
    public Stage Stage { get; set; }

    /// <Summary> The movement state machine </Summary>
    public StateMachine MovementStateMachine { get; set; } = new StateMachine();

    /// <Summary> The animation sprite </Summary>
    public AnimatedSprite Sprite { get; set; }

    /// <Summary> The current velocity </Summary>
    public Vector2 Velocity { get; set; } = new Vector2(0, 0);

    private int _facingDirection = 1;

    /// <Summary> The current facing direction </Summary>
    public int FacingDirection { get { return _facingDirection; } private set {
        _facingDirection = value;
        Sprite.FlipH = _facingDirection == -1;
    } }

    /// <Summary> The current moving direction </Summary>
    public int MoveDirection { get; private set; } = 0;

    // Editor properties

    /// <Summary> The path of the animation sprite </Summary>
    [Export] public NodePath SpritePath { get; set; }

    /// <Summary> The factor that the delta gets multiplied by </Summary>
    [Export] public float SpeedFactor { get; set; } = 10f;

    [Export(PropertyHint.Range, "0,1")] public float GroundIdleDampening { get; set; } = 0.5f;

    [Export] public float GroundWalkAcceleration { get; set; } = 10f;
    [Export(PropertyHint.Range, "0,1")] public float GroundWalkDampening { get; set; } = 0.5f;

    // Private variables

    private float delta;

    public override void _Ready() {
        Sprite = (AnimatedSprite)GetNode(SpritePath);
        MovementStateMachine.PushState(new MarioIdleState(this));
    }

    public override void _Process(float currentDelta) {
        delta = currentDelta;
        MovementStateMachine.Update();

        // apply physics
        Position += Velocity * delta;
    }

    /// <Summary> Handles movement logic </Summary>
    public void MovementUpdate(float acceleration, float dampening) {
        if (Input.IsActionPressed("move_right") || Input.IsActionPressed("move_left")) {
            FacingDirection = Input.IsActionPressed("move_right") ? 1 : -1;
            MoveDirection = FacingDirection;
            if (MovementStateMachine.CurrentState is MarioIdleState) {
                MovementStateMachine.PushState(new MarioWalkState(this));
            }
        } else {
            MoveDirection = 0;
            if (MovementStateMachine.CurrentState is MarioWalkState) {
                MovementStateMachine.PopState();
            }
        }

        float horizontalVelocity = Velocity.x;
        horizontalVelocity += MoveDirection * acceleration;
        horizontalVelocity *= Mathf.Pow(1f - dampening, delta * SpeedFactor);
        Velocity = new Vector2(horizontalVelocity, Velocity.y);
    }
}
