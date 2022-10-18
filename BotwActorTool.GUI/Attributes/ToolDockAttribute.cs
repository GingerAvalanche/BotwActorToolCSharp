#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System;

namespace BotwActorTool.GUI.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ToolDockAttribute : Attribute
    {
        public string Title { get; set; }

        public ToolDockAttribute() { }
        public ToolDockAttribute(string title) => Title = title;
    }
}
