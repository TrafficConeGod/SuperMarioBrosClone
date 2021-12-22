using Godot;
using System;

[Actor(Id = 3, Scene = "res://Assets/Scenes/Actors/Tilemap/Tilemap.tscn")]
public class Tilemap : Actor {
    [EditableProperty]
    public string Path { get; set; }

    public override void _Ready() {
        GD.Print(Path);
    }
}
