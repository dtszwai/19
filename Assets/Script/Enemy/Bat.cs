using System.Collections;
using UnityEngine;

namespace Shmup
{
  public class Bat : EnemyBase
  {
    public float fallHeight = 5f; // The y position to which the bat will first fall
    public float fallSpeed = 1f; // The speed at which the bat falls
    public float chaseSpeed = 2f; // The speed at which the bat chases the player
    public float minChaseTime = 2f; // The minimum time the bat chases the player before pausing
    public float maxChaseTime = 3.5f; // The maximum time the bat chases the player before pausing
    public float minHoverTime = 2f; // The minimum time the bat hovers before it starts chasing
    public float maxHoverTime = 3.5f; // The maximum time the bat hovers before it starts chasing

    private GameObject player; // Reference to the player
    private bool isHovering = false; // Whether the bat is currently hovering
    private bool isChasing = false; // Whether the bat is currently chasing the player

    protected override void Start()
    {
      base.Start();

      rb.gravityScale = 0;
      player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Update()
    {
      base.Update();

      if (!isHovering && !isChasing)
      {
        if (transform.position.y > fallHeight)
        {
          rb.velocity = new Vector2(0, -fallSpeed);
        }
        else
        {
          // Once the bat has reached the fall height, it starts hovering
          isHovering = true;
          StartCoroutine(Hover());
        }
      }
      else if (isChasing)
      {
        // The bat moves towards the player's x position and continues to fall down
        float direction = player.transform.position.x - transform.position.x;
        rb.velocity = new Vector2(chaseSpeed * Mathf.Sign(direction), -fallSpeed);
      }
    }

    IEnumerator Hover()
    {
      // Stop moving while hovering
      rb.velocity = Vector2.zero;

      // Wait for a random hover time to pass
      yield return new WaitForSeconds(Random.Range(minHoverTime, maxHoverTime));

      // Start chasing the player
      isHovering = false;
      isChasing = true;
      StartCoroutine(Chase());
    }

    IEnumerator Chase()
    {
      // Chase the player for a random chase time
      yield return new WaitForSeconds(Random.Range(minChaseTime, maxChaseTime));

      // Pause and hover
      isChasing = false;
      isHovering = true;
      StartCoroutine(Hover());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
      if (other.gameObject.tag == "Ground")
      {
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        StartCoroutine(DestroyVirus(0.25f));
      }

      if (other.gameObject.tag == "Player")
      {
        Destroy(gameObject);
      }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.tag == "Ground")
      {
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        StartCoroutine(DestroyVirus(0.5f));
      }
    }
  }
}
