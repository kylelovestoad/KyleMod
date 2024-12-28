using System;
using System.Collections;
using System.Threading;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.KyleMod.Entities;

[CustomEntity("KyleMod/AttractedBlock")]
public class AttractedBlock : Solid {
    
    public bool IsAffected;

    public bool InDelay;

    public float CurrentDelay;
    
    public float Delay;

    public AttractedBlock(EntityData data, Vector2 offset)
        : base(data.Position + offset, data.Width, data.Height, false)
    {
        // TODO: read properties from data
        Add(GFX.SpriteBank.Create("attractedBlock"));
        Collider = new Hitbox(data.Width, data.Height, -8, -8);
    }

    public override void Update()
    {
        base.Update();
        if (InDelay)
        {
            StartShaking();
            return;
        }
        
        StopShaking();
    }
}