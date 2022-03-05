using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    
    // checks every time you launch/start game//
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
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
    }
}
