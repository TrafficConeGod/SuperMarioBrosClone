using System;
using System.Reflection;
using System.Collections.Generic;
using Godot;

public class ActorType {
    /// <Summary> The id of the actor type </Summary>
    public ushort Id { get; set; }

    /// <Summary> The scene of the actor type </Summary>
    public PackedScene Scene { get; set; }

    /// <Summary> A list of all editable properties of the actor type </Summary>
    public List<PropertyInfo> EditableProperties { get; set; }

    private void AddEditablePropertiesForType(Type type) {
        // Add base type editable properties if the type has one
        if (type.BaseType != null && IsActor(type.BaseType)) {
            AddEditablePropertiesForType(type.BaseType);
        }

        // Get all properties of the actor type
        foreach (PropertyInfo info in type.GetProperties()) {
            if (info.DeclaringType == type && info.GetCustomAttribute<EditablePropertyAttribute>() != null) {
                // Add the editable property to the list
                EditableProperties.Add(info);
            }
        }
    }

    public ActorType(Type type) {
        // Get the actor attribute data
        var attribute = type.GetCustomAttribute<ActorAttribute>();
        Id = attribute.Id;
        Scene = (PackedScene)ResourceLoader.Load(attribute.Scene);

        EditableProperties = new List<PropertyInfo>();
        AddEditablePropertiesForType(type);
    }

    /// <Summary> Whether or not the type is an actor type </Summary>
    public static bool IsActor(Type type) {
        return type.GetCustomAttribute<ActorAttribute>() != null;
    }
}