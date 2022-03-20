using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

   
    private void Awake()
    {
        //Grab references for rigidbody and animator from object//
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Checks every frame
    private void Update() 
    {
        horizontalInput = Input.GetAxis("Horizontal");
        // moving left and right//
        body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);

        //Flips the character depending on the direction you are moving.//
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        
        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //wall jump logic
        if(wallJumpCooldown > 0.2f)
        {
                body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);

                if(onWall() && !isGrounded()) 
                {
                    body.gravityScale = 0;
                    body.velocity = Vector2.zero;
                }

                else
                    body.gravityScale = 1.25f;

                 //jump code//
                if(Input.GetKey(KeyCode.Space))
                Jump();
                
        }
        else
            wallJumpCooldown += Time.deltaTime;

    }

    // Jump function
    private void Jump() 
    {
        if(isGrounded())
        {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded()) 
        {
            if(horizontalInput == 0) 
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 12, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else 
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 15, 5);
            wallJumpCooldown = 0;
            // -Mathf returns 1 when facing right and -1 when facing left against wall. This is helpful for the wall climbing action.
        }
    }

    private bool isGrounded()
    {
        //used to tell when object is touching the ground instead of using a booleon. 
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    
    }

    private bool onWall()
    {
        //used to tell when object is touching the ground instead of using a booleon. 
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
