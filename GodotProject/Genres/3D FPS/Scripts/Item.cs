using Godot;

namespace Template.FPS3D;

public class Item // An item the player can hold
{
    public Skeleton3D SkeletonRig { get; set; }
    public AnimationPlayer AnimationPlayer { get; set; }

    readonly Node3D parent;

    public Item(Node3D parent)
    {
        this.parent = parent;
        SkeletonRig = parent.GetNode<Skeleton3D>("Armature/Skeleton3D");
        AnimationPlayer = parent.GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void SetVisible(bool v) => parent.Visible = v;
}

