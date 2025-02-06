using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 10f;
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

    private void Update()
    {
        if (groundCheck.IsGrounded())
        {
            _currentJumps = _extraJump;
            _coyoteTimeCounter = _coyoteTime;
            //_currentJumpForce = _jumpHeight;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }
        else
        {
            //print(_jumpBufferCounter);
            _jumpBufferCounter -= Time.deltaTime;
        }

        if (/*Input.GetButtonDown("Jump") && */_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
        {
            print("big jump");
            _currentJumpForce = Jump(_jumpHeight);
        }

        if (!groundCheck.IsGrounded() && _currentJumps > 0 && Input.GetButtonDown("Jump") && !(_coyoteTimeCounter > 0f))
        {
            print("small jump");
            _currentJumps--;
            _currentJumpForce = Jump(_jumpHeight * 0.65f);
        }

        if (_jumping && !groundCheck.IsGrounded())
        {
            //float velocity = Mathf.SmoothDamp()
            //rb.velocity = new Vector2(rb.velocity.x, iets);
            //iets -= 0.1f;
            //rb.velocity = new Vector2(rb.velocity.x, _currentJumpForce);
            //rb.AddForce(Vector2.up * _currentJumpForce, ForceMode2D.Impulse);
            rb.AddForce(new Vector2(0f, _currentJumpForce), ForceMode2D.Force);
            //rb.Add
            _jumpTime += Time.deltaTime;

            //if (Input.GetButtonUp("Jump"))
            //{
            //    _jumpCancelled = true;
            //}

            //if (_jumpTime > _buttonTime)
            //{
            //    _jumping = false;
            //}
        }

        if (Input.GetButtonUp("Jump") || _jumpTime > _buttonTime)
        {
            _jumping = false;

        }

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

    private float Jump(float pJumpHeight)
    {
        print(pJumpHeight);
        rb.gravityScale = _gravityScale;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        float jumpForce = Mathf.Sqrt(pJumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _jumping = true;
        //_jumpCancelled = false;
        _jumpTime = 0f;
        _jumpBufferCounter = 0f;
        return jumpForce;
    }

    //private GroundCheck groundCheck;
    //private DustEffects dustEffects;

    //[Header("Jumping")]
    //[SerializeField] float _jumpForce;
    //[SerializeField] float _jumpTime;
    //[SerializeField] float _fallMultiplier;
    //[SerializeField] float _jumpMultiplier;

    ////[Range(0.0f, 1.0f)] [SerializeField] float _jumpCutMultiplier;
    //[Space(10f)]
    //[SerializeField] float _coyoteTime;
    //private float _coyoteTimeCounter;

    //[SerializeField] float _jumpBufferTime;
    //private float _jumpBufferCounter;

    ////[Space(10f)]
    ////[SerializeField] float _baseGravity = 2f;
    ////[SerializeField] float _maxFallSpeed = 18f;
    ////[SerializeField] float _fallGravityMultiplier = 2f;

    //private Vector2 _velGravity;

    //private float _jumpCounter;

    //private bool _isJumping = false;
    //private bool _isFalling = false;

    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    groundCheck = GetComponent<GroundCheck>();
    //    dustEffects = GetComponent<DustEffects>();
    //}

    //private void Start()
    //{
    //    //_velGravity = new Vector2(0, -Physics2D.gravity.y);
    //}

    //private void Update()
    //{
    //    //if (groundCheck.IsGrounded() && Input.GetAxis("Vertical") < 0)
    //    //    return;



    //    if (groundCheck.IsGrounded())
    //    {
    //        _coyoteTimeCounter = _coyoteTime;
    //        _isJumping = false;
    //        _isFalling = false;
    //    }
    //    else
    //    {
    //        _coyoteTimeCounter -= Time.deltaTime;
    //        _isJumping = true;
    //    }
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        dustEffects.DustTrail();
    //        _jumpBufferCounter = _jumpBufferTime;
    //    }
    //    else
    //    {
    //        _jumpBufferCounter -= Time.deltaTime;
    //    }

    //    if (Input.GetButtonDown("Jump") && groundCheck.IsGrounded())
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
    //        _isJumping = true;
    //        _jumpCounter = 0;
    //    }

    //    if (rb.velocity.y > 0 && _isJumping)
    //    {
    //        _jumpCounter += Time.deltaTime;

    //        if (_jumpCounter > _jumpTime && groundCheck.IsGrounded())
    //            _isJumping = false;

    //        float t = _jumpCounter / _jumpTime;
    //        float currentJumpM = _jumpMultiplier;

    //        if (t > 0.5f)
    //        {
    //            currentJumpM = _jumpMultiplier * (1f - t);
    //        }

    //        rb.velocity += _velGravity * currentJumpM * Time.deltaTime;
    //    }

    //    if (_isJumping && _jumpCounter >= _jumpTime)
    //    {
    //        _isFalling = rb.velocity.y < 0 ? true : false;
    //        if (!_isFalling)
    //        {
    //            //print("Stop");
    //            print("probleem?");
    //            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
    //        }
    //    }

    //    if (Input.GetButtonUp("Jump"))
    //    {
    //        //_isJumping = false;
    //        _jumpCounter = 0;

    //        if (rb.velocity.y < 0.5f)
    //        {
    //            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
    //        }
    //    }

    //    if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
    //    {
    //        //_rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    //        rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
    //        _jumpBufferCounter = 0f;
    //    }

    //    if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
    //    {
    //        //rb.AddForce(Vector2.down * _jumpForce, ForceMode2D.Impulse);
    //        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
    //        _coyoteTimeCounter = 0f;
    //    }

    //    if (rb.velocity.y < 0.9f && !groundCheck.IsGrounded() && _coyoteTimeCounter < 0f)
    //    {

    //        rb.velocity -= _velGravity * _fallMultiplier * Time.deltaTime;
    //    }
    //}
}
