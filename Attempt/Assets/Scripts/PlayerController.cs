using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Movement Variables
    Rigidbody2D rb;
    public float jumpForce;
    public float speed;

    //Ground check
    public bool isGrounded;

    //Animation variables
    Animator anim;
    public bool moving;
    public bool jumping;


    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;

        //variables to mirror the player
        Vector3 newScale = transform.localScale;
        float currentScale = Mathf.Abs(transform.localScale.x); //take absolute value of the current x scale, this is always positive


        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition.x -= speed;
            newScale.x = -currentScale;
            moving = true;
        }

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition.x += speed;
            newScale.x = currentScale;
            moving = true;
        }

        if (Input.GetKey("w") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumping = true;
        }

        if(Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            moving = false;
        }

        if(Input.GetKeyUp("w"))
        {
            jumping = false;
        }

        anim.SetBool("IsMoving", moving);
        anim.SetBool("IsJumping", jumping);
        transform.position = newPosition;
        transform.localScale = newScale;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            Debug.Log("i hit the ground");
            isGrounded = true;
        }

        if (collision.gameObject.tag.Equals("coin"))
        {
            //score goes up
            gm.score++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag.Equals("Goal"))
        {
            SceneManager.LoadScene(1);
        }

        if (collision.gameObject.tag.Equals("Goal2"))
        {
            SceneManager.LoadScene(3);
        }

        if (collision.gameObject.tag.Equals("Obstacle"))
        {
            SceneManager.LoadScene(3);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            isGrounded = false;
        }
    }
    
}
