using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;

/// <Summary> This node has a lifetime for the entire existence of the current loaded stage </Summary>
public class Stage : Node {
    /// <Summary> A list of all actor scene caches </Summary>
	private static Dictionary<ushort, PackedScene> actorSceneCache = new Dictionary<ushort, PackedScene>();

    /// <Note> stream shall not be used after this call </Note>
    public void Construct(DataStream stream) {
        while (!stream.Empty) {
            ConstructActorFromStream(stream);
            break; // temporary
        }
    }

    /// <Summary> Initializes the actor scene cache </Summary>
    private void InitializeActorSceneCache() {
		Assembly assembly = typeof(Stage).Assembly;
		// loop through types on the assembly
		foreach (Type type in assembly.GetTypes()) {
			// check if class has ActorAttribute attribute
			ActorAttribute actorAttribute = type.GetCustomAttribute<ActorAttribute>();
			// cache the scene if it has the requested id
			if (actorAttribute != null && actorAttribute.Scene != "") {
				actorSceneCache[actorAttribute.Id] = (PackedScene)ResourceLoader.Load(actorAttribute.Scene);
			}
		}
	}

	/// <Summary> Gets the actor scene necessary to load the actor </Summary>
	private PackedScene GetActorSceneWithId(ushort id) {
		// get the scene if the actorSceneCache does not have the type
		if (actorSceneCache.Count == 0) {
			InitializeActorSceneCache();
		}
		if (actorSceneCache.ContainsKey(id)) {
			return actorSceneCache[id];
		}
		throw new InvalidStateException("No actor found with id " + id);
	}

    /// <Summary> Constructs an actor from the data stream </Summary>
    private Actor ConstructActorFromStream(DataStream stream) {
        var id = stream.ReadUShort();
        GD.Print(id);
        var scene = GetActorSceneWithId(id);
        var actor = (Actor)scene.Instance();
        actor.Construct(this);
        AddChild(actor);
        return actor;
    }

    public override void _Ready() {
        
    }
}
