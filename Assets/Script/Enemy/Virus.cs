using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class Virus : EnemyBase
    {
        public float minFallSpeed = 1f;
        public float maxFallSpeed = 3f;

        protected override void Start()
        {
            base.Start();

            rb.gravityScale = 0;
            float fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
            float horizontalSpeed = Random.Range(-1f, 1f); // Add horizontal speed
            float rotationSpeed = Random.Range(-150f, 150f); // Add rotation speed
            rb.velocity = new Vector2(horizontalSpeed, -fallSpeed);
            rb.angularVelocity = rotationSpeed; // Set the rotation
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
            if (other.gameObject.tag == "Ground")
            {
                StartCoroutine(DestroyVirus());
            }
        }
    }
}