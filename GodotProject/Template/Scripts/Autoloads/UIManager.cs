using Godot;
using System.Collections.Generic;

namespace Template;

public partial class UIManager : Node
{
    private static List<Control> rootControls = [];

    public override void _Ready()
	{
        GetRootControlNodes(GetTree().Root);

        // This should not initially be set to DisplayServer.WindowGetSize() because then UI sizes
        // will be inconsistent if the player starts the game in fullscreen vs in windowed mode
        Vector2 referenceWindowSize = new(1280, 720);

        SetRootControlPositions(referenceWindowSize);

        GetTree().Root.GetViewport().SizeChanged += () =>
        {
            SetRootControlPositions(referenceWindowSize);
        };
    }

    private static void SetRootControlPositions(Vector2 initialWindowSize)
    {
        foreach (Control infoPanel in rootControls)
        {
            float scaleFactor = initialWindowSize.X / DisplayServer.WindowGetSize().X;
            Vector2 newScale = Vector2.One * scaleFactor;

            // Calculate the new position and size based on the original position and size
            Vector2 originalPosition = infoPanel.GetRect().Position;
            Vector2 originalSize = infoPanel.GetRect().Size;

            Vector2 newPosition = originalPosition * newScale;
            Vector2 newSize = originalSize * newScale;

            Rect2 rect = infoPanel.GetRect();

            // Apply the new position and size
            rect.Position = newPosition;
            rect.Size = newSize;
        }
    }

    private void GetRootControlNodes(Node node)
    {
        if (node is Control controlNode)
        {
            rootControls.Add(controlNode);
            return;
        }

        foreach (Node child in node.GetChildren())
        {
            GetRootControlNodes(child);
        }
    }
}

