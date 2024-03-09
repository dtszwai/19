using System.Collections;
using UnityEngine;

namespace Shmup
{
    public class BigWhite : MonoBehaviour
    {
        private float moveSpeed = 1.5f;
        public bool isLeft = true;
        private float bounceForce = 20f;
        private Vector3 direction;
        void Start()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            rb.gravityScale = 0;
            Destroy(gameObject, 25);
            direction = isLeft ? Vector3.left : Vector3.right;
            Vector3 scale = transform.localScale;
            scale.x *= !isLeft ? -1 : 1;
            transform.localScale = scale;

        }
        void Update()
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (other.gameObject.GetComponent<Player>().IsInvincible())
                {
                    return;
                }
                Vector2 bounceDirection = other.transform.position - transform.position;
                bounceDirection = bounceDirection.normalized;
                Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
                StartCoroutine(ApplyBounceForce(playerRb, bounceDirection, bounceForce, 0.1f));
                StartCoroutine(ResetPlayerVelocity(playerRb, 0.5f)); // Add this line
                other.gameObject.GetComponent<Player>().TakeDamage();
            }
        }

        IEnumerator ApplyBounceForce(Rigidbody2D rb, Vector2 direction, float force, float duration)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                rb.AddForce(direction * (force / duration) * Time.deltaTime, ForceMode2D.Impulse);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        IEnumerator ResetPlayerVelocity(Rigidbody2D playerRb, float delay)
        {
            yield return new WaitForSeconds(delay);
            playerRb.velocity = Vector2.zero;
        }
    }
}