using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Physics2DObject
{
    [SerializeField] private PlayerMovement _playerMovement = null;
    [SerializeField] private PlayerJump _playerJump = null;

}
