using CSharpUtils;
using Godot;

namespace Template.Platformer2D;

public partial class Player : Entity
{
    float maxSpeed = 500;
    float acceleration = 40;
    float friction = 20;
    float gravity = 20;

    public override void Update()
    {
        Vector2 vel = Velocity;
        float horzDir = Input.GetAxis("move_left", "move_right");

        // Horizontal movement
        vel.X += horzDir * acceleration;
        vel.X = Utils.ClampAndDampen(vel.X, friction, maxSpeed);

        // Gravity
        vel.Y += gravity;

        Velocity = vel;
    }

    protected override State InitialState() => Idle();
}

