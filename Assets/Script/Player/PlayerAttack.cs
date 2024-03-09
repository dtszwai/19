using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Shmup
{
  public class PlayerAttack : MonoBehaviour
  {
    public GameObject projectile;
    public int attackPerSecond = 3;
    private int currentLevel = 0;
    [SerializeField] int[] scoreMilestones = { 10, 30, 50, 100, 200, 400, 600, 800 };

    void Start()
    {
      InvokeRepeating(nameof(Attack), 1f, 1f / attackPerSecond);
    }
    void Update()
    {
      if (currentLevel < scoreMilestones.Length && GameController.instance.GetScore() >= scoreMilestones[currentLevel])
      {
        LevelUp();
      }
    }

    void Attack()
    {
      if (!GameController.instance.IsGameOver())
      {
        int numberOfProjectiles = projectile.GetComponent<Projectile>().projectileNumber[currentLevel];
        for (int i = 0; i < numberOfProjectiles; i++)
        {
          // Calculate the starting position of the projectile
          Vector3 startPosition = transform.position + new Vector3((i - (numberOfProjectiles - 1) / 2.0f) * 0.5f, 0, 0);
          // Instantiate the projectile and set its direction
          GameObject projectileInstance = Instantiate(projectile, startPosition, Quaternion.identity);
          projectileInstance.GetComponent<Projectile>().SetParent(this.transform);
          projectileInstance.GetComponent<Projectile>().direction = new Vector2(0, 1);
        }
      }
    }


    void LevelUp()
    {
      if (currentLevel < scoreMilestones.Length - 1)
      {
        currentLevel++;
        Helpers.AddText(transform.position, "LV UP", Color.green);
      }
    }

    public int GetCurrentLevel()
    {
      return currentLevel;
    }
  }
}