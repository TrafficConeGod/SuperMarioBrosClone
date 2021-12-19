using Godot;
using System;

/// <Summary> This node has a lifetime for the entire existence of the game stage system being played </Summary>
public class Player : Node {
    public override void _Ready() {
        // var stage = new Stage();
        // stage.Construct(new DataStream("Assets/Stages/MainStage.stg"));
        // AddChild(stage);
        var stream = new DataStream("Assets/Stages/MainStage.stg");
        GD.Print(stream.ReadUInt16());
    }
}
