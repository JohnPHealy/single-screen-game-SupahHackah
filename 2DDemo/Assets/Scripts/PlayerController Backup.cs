using UnityEngine;

public class PlayerControllerBackup : MonoBehaviour
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


    public int extraJumps;
    public int extraJumpsValue;

    void Start()
    {
        extraJumps = extraJumpsValue;
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        _moveInput = Input.GetAxis("Horizontal");
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
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            _rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && _isGrounded)
        {
            _rb.velocity = Vector2.up * jumpForce;
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