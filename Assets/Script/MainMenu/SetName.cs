using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using TMPro;
using System.Collections.Generic;

namespace Shmup
{
    public class SetName : MonoBehaviour
    {
        [SerializeField] Button updateButton;
        [SerializeField] private TMP_InputField playerNameInputField;

        void Awake()
        {
            updateButton.onClick.AddListener(() => UpdateName());
        }

        void UpdateName()
        {
            Data.instance.SetPlayerName(playerNameInputField.text);
            gameObject.SetActive(false);
        }
    }
}