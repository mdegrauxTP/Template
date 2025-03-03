using Godot;

namespace Template;

public static class Sounds
{
    public static AudioStream GameOver { get; } =
        Load("Game Over/musical-game-over.wav");

    static AudioStream Load(string path) =>
        GD.Load<AudioStream>($"res://Audio/SFX/{path}");
}

