using System;
using System.Collections;
using System.Threading;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using On.FMOD;

namespace Celeste.Mod.KyleMod.Entities;

[CustomEntity("KyleMod/AttractedBlock")]
public class AttractedBlock : Solid
{

    public MagneticCollider MagneticCollider;

    public Level level;

    public const float Delay = 3f;
    
    public float CurrDelay;
    
    public bool Moving;

    public float speed;

    public Coroutine MagnetRoutine;

    public AttractedBlock(EntityData data, Vector2 offset)
        : base(data.Position + offset, data.Width, data.Height, false)
    {
        // TODO: read properties from data
        Add(GFX.SpriteBank.Create("attractedBlock"));
        Collider = new Hitbox(data.Width, data.Height, -8, -8);
        Add(MagneticCollider = new MagneticCollider(OnMagnet));
        CurrDelay = Delay;
        Speed = Vector2.Zero;
        Add(MagnetRoutine = new Coroutine());
    }
    
    public override void Added(Scene scene)
    {
        base.Added(scene);
        level = SceneAs<Level>();
    }

    public IEnumerator MagnetSequence(Magnetic magnetic)
    {
        while (CurrDelay > 0.0)
        {
            CurrDelay -= Engine.DeltaTime;
            yield return null;
        }
        
        Moving = true;
        
        while (true)
        {
            Speed.X = Calc.Approach(Speed.X, 60f, 160f * Engine.DeltaTime);
            Speed.Y = Calc.Approach(Speed.Y, 60f, 160f * Engine.DeltaTime);
            
        }
    }

    public void OnMagnet(Magnetic magnetic)
    {
        
        switch (CurrDelay)
        {
            case Delay:
                StartShaking(Delay);
                Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
                CurrDelay -= Engine.DeltaTime;
                return;
            case > 0 when !Moving:
                CurrDelay -= Engine.DeltaTime;
                return;
            case > 0:
                Moving = true;
                StopShaking();
                break;
        }
        
        var horizontalCollision = false;
        var verticalCollision = false;
        
        var distanceVector = magnetic.Entity.Position - Position;
        var distance = distanceVector.Length();
        var direction = distanceVector.Angle();
        
        
        Engine.Commands.Log(magnetic.Entity.Position);
        Engine.Commands.Log(Position);
        Engine.Commands.Log(distanceVector);
        Engine.Commands.Log(direction);
        Engine.Commands.Log(Speed);
        
        speed = Calc.Approach(speed, 160f, 160f * Engine.DeltaTime);

        MoveHCollideSolids(speed * (float)Math.Cos(direction) * Engine.DeltaTime, true);

        MoveVCollideSolids(speed * (float)Math.Sin(direction) * Engine.DeltaTime, true);

    }
    
    public void ImpactSfx()
    {
        Audio.Play("event:/game/general/fallblock_impact", Center);
    }
}