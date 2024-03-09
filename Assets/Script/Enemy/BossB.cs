using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class BossB : EnemyBase
    {
        public float minFallSpeed = 1f;
        public float maxFallSpeed = 3f;
        public GameObject virusPrefab;
        public int virusCount = 3; // The number of viruses to spawn
        public float throwSpeed = 5f; // The speed at which the viruses are thrown
        private bool isThrowing = true;

        protected override void Start()
        {
            base.Start();
            rb.gravityScale = 0;
            float fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
            float horizontalSpeed = Random.Range(-1f, 1f); // Add horizontal speed
            rb.velocity = new Vector2(horizontalSpeed, -fallSpeed);
        }

        protected override void Update()
        {
            if (isThrowing)
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

        public override void TakeDamage(int damage = 1)
        {
            base.TakeDamage(damage);
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                rb.velocity = Vector2.zero;
            }

            if (collision.gameObject.tag == "Ground")
            {
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                isThrowing = false;
                StartCoroutine(DestroyVirus(2f));
            }
        }
    }
}