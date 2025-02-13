using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private GameObject _gameObject;

    void Awake() 
    {
        LoadObjects();
    }
    protected virtual void Start() { }
    protected virtual void LoadObjects() 
    { 
        
    }
    //protected void FixedUpdate() { }
    //protected void Update() { }
    //protected void LateUpdate() { }
}