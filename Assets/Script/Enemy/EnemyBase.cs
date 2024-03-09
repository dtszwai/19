using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilities;

namespace Shmup
{
  public abstract class EnemyBase : MonoBehaviour
  {
    [SerializeField] int maxHealth;
    int health;
    public int score;
    protected Rigidbody2D rb;
    protected GameController GameController;
    public GameObject scoreTextPrefab;
    public float timeBeforeDestroy = 0f;
    protected Vector3 position;

    protected DamageFlash damageFlash;

    protected virtual void Awake() => health = maxHealth;


    protected virtual void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      GameController = GameObject.Find("GameController").GetComponent<GameController>();
      damageFlash = GetComponent<DamageFlash>();
    }


    protected virtual void Update()
    {
      position = transform.position;
      if (transform.position.y < -5f || transform.position.x < -10f || transform.position.x > 10f)
      {
        Destroy(gameObject);
      }
    }

    public virtual void TakeDamage(int damage = 1)
    {
      health -= damage;
      damageFlash.Flash(Color.red);
      if (health <= 0)
      {
        Helpers.AddText(position, "+" + score, Color.white);
        StartCoroutine(DestroyVirus(0));
        GameController.AddScore(score);
      }
    }

    protected virtual IEnumerator DestroyVirus()
    {
      yield return DestroyVirus(timeBeforeDestroy);
    }

    protected virtual IEnumerator DestroyVirus(float timeBeforeDestroy)
    {
      SpriteRenderer sprite = GetComponent<SpriteRenderer>();
      float startTime = Time.time;
      while (Time.time - startTime < timeBeforeDestroy)
      {
        float elapsed = Time.time - startTime;
        sprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, elapsed / timeBeforeDestroy));
        yield return null;
      }
      BeforeDestroy();
      Destroy(gameObject);
    }

    protected virtual void BeforeDestroy()
    {

    }
  }
}