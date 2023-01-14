namespace BotwActorTool.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ToolbarItemAttribute : Attribute
    {
        public string Description { get; set; }
        public string Icon { get; set; }
        public int GroupId { get; set; }

        public ToolbarItemAttribute(string description, string icon)
        {
            Description = description;
            Icon = icon;
        }
    }
}
