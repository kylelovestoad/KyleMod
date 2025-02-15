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
    public readonly Collider MagneticField;
    
    public Magnetic(Collider magneticField = null) : base(true, true)
    {
        MagneticField = magneticField;
    }

    public void CheckAgainstColliders()
    {
        foreach (var component in Scene.Tracker.GetComponents<MagneticCollider>().Cast<MagneticCollider>())
        {
            // When it is colliding with this magnetic field, then add this to the object's magnet set
            if (component.CheckInField(this))
            {
                component.Magnets.Add(this);
                component.OnCollide(this);
            }
            else
            {
                // Attempt to remove the magnet when not colliding if it is in the magnet set
                component.Magnets.Remove(this);
            }
        }
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