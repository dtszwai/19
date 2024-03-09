using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class BigWhiteSpawn : MonoBehaviour
    {
        public GameObject BigWhite;
        [SerializeField] float spawnRate;
        private float timer = 0;
        [SerializeField] GameObject warning;
        private bool isLeft;

        void Update()
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;

                // If there are less than 3 seconds remaining until next spawn
                if ((spawnRate - timer) < 3)
                {
                    // Set the warning's position
                    warning.transform.position = new Vector3(8.32f * (isLeft ? -1 : 1), warning.transform.position.y, warning.transform.position.z);
                    warning.SetActive(true);
                }
                else
                {
                    warning.SetActive(false);
                }
            }
            else
            {
                // Instantiate BigWhite
                GameObject spawnedBigWhite = Instantiate(BigWhite, new Vector3(transform.position.x * (isLeft ? -1 : 1), -2.277397f, 0), transform.rotation);
                spawnedBigWhite.GetComponent<BigWhite>().isLeft = !isLeft;

                timer = 0;
                isLeft = (Random.Range(0, 2) == 0); // Decide for next spawn
            }
        }
    }
}
