using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float _moveInput;

    private Rigidbody2D _rb;

    private bool _facingRight = true;

    // This is for the double jump mechanic
    private bool _isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    // This is for the wall jump mechanic
    public Transform frontCheck;
    private bool _isWalled;

    public int extraJumps;
    public int extraJumpsValue;


    //Animations
    public Animator animator;
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");


    void Start()
    {
        extraJumps = extraJumpsValue;
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        _isWalled = Physics2D.OverlapCircle(frontCheck.position, 0.5f, whatIsGround);

        _moveInput = Input.GetAxis("Horizontal");

        animator.SetFloat(Speed, Mathf.Abs(_moveInput));

        Debug.Log(_moveInput);
        _rb.velocity = new Vector2(_moveInput * speed, _rb.velocity.y);


        if (_facingRight == false && _moveInput > 0)
        {
            Flip();
        }
        else if (_facingRight && _moveInput < 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        if (_isGrounded)
        {
            extraJumps = extraJumpsValue;
            animator.SetBool(IsGrounded, true);
            animator.SetBool(IsFalling, false);
        }

        if (_isWalled)
        {
            extraJumps = extraJumpsValue;
            _isWalled = false;
        }


        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            //2nd Jump
            _rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            animator.SetBool(IsJumping, false);
            animator.SetBool(IsGrounded, false);
            animator.SetBool(IsFalling, true);
        }

        //Jump from ground
        if (Input.GetButtonDown("Jump") && extraJumps == 0 && _isGrounded)
        {
            _rb.velocity = Vector2.up * jumpForce;
            animator.SetBool(IsJumping, true);
            animator.SetBool(IsFalling, true);
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        var transform1 = transform;
        Vector3 scaler = transform1.localScale;
        scaler.x *= -1;
        transform1.localScale = scaler;
    }
}