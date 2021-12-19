using Godot;
using System;

/// <Summary> Thrown when the code reaches an invalid state </Summary>
[Serializable]
class InvalidStateException : Exception {
	public InvalidStateException() {}

	public InvalidStateException(string name)
	: base(String.Format("InvalidStateException: " + name)) {
		GD.Print("InvalidStateException: " + name);
	}
}
