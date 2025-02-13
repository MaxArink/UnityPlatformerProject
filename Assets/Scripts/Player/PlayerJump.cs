using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 10f;
    [SerializeField] private float _holdJumpHeight = 10f;
    private float _currentJumpForce;
    //float iets = 5f;

    [SerializeField] private float _buttonTime = 0.2f;
    //[SerializeField] private float _cancelRate = 100f;

    [SerializeField] private float _gravityScale = 5f;
    [SerializeField] private float _fallGravityScale = 15f;

    private Rigidbody2D rb;

    private GroundCheck groundCheck;

    [SerializeField] private int _extraJump = 0;
    private int _currentJumps;

    [SerializeField] private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;
    [SerializeField] private float _jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;

    [SerializeField] private float _yClamp = 30f;

    float _jumpTime;
    bool _jumping;
    //bool _jumpCancelled;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();
    }

    //private void Update()
    //{
    //    if (groundCheck.IsGrounded() && !_jumping)
    //    {
    //        _currentJumps = _extraJump;
    //        _coyoteTimeCounter = _coyoteTime;
    //        //_currentJumpForce = _jumpHeight;
    //    }
    //    else
    //    {
    //        _coyoteTimeCounter -= Time.deltaTime;
    //    }

    //    if (Input.GetButtonDown("Jump")/* && !groundCheck.IsGrounded()*/)
    //    {
    //        _jumpBufferCounter = _jumpBufferTime;
    //    }
    //    else
    //    {
    //        //print(_jumpBufferCounter);
    //        _jumpBufferCounter -= Time.deltaTime;
    //    }

    //    if (/*Input.GetButtonDown("Jump") && */_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
    //    {
    //        _currentJumpForce = Jump(_jumpHeight);
    //    }

    //    if (!groundCheck.IsGrounded() && _currentJumps > 0 && Input.GetButtonDown("Jump"))
    //    {
    //        _currentJumps--;
    //        _currentJumpForce = Jump(_jumpHeight * 0.65f);
    //    }

    //    if (_jumping && !groundCheck.IsGrounded())
    //    {
    //        //float velocity = Mathf.SmoothDamp()
    //        //rb.velocity = new Vector2(rb.velocity.x, iets);
    //        //iets -= 0.1f;
    //        //rb.velocity = new Vector2(rb.velocity.x, _currentJumpForce);
    //        //rb.AddForce(Vector2.up * _currentJumpForce, ForceMode2D.Impulse);
    //        rb.AddForce(new Vector2(0f, _currentJumpForce), ForceMode2D.Force);
    //        //rb.Add
    //        _jumpTime += Time.deltaTime;

    //        //if (Input.GetButtonUp("Jump"))
    //        //{
    //        //    _jumpCancelled = true;
    //        //}

    //        //if (_jumpTime > _buttonTime)
    //        //{
    //        //    _jumping = false;
    //        //}
    //    }

    //    if (Input.GetButtonUp("Jump") || _jumpTime > _buttonTime)
    //    {
    //        _jumping = false;
    //    }

    //    if (rb.velocity.y >= 0)
    //    {
    //        rb.gravityScale = _gravityScale;
    //    }
    //    else if (rb.velocity.y < 0)
    //    {
    //        rb.gravityScale = _fallGravityScale;
    //    }
    //}
    private void Update()
    {
        // Reset coyote time en jumps als je op de grond staat
        if (groundCheck.IsGrounded() && !_jumping)
        {
            _currentJumps = _extraJump;
            _coyoteTimeCounter = _coyoteTime;
            _jumping = false;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        // Jump buffering: Bewaart input alleen als de knop nog ingedrukt is
        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            _jumpBufferCounter = 0f;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        // **Springen als coyote time en jump buffer geldig zijn EN de knop nog ingedrukt is**
        if (_jumpBufferCounter > 0f && _coyoteTimeCounter > 0f && !_jumping && Input.GetButton("Jump"))
        {
            _jumpBufferCounter = 0f;
            _coyoteTimeCounter = 0f;
            Jump(_jumpHeight);
        }

        // **Dubbele sprong (alleen als de knop nog ingedrukt is!)**
        if (!groundCheck.IsGrounded() && _currentJumps > 0 && Input.GetButtonDown("Jump"))
        {
            _currentJumps--;
            Jump(_jumpHeight * 0.65f);
        }

        // **Hold Jump zonder Lerp (natuurlijker effect)**
        if (_jumping && Input.GetButton("Jump") && _jumpTime < _buttonTime)
        {
            rb.AddForce(Vector2.up * _holdJumpHeight, ForceMode2D.Force);
            _jumpTime += Time.deltaTime;
        }

        // **Stop Jump als de knop losgelaten wordt of de tijd om is**
        if (Input.GetButtonUp("Jump") || _jumpTime >= _buttonTime)
        {
            _jumping = false;
        }

        // **Verander gravity afhankelijk van of je stijgt of daalt**
        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = _gravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = _fallGravityScale;
        }
    }





    private void FixedUpdate()
    {
        //if (_jumpCancelled && _jumping && rb.velocity.y > 0)
        //{
        //    print("call");
        //    rb.AddForce(Vector2.down * _cancelRate);
        //}

        //if (rb.velocity.y <= -_yClamp)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, -_yClamp);
        //}
        //else if (rb.velocity.y >= _yClamp)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, _yClamp);
        //}
    }

    //private float Jump(float pJumpHeight)
    //{

    //    if (_jumping) 
    //        return 0f;

    //    _jumping = true;
    //    rb.gravityScale = _gravityScale;
    //    rb.velocity = new Vector2(rb.velocity.x, 0f);

    //    float jumpForce = Mathf.Sqrt(pJumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
    //    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    //    //_jumpCancelled = false;
    //    _jumpTime = 0f;
    //    _jumpBufferCounter = 0f;

    //    return jumpForce;
    //}

    private void Jump(float pJumpHeight)
    {
        _jumping = true;

        rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset verticale snelheid

        float jumpForce = Mathf.Sqrt(Mathf.Abs(pJumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale))) * rb.mass;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        _jumpTime = 0f;  // Reset jump timer
    }
}
