using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Monocle;

namespace Celeste.Mod.KyleMod.Entities;

[Tracked]
public class Attractable : Component
{
    public bool IsAffected;

    public bool InDelay;

    public float CurrentDelay;
    
    public float Delay;
    
    public HashSet<Magnetic> Magnets = new();
    
    public Attractable(float delay) : base(true, false)
    {
        Delay = delay;
        CurrentDelay = Delay;
        IsAffected = false;
        InDelay = false;
    }
    
    private bool CheckMagnetCollision(Magnetic magnetic)
    {
        var collider = Entity.Collider;
        var num = !magnetic.Entity.CollideCheck(Entity);
        Entity.Collider = collider;
        return num;
    }
}