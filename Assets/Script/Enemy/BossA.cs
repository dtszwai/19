using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
  public class BossA : EnemyBase
  {
    private float targetY = 1.9f;
    public float speed = 3f;
    private bool isMoving = false;
    public GameObject virusPrefab;
    public int virusCount = 6; // The number of viruses to spawn
    public float throwSpeed = 5f; // The speed at which the viruses are thrown


    protected override void Start()
    {
      base.Start();
      rb.gravityScale = 0; // Prevent the boss from falling
    }

    protected override void Update()
    {
      base.Update();
      if (GameController.GetScore() >= 10 && !isMoving)
      {
        rb.gravityScale = 0;
        isMoving = true;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
      }

      if (isMoving && transform.position.y > targetY)
      {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), speed * Time.deltaTime);
      }

    }

    public override void TakeDamage(int damage = 1)
    {
      base.TakeDamage(damage);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("Player"))
      {
        rb.velocity = Vector2.zero;
      }
    }

    protected override void BeforeDestroy()
    {
      for (int i = 0; i < virusCount; i++)
      {
        GameObject virus = Instantiate(virusPrefab, transform.position, Quaternion.identity);

        Vector2 throwDirection = new Vector2((i < virusCount / 2 ? -1 : 1) * throwSpeed, throwSpeed);

        if (virus.TryGetComponent<Rigidbody2D>(out var virusRb))
        {
          virusRb.AddForce(throwDirection, ForceMode2D.Impulse);
        }
      }
    }
  }
}