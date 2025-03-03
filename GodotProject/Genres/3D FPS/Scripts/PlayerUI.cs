using Godot;
using GodotUtils;

namespace Template.FPS3D;

public partial class Player : CharacterBody3D
{
    [Export] OptionsManager options;

    Vector3 cameraTarget;
    Vector2 mouseInput;
    float mouseSensitivity;

    void OnReadyUI()
    {
        mouseSensitivity = options.Options.MouseSensitivity * 0.0001f;

        UIOptionsGameplay gameplay = GetNode<UIPopupMenu>("%PopupMenu")
            .Options.GetNode<UIOptionsGameplay>("%Gameplay");

        gameplay.OnMouseSensitivityChanged += value =>
        {
            mouseSensitivity = value * 0.0001f;
        };
    }

    void OnPhysicsProcessUI()
    {
        if (Input.IsActionJustPressed("next_held_item"))
        {
            animTree.SetCondition("holster", true);
        }

        if (Input.IsActionJustPressed("previous_held_item"))
        {
            //animTree.SetCondition("holster", true);
        }
    }

    void OnInputUI(InputEvent @event)
    {
        if (Input.MouseMode != Input.MouseModeEnum.Captured)
            return;

        if (@event is InputEventMouseMotion motion)
        {
            mouseInput = motion.Relative;

            cameraTarget += new Vector3(
                -motion.Relative.Y * mouseSensitivity,
                -motion.Relative.X * mouseSensitivity, 0);

            // Prevent camera from looking too far up or down
            Vector3 rotDeg = cameraTarget;
            rotDeg.X = Mathf.Clamp(rotDeg.X, -89f.ToRadians(), 89f.ToRadians());
            cameraTarget = rotDeg;
        }
    }
}

