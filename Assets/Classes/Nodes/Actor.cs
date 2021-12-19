using Godot;
using System;

/// <Summary> This node has a lifetime for the duration of the actor in game </Summary>
[Actor(Id = 0)]
public class Actor : Node2D {
    /// <Summary> The stage the actor is in </Summary>
    protected Stage Stage { get; set; }

    public void Construct(Stage stage) {
        Stage = stage;
    }
}
