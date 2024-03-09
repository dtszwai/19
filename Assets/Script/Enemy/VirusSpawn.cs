using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class VirusData
{
    public GameObject virusPrefab;
    public float spawnTime;
    [System.NonSerialized]
    public float spawnCounter;
}

namespace Shmup
{
    public class VirusSpawn : MonoBehaviour
    {
        public List<VirusData> virusDataList = new List<VirusData>();

        void Start()
        {
            foreach (var virusData in virusDataList)
            {
                virusData.spawnCounter = virusData.spawnTime;
            }
        }

        void Update()
        {
            foreach (var virusData in virusDataList)
            {
                virusData.spawnCounter -= Time.deltaTime;
                if (virusData.spawnCounter <= 0)
                {
                    virusData.spawnCounter = virusData.spawnTime;
                    float randomX = Random.Range(-8.5f, 8.5f);
                    Vector3 randomPosition = new Vector3(randomX, 6, 0);
                    Instantiate(virusData.virusPrefab, randomPosition, Quaternion.identity);
                }
            }
        }
    }
}