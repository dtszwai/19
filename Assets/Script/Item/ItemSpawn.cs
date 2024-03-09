using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class ItemSpawn : MonoBehaviour
    {
        public GameObject[] items;
        public float spawnTime = 10f;
        private float timer = 0;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (timer > spawnTime)
            {
                timer = 0;
                int index = Random.Range(0, items.Length);
                float randomX = Random.Range(-8.5f, 8.5f);
                Vector3 randomPosition = new Vector3(randomX, 6, 0);
                Instantiate(items[index], randomPosition, Quaternion.identity);
            }
        }
    }
}