using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBall : MonoBehaviour
{
    public static int score;
    public float tileJump;
    public float DeviceJump;
    public float moveSpeed;
    public float maxSpeed;
    private Rigidbody2D rb;
    void Start()
    {
        score = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * moveSpeed * Time.deltaTime * 60, ForceMode2D.Force);       
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * moveSpeed * Time.deltaTime * 60, ForceMode2D.Force);
        }
        if (rb.velocity.x < maxSpeed * (-1))
            rb.velocity = new Vector2(maxSpeed * (-1), rb.velocity.y);
        if (rb.velocity.x > maxSpeed)
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
    }


    public void Jump(float jumpForce)
    {
        rb.AddForce(Vector2.up * jumpForce * Time.deltaTime * 60, ForceMode2D.Impulse);
        Debug.Log("jump");
    }
   
    private void OnCollisionStay2D(Collision2D collision)
       
    {
        
        if (collision.transform.tag == "Tile" && rb.velocity.y == 0 )
        {
            Jump(tileJump);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "SawBlade")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.transform.tag == "JumpDevice")
        {
            Jump(DeviceJump);
            collision.gameObject.GetComponent<Animator>().SetTrigger("Jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Strawberry")
        {
            score += 1;
            Debug.Log(score);
            collision.gameObject.SetActive(false);
        }
    }


}
