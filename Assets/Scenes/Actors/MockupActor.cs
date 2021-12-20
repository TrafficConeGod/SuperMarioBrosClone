using Godot;
using System;

/// <Summary> Mockup actor for testing purposes </Summary>
[Actor(Id = 2, Scene = "res://Assets/Scenes/Actors/MockupActor.tscn")]
public class MockupActor : Actor {
    [EditableProperty]
    public int Custom { get; set; }

    public override void _Ready() {
        GD.Print("Hello world!");
        GD.Print(XPosition);
        GD.Print(YPosition);
        GD.Print(Custom);
    }
}
