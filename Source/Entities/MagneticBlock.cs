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

    private Hitbox magneticField;

    public MagneticBlock(EntityData data, Vector2 offset) :
        base(data.Position + offset, data.Width, data.Height, false)
    {
        Add(GFX.SpriteBank.Create("magneticBlock"));
        Collider = new Hitbox(data.Width, data.Height, -8, -8);
        var field = new Circle(data.Float("magneticFieldRadius"));
        Add(Magnet = new Magnetic(field));
        // Collidable = true;
    }

    public override void Added(Scene scene)
    {
        base.Added(scene);
        level = SceneAs<Level>();
    }

    public override void Update()
    {
        base.Update();
        Magnet.CheckAgainstColliders();
    }
}