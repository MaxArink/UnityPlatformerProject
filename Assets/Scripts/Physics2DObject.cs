using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Physics2DObject : CollisionObject
{
    private Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();

    public Rigidbody2D Rb2D
    {
        get => _rb2D;
    }
}