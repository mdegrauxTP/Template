using Godot;
using GodotUtils;

namespace Template;

[Tool]
public partial class SetupToolScript : Node
{
    [Export] public bool RemoveEmptyFolders
    {
        get => false;
        set
        {
            GDirectories.DeleteEmptyDirectories(ProjectSettings.GlobalizePath("res://"));
            GD.Print("Removed all empty folders from the project");
        }
    }
}

