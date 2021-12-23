using Godot;
using System;

/// <Summary> This node has a lifetime for the entire existence of the game stage system being played </Summary>
public class Player : Node2D {
    public override void _Ready() {
        OS.WindowSize = new Vector2(768, 720); // hacky way to set the window size

        var stage = Stage.Construct("res://Assets/Scenes/Stages/Stage1.tscn");
        AddChild(stage);
    }
}
