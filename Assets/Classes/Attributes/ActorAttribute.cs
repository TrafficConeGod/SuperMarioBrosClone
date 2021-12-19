using System;

[AttributeUsage(AttributeTargets.Class)]
public class ActorAttribute : Attribute {
	/// <Summary> The id of the actor </Summary>
	public ushort Id { get; set; }
	/// <Summary> The scene path of the actor </Summary>
	public string Scene { get; set; }
	
	public ActorAttribute() {
		Id = 0;
		Scene = "";
	}
}