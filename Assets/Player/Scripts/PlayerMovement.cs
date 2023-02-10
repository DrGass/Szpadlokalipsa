using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpingForce;
    public LayerMask groundObjects;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public float checkRadius;
    public int maxJumpCount;
    public Transform weapon;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Sprite oldSprite;
    
    private Rigidbody2D rb;
    private float moveDirection;
    private bool facingRight = true;
    private bool isJumping;
    private bool isGrounded;
    private float jumpCount;
    private bool lookingUp = false;
    private bool lookingDown = false;
    private Vector3 initialPos = new Vector3(0.484f,0.037f,0f);
    private bool isCrouching = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Inputs
        ProcessInputs();
        
        // Check if weapon needs adjustment
        ProcessWeapon();
        
        // Animate
        Animate();

    }
    private void FixedUpdate()
    {
        //Check if grounded
        CheckGrounded();

        // Move
        Move();
        
        

    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f,jumpingForce));
            jumpCount--;
        }
        isJumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal"); // Scale of -1 -> 1.
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
        
    }

    void ProcessWeapon()
    {
        if (Input.GetButtonDown("LookUp") && lookingUp == false)
        {
            lookingUp = true;
            WeaponUp();
        }
        else if (Input.GetButtonUp("LookUp"))
        {
            lookingUp = false;
            TurnBackWeapon();
            weapon.transform.Rotate(0f, 0f, -90f);
            lookingUp = false;
        }

       
        if (Input.GetButtonDown("LookDown") && !lookingDown && !isGrounded)
        {
            lookingDown = true;
            WeaponDown();
        }
        else if (lookingDown && isGrounded)
        {
            TurnBackWeapon();
            weapon.transform.Rotate(0f, 0f, 90f);
            lookingDown = false;
        }
        else if (Input.GetButtonUp("LookDown") && lookingDown)
        {
            TurnBackWeapon();
            weapon.transform.Rotate(0f, 0f, 90f);
            lookingDown = false;
        }
        
    }

    private void TurnBackWeapon()
    {
        if (facingRight) weapon.transform.position = transform.position + initialPos;
        else weapon.transform.position = transform.position - initialPos;
    }

    void WeaponDown()
        {
            weapon.transform.Rotate(0f, 0f, -90f);
            var transformPosition = transform.position;
            transformPosition.y -= 1f;
            weapon.transform.position = transformPosition;
        }
   

    void WeaponUp()
        {
            weapon.transform.Rotate(0f, 0f, 90f);
            var transformPosition = transform.position;
            transformPosition.y += 1f;
            weapon.transform.position = transformPosition;
        }
    
}
