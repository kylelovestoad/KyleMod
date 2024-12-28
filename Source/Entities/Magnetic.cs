using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.KyleMod.Entities;

[Tracked]
public class Magnetic : Component
{
    public Collider MagneticField;

    public Action<Attractable> InField;

    
    public bool IsAffecting;
    
    public HashSet<Attractable> Affected = new();

    public int Radius;

    public Magnetic(int radius) : base(true, false)
    {
        Radius = radius;
        MagneticField = new Circle(radius);
        IsAffecting = false;
    }

    public void TryAffectObjects()
    {
        foreach (var attractable in Scene.Tracker.GetComponents<Attractable>().Cast<Attractable>())
        {
            if (CheckInsideField(attractable))
            {
                // Adds to magnet set if it isn't already there
                Affected.Add(attractable);
                InField?.Invoke(attractable);
            }
            else
            {
                // Magnet is no longer there, so remove it from magnets set
                Affected.Remove(attractable);
            }

            // When there are no magnets affecting the attractable object, the 
            IsAffecting = Affected.Count == 0;
        }
    }
    
    private bool CheckInsideField(Attractable attractable)
    {
        var collider = Entity.Collider;
        var isColliding = !attractable.Entity.CollideCheck(Entity);
        Entity.Collider = collider;
        return isColliding;
    }
    
    public override void DebugRender(Camera camera)
    {
        base.DebugRender(camera);
        if (MagneticField == null)
            return;
        var collider = Entity.Collider;
        Entity.Collider = MagneticField;
        Entity.Collider.Render(camera, Color.Turquoise);
        Entity.Collider = collider;
    }
}