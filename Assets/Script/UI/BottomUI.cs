using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shmup
{
    public class BottomUI : MonoBehaviour
    {
        public GameObject LifePrefab;
        public GameObject EmptyLifePrefab;
        public GameObject ScoreTextPrefab;
        [SerializeField] private GameObject NameBox;
        public GameObject LifeBox;
        public GameObject TimerBox;
        private Player player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            NameBox.GetComponent<TextMeshProUGUI>().text = Data.instance?.GetPlayerName() ?? "Player";
        }
        void Update()
        {
            foreach (Transform child in LifeBox.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < player.GetHealth(); i++)
            {
                GameObject LifeInstance = Instantiate(LifePrefab, LifeBox.transform);
            }

            for (int i = 0; i < player.GetMaxHealth() - player.GetHealth(); i++)
            {
                GameObject EmptyLifeInstance = Instantiate(EmptyLifePrefab, LifeBox.transform);
            }

            ScoreTextPrefab.GetComponent<TextMeshProUGUI>().text = GameController.instance.GetScore().ToString();
            TimerBox.GetComponent<TextMeshProUGUI>().text = FormatTime(GameController.instance.GetTime());
        }

        string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}