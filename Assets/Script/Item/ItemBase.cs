using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public abstract class ItemBase : MonoBehaviour
    {
        public float timeBeforeDestroy = 2f;
        public float minFallSpeed = 1f;
        public float maxFallSpeed = 3f;
        protected Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            float fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
            float horizontalSpeed = Random.Range(-1f, 1f); // Add horizontal speed
            float rotationSpeed = Random.Range(-150f, 150f); // Add rotation speed
            rb.velocity = new Vector2(horizontalSpeed, -fallSpeed);
            rb.angularVelocity = rotationSpeed; // Set the rotation
        }

        protected virtual void Effect(Player player)
        {
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Effect(other.gameObject.GetComponent<Player>());
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Ground"))
            {
                rb.velocity = Vector2.zero;
                Destroy(gameObject, timeBeforeDestroy);
            }
        }
    }
}