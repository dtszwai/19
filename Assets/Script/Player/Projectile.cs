using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Shmup
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed;
        //prefab
        [SerializeField] GameObject syringePrefab;
        public Rigidbody2D rb;
        public Vector2 direction;
        [SerializeField] public int[] projectileDamage = { 1, 1, 1, 2, 2, 4, 4, 4 };
        [SerializeField] public int[] projectileNumber = { 1, 2, 3, 2, 3, 2, 3, 3 };
        [SerializeField] public float[] scale = { 0.07615896f, 0.07615896f, 0.07615896f, 0.1f, 0.1f, 0.12f, 0.12f, 0.12f };

        private int damage;
        Transform parent;

        public void SetSpeed(float speed) => this.speed = speed;
        public void SetParent(Transform parent) => this.parent = parent;
        public Action Callback;

        void Start()
        {
            Destroy(gameObject, 5);
        }

        void Update()
        {
            int currentLevel = parent.GetComponent<PlayerAttack>().GetCurrentLevel();
            damage = projectileDamage[currentLevel];
            transform.position += speed * Time.deltaTime * transform.forward;
            rb.velocity = direction * speed;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
                if (other.gameObject.name.Contains("Boss"))
                {
                    Helpers.AddText(transform.position, "+1", Color.white);
                    GameObject.Find("GameController").GetComponent<GameController>().AddScore(1);
                }
                Destroy(gameObject);
            }
        }
    }
}