using Godot;
using System;

public class Mario : Node2D, IActor {
    public Stage Stage { get; set; }

    /// <Summary> The current state of the movement state machine </Summary>
    public IState MovementState { get { return movementStateMachine.State; } set { movementStateMachine.State = value; } }

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

    [Export(PropertyHint.Range, "0,1")] public float AirIdleDampening { get; set; } = 0.5f;

    [Export] public float GroundWalkAcceleration { get; set; } = 10f;
    [Export(PropertyHint.Range, "0,1")] public float GroundWalkDampening { get; set; } = 0.5f;

    [Export] public float AirWalkAcceleration { get; set; } = 10f;
    [Export(PropertyHint.Range, "0,1")] public float AirWalkDampening { get; set; } = 0.5f;

    [Export] public float GroundRunAcceleration { get; set; } = 10f;
    [Export(PropertyHint.Range, "0,1")] public float GroundRunDampening { get; set; } = 0.5f;

    [Export] public float AirRunAcceleration { get; set; } = 10f;
    [Export(PropertyHint.Range, "0,1")] public float AirRunDampening { get; set; } = 0.5f;

    // Private variables

    /// <Summary> The horizontal movement state machine </Summary>
    private StateMachine movementStateMachine = new StateMachine();

    /// <Summary> The current delta time </Summary>
    private float delta;

    public override void _Ready() {
        Sprite = (AnimatedSprite)GetNode(SpritePath);
        MovementState = new MarioIdleState(this);
    }

    public override void _Process(float currentDelta) {
        if (Input.IsActionPressed("move_right") || Input.IsActionPressed("move_left")) {
            FacingDirection = Input.IsActionPressed("move_right") ? 1 : -1;
            MoveDirection = FacingDirection;
        } else {
            MoveDirection = 0;
        }

        delta = currentDelta;
        movementStateMachine.Update();

        // apply physics
        Position += Velocity * delta;
    }

    /// <Summary> Handles basic movement state change logic, returns true if the state was changed </Summary>
    public bool MovementStateChangeUpdate() {
        return false;
    }

    /// <Summary> Handles basic horizontal movement logic </Summary>
    public void HorizontalMovementUpdate(float acceleration, float dampening) {
        float horizontalVelocity = Velocity.x;
        horizontalVelocity += MoveDirection * acceleration;
        horizontalVelocity *= Mathf.Pow(1f - dampening, delta * SpeedFactor);
        Velocity = new Vector2(horizontalVelocity, Velocity.y);
    }
}
