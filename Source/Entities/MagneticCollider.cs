using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Monocle;

namespace Celeste.Mod.KyleMod.Entities;

[Tracked]
public class MagneticCollider : Component
{
    public bool IsAffected => Magnets.Count > 0;

    public readonly HashSet<Magnetic> Magnets = new();
    
    public readonly Action<Magnetic> OnCollide;
    
    public MagneticCollider(Action<Magnetic> onCollide) : base(true, false)
    {
        OnCollide = onCollide;
    }
    
    public bool CheckInField(Magnetic magnetic)
    {
        var collider = Entity.Collider; 
        if (magnetic.MagneticField != null)
            Entity.Collider = magnetic.MagneticField;

        var isColliding = magnetic.Entity.CollideCheck(Entity);
        Entity.Collider = collider;
        return isColliding;
    }
}