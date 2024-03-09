using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using Utilities;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;


namespace Shmup
{
  public class MainMenuUI : MonoBehaviour
  {
    [SerializeField] SceneReference startingLevel;
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button changeNameButton;
    [SerializeField] GameObject changeNameUI;
    [SerializeField] Button rankButton;
    [SerializeField] GameObject rankUI;
    [SerializeField] GameObject rankTable;
    [SerializeField] GameObject textPrefab;
    [SerializeField] Button closeRankButton;
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private List<TextMeshProUGUI> playerNameTextFields;
    private List<List<string>> data;

    void Awake()
    {
      playButton.onClick.AddListener(() => Loader.Load(startingLevel));
      quitButton.onClick.AddListener(() => Helpers.QuitGame());
      changeNameButton.onClick.AddListener(() => changeNameUI.SetActive(true));
      rankButton.onClick.AddListener(() => OnShowRank());
      closeRankButton.onClick.AddListener(() => rankUI.SetActive(false));
      playerNameInputField.text = Data.instance.GetPlayerName();
      Time.timeScale = 1f;
    }

    void Update()
    {
      string playerName = Data.instance.GetPlayerName();
      foreach (var text in playerNameTextFields)
      {
        text.text = playerName;
      }
    }


    void OnShowRank()
    {
      rankUI.SetActive(true);
      StartCoroutine(FetchData(() =>
      {
        foreach (Transform child in rankTable.transform)
        {
          foreach (Transform grandChild in child)
          {
            Destroy(grandChild.gameObject);
          }
        }

        // Add the headers
        AddDataToLayout(rankTable.transform.Find("Rank"), "Rank");
        AddDataToLayout(rankTable.transform.Find("Name"), "Name");
        AddDataToLayout(rankTable.transform.Find("Score"), "Score");
        AddDataToLayout(rankTable.transform.Find("Time"), "Time");

        // Add the new data
        for (int i = 0; i < data.Count; i++)
        {
          Transform rankLayoutGroup = rankTable.transform.Find("Rank");
          Transform NameLayoutGroup = rankTable.transform.Find("Name");
          Transform ScoreLayoutGroup = rankTable.transform.Find("Score");
          Transform TimeLayoutGroup = rankTable.transform.Find("Time");

          AddDataToLayout(rankLayoutGroup, (i + 1).ToString());
          AddDataToLayout(NameLayoutGroup, data[i][0]);
          AddDataToLayout(ScoreLayoutGroup, data[i][1]);
          AddDataToLayout(TimeLayoutGroup, data[i][2]);
        }
      }));
    }
    void AddDataToLayout(Transform layoutGroup, string data)
    {
      GameObject newText = Instantiate(textPrefab, layoutGroup);
      TextMeshProUGUI text = newText.GetComponent<TextMeshProUGUI>();
      text.text = data;
    }

    public IEnumerator FetchData(Action callback = null)
    {
      string URL = "https://script.google.com/macros/s/AKfycbxNykjKYd9VQXM91yDGtyAjz0VXrC2CKOyJ0fpkypSvm6z7DVhvM6kYENjpbBJ5LN9U/exec";
      using UnityWebRequest request = UnityWebRequest.Get(URL);
      yield return request.SendWebRequest();

      if (request.result != UnityWebRequest.Result.Success)
      {
        Debug.Log(request.error);
      }
      else
      {
        var stringResult = request.downloadHandler.text;
        data = JsonConvert.DeserializeObject<List<List<string>>>(stringResult);
      }

      callback?.Invoke();
    }

  }
}
