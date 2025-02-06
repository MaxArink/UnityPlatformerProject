using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] Transform _groundCheckPoint;
    [SerializeField] Vector2 _groundCheckSize;
    [SerializeField] LayerMask _whatIsGround;

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _whatIsGround);
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheckPoint == null)
            return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
    }
}
