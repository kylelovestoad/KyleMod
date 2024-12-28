using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.KyleMod.Entities;

[CustomEntity("KyleMod/MagneticBlock")]
[Tracked]
public class MagneticBlock : Solid
{
    
    private Magnetic Magnet;

    private bool MagnetActive;

    private Level level;

    private float _speed;

    public MagneticBlock(EntityData data, Vector2 offset) : 
        base(data.Position + offset, data.Width, data.Height, false)
    {
        // TODO: read properties from data
        Add(GFX.SpriteBank.Create("magneticBlock"));
        Add(Magnet = new Magnetic(data.Int("magneticFieldRadius")));
        Magnet.InField = InField;
        Collider = new Hitbox(data.Width, data.Height, -8, -8);
    }
    
    public override void Added(Scene scene)
    {
        base.Added(scene);
        level = SceneAs<Level>();
    }

    public override void Update()
    {
        base.Update();
        Magnet.TryAffectObjects();
    }

    public void InField(Attractable attractable)
    {
        if (attractable.CurrentDelay < 0 && !attractable.IsAffected)
        {
            attractable.CurrentDelay -= Engine.DeltaTime;
            return;
        }

        if (!attractable.IsAffected)
        {
            attractable.CurrentDelay = attractable.Delay;
            attractable.IsAffected = true;
        }

        _speed = Calc.Approach(_speed, 240f, 500f * Engine.DeltaTime);
        if (Top < level.Bounds.Bottom + 32)
        {
            
        }
        else
        {
            RemoveSelf();
            return;
        }
    }
}