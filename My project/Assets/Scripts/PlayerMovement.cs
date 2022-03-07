using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private Animator anim;

   
    private void Awake()
    {
        //Grab references for rigidbody and animator from object//
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Checks every frame
    private void Update() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        // moving left and right//
        body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);

        //Flips the character depending on the direction you are moving.//
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        
        //jump code//
        if(Input.GetKey(KeyCode.Space))
            body.velocity = new Vector2(body.velocity.x, speed);

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
    }
}
