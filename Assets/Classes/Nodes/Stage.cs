using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;

/// <Summary> This node has a lifetime for the entire existence of the current loaded stage </Summary>
public class Stage : Node {
    /// <Summary> A cache of packed scenes </Summary>
    private static Dictionary<string, PackedScene> packedSceneCache = new Dictionary<string, PackedScene>();

    public static Stage Construct(string path) {
        if (!packedSceneCache.ContainsKey(path)) {
            var packedScene = (PackedScene)ResourceLoader.Load(path);
            if (packedScene == null) {
                throw new InvalidStateException("Could not load stage at path: " + path);
            }
            packedSceneCache.Add(path, packedScene);
        }
        var stage = (Stage)packedSceneCache[path].Instance();
        foreach (var child in stage.GetChildren()) {
            if (child.GetType().IsAssignableFrom(typeof(IActor))) {
                ((IActor)child).Stage = stage;
            }
        }
        return stage;
    }
}
