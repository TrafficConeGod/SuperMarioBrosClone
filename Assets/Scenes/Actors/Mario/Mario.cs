using Godot;
using System;

[Actor(Id = 2, Scene = "res://Assets/Scenes/Actors/Mario/Mario.tscn")]
public class Mario : Actor {
    public override void _Ready() {
        GD.Print("Hello world!");
    }
}
