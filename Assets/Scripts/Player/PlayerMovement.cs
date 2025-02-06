using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    //private DustEffects dustEffects;
    private Vector2 _moveInput;

    [Header("Movement")]
    [SerializeField] float _moveSpeed;
    [SerializeField] float _acceleration;
    [SerializeField] float _decceleration;
    [SerializeField] float _velPower;

    private bool facingRight = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");

        if ((_moveInput.x > 0.01f && !facingRight) || (_moveInput.x < -0.01f && facingRight))
        {
            //Flip();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float targetSpeed = _moveInput.x * _moveSpeed;

        float speedDif = targetSpeed - _rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _decceleration;
        // Pow =  X^Y
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velPower) * Mathf.Sign(speedDif);
        _rb.AddForce(movement * Vector2.right);

        //_rb.velocity = new Vector2(_xPos * _mSpeed, _rb.velocity.y);
    }

    private void Flip()
    {
        //dustEffects.DustTrail();
        facingRight = !facingRight;
        transform.rotation = /*Quaternion.Euler(transform.rotation.x, -transform.rotation.y, transform.rotation.z);*/Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
}