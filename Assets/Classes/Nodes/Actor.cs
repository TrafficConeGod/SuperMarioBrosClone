using Godot;
using System;

/// <Summary> This node has a lifetime for the duration of the actor in game </Summary>
[Actor(Id = 0)]
public class Actor : Node2D {
    /// <Summary> The stage the actor is in </Summary>
    protected Stage Stage { get; set; }

	/// <Summary> Editable position </Summary>
	[EditableProperty]
	public Vector2 EditablePosition { get => Position; set => Position = value; }

	/// <Summary> Editable scale </Summary>
	[EditableProperty]
	public Vector2 EditableScale { get => Scale; set => Scale = value; }

	/// <Summary> Editable rotation </Summary>
	[EditableProperty]
	public float EditableRotation { get => RotationDegrees; set => RotationDegrees = value; }

    public void Construct(Stage stage) {
        Stage = stage;
    }
}
