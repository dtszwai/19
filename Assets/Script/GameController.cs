using UnityEngine;
using TMPro;
using Eflatun.SceneReference;
using Utilities;
using System.Collections;

namespace Shmup
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] SceneReference level1Scene;
        [SerializeField] SceneReference mainMenuScene;
        [SerializeField] GameObject gameOverUI;
        public static GameController instance { get; private set; }
        Player player;
        int score;
        float timer = 0;
        bool dataSent = false;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        void Update()
        {
            if (IsGameOver())
            {
                Time.timeScale = 0;
                if (gameOverUI != null && !gameOverUI.activeSelf)
                {

                    GameObject[] textObjects = GameObject.FindGameObjectsWithTag("Text");
                    foreach (GameObject textObject in textObjects)
                    {
                        Destroy(textObject);
                    }
                    gameOverUI.SetActive(true);
                }

                if (!dataSent)
                {
                    dataSent = true;
                    StartCoroutine(Helpers.PostData(Data.instance.GetPlayerName(), score, timer));
                }
            }

            timer += Time.deltaTime;
        }

        // IEnumerator SendDataCoroutine(string name, int score, float time)
        // {
        //     var task = Helpers.FetchData(name, score, time);
        //     yield return new WaitUntil(() => task.IsCompleted);
        // }


        public void AddScore(int score) => this.score += score;
        public int GetScore() => score;
        public bool IsGameOver() => player.GetHealth() <= 0;
        public float GetTime() => timer;
    }
}