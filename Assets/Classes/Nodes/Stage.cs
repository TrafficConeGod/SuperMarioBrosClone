using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;

/// <Summary> This node has a lifetime for the entire existence of the current loaded stage </Summary>
public class Stage : Node {
    /// <Summary> A dictionary of all actor types </Summary>
	private static Dictionary<ushort, ActorType> actorTypeCache = new Dictionary<ushort, ActorType>();

    /// <Note> stream shall not be used after this call </Note>
    public void Construct(DataStream stream) {
        while (!stream.Empty) {
            ConstructActorFromStream(stream);
            break; // temporary
        }
    }

    /// <Summary> Initializes the actor scene cache </Summary>
    private void InitializeActorTypeCache() {
		Assembly assembly = typeof(Stage).Assembly;
		// loop through types on the assembly
		foreach (Type type in assembly.GetTypes()) {
			if (ActorType.IsActor(type)) {
                var actorType = new ActorType(type);
                // get the actor type
                actorTypeCache[actorType.Id] = actorType;
            }
		}
	}

	/// <Summary> Gets the actor type from id </Summary>
	private ActorType GetActorTypeFromId(ushort id) {
		// get the scene if the actorTypeCache does not have the type
		if (actorTypeCache.Count == 0) {
			InitializeActorTypeCache();
		}
		if (actorTypeCache.ContainsKey(id)) {
			return actorTypeCache[id];
		}
		throw new InvalidStateException("No actor found with id " + id);
	}

    /// <Summary> Constructs an actor from the data stream </Summary>
    private Actor ConstructActorFromStream(DataStream stream) {
        var id = stream.ReadUShort();
        var actorType = GetActorTypeFromId(id);
        var actor = (Actor)actorType.Scene.Instance();
        actor.Construct(this);
        AddChild(actor);
        return actor;
    }

    public override void _Ready() {
        
    }
}
