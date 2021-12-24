using Godot;
using System;

/// <Summary> This represents an actor with a stage and a lifetime </Summary>
public interface IActor {
	/// <Summary> The stage this actor is in </Summary>
	Stage Stage { get; set; }
}
