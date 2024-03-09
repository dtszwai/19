using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
  public class PlayerMovement : MonoBehaviour
  {
    private Rigidbody2D rb;
    private float speed = 5f;
    private bool facingRight = true;
    private float jumpForce = 15f;
    private bool isJumping = false;

    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
      if (Input.GetKey(KeyCode.LeftArrow))
      {
        transform.Translate(speed * Time.deltaTime * Vector3.left);
        if (facingRight) { Flip(); }
      }
      if (Input.GetKey(KeyCode.RightArrow))
      {
        transform.Translate(speed * Time.deltaTime * Vector3.right);
        if (!facingRight) { Flip(); }
      }
      if (Input.GetKey(KeyCode.UpArrow) && !isJumping)
      {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
      }

      if (rb.velocity.y == 0)
      {
        isJumping = false;
      }
    }

    void Flip()
    {
      facingRight = !facingRight;
      Vector3 theScale = transform.localScale;
      theScale.x *= -1;
      transform.localScale = theScale;
    }
  }
}