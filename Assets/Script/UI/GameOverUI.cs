using Eflatun.SceneReference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Shmup
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] SceneReference startingLevel;
        [SerializeField] SceneReference mainMenu;
        [SerializeField] Button restartButton;
        [SerializeField] Button homeButton;
        [SerializeField] GameObject scoreText;
        void Awake()
        {
            restartButton.onClick.AddListener(() => Loader.Restart());
            homeButton.onClick.AddListener(() => Loader.Restart());
            Time.timeScale = 1f;
            scoreText.GetComponent<TextMeshProUGUI>().text = GameController.instance.GetScore().ToString();
        }
    }
}