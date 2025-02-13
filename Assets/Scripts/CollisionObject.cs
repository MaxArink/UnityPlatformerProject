using System.Collections.Generic;
using System;
using UnityEngine;

public class CollisionObject : Object
{
    private List<Collider> _collider3D = null;
    private List<Collider2D> _collider2D = null;

    public List<Collider> Colliders3D
    {
        get => _collider3D;
    }

    public List<Collider2D> Colliders2D
    {
        get => _collider2D;
    }

    protected override void LoadObjects()
    {
        try
        {
            if (gameObject.GetComponent<Collider>() != null)
                foreach (Collider col in gameObject.GetComponents<Collider>())
                    _collider3D.Add(col);

            if (gameObject.GetComponent<Collider2D>() != null)
                foreach(Collider2D col in gameObject.GetComponents<Collider2D>())
                    _collider2D.Add(col);
        }
        catch (Exception e)
        { 
            print($"{e} | {gameObject.name}");
        }

        base.LoadObjects();
    }
}