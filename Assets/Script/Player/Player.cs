using System.Collections;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace Shmup
{
  public class Player : MonoBehaviour
  {
    private SpriteRenderer spriteRenderer;
    int health = 3;
    readonly int maxHealth = 3;
    private bool isInvincible = false;
    private bool isProtected = false;
    public Sprite protectedSprite;
    private Sprite defaultSprite;
    private DamageFlash damageFlash;

    void Start()
    {
      spriteRenderer = GetComponent<SpriteRenderer>();
      defaultSprite = spriteRenderer.sprite;
      damageFlash = GetComponent<DamageFlash>();
    }

    void Update()
    {
      if (isInvincible)
      {
        // StartCoroutine(Flash());
        gameObject.layer = LayerMask.NameToLayer("Invincible");
      }
      else
      {
        gameObject.layer = LayerMask.NameToLayer("Player");
      }
    }

    public void SetInvincible(int duration = 3)
    {
      isInvincible = true;
      Invoke(nameof(ResetInvincibility), duration);
    }

    void ResetInvincibility()
    {
      isInvincible = false;
    }

    public void Heal()
    {
      if (health < maxHealth)
      {
        health += 1;
        Helpers.AddText(transform.position, "+1", Color.green);
      }
      else
      {
        Helpers.AddText(transform.position, "MAX HP", Color.green);
      }
    }

    public void TakeDamage()
    {
      if (isInvincible)
      {
        return;
      }

      health -= 1;
      damageFlash.Flash(Color.red);
      Helpers.AddText(transform.position, "-1", Color.red);
      SetInvincible();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
      if (isInvincible)
      {
        return;
      }

      if (other.gameObject.CompareTag("Enemy"))
      {
        if (isProtected)
        {
          RemoveProtected();
        }
        else
        {
          GetComponent<Player>().TakeDamage();
        }
      }
    }

    public int GetHealth() => health;
    public int GetMaxHealth() => maxHealth;

    public void SetProtected()
    {
      isProtected = true;
      spriteRenderer.sprite = protectedSprite;
      Helpers.AddText(transform.position, "MASK UP", Color.green);
    }

    public void RemoveProtected()
    {
      isProtected = false;
      spriteRenderer.sprite = defaultSprite;
      Helpers.AddText(transform.position, "PROTECTED", Color.red);
    }

    public bool IsInvincible() => isInvincible;
  }
}