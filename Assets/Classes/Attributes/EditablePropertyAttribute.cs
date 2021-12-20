using System;

/// <Summary> Attribute that specifies an editable property on an actor </Summary>
[AttributeUsage(AttributeTargets.Property)]
public class EditablePropertyAttribute : Attribute {
	public EditablePropertyAttribute() {}
} 
