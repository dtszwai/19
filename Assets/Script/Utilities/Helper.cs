using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.Networking;

namespace Utilities
{
  public class Helpers : MonoBehaviour
  {
    private static GameObject textPrefab;
    private static Canvas canvas;

    static Helpers()
    {
      textPrefab = Resources.Load<GameObject>("Text");
      canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public static void AddText(Vector3 position, string text, Color color)
    {
      position = new Vector3(position.x, position.y + 0.5f, position.z);
      GameObject textObject = Instantiate(textPrefab, canvas.transform, false);
      textObject.GetComponent<TextMeshProUGUI>().text = text;
      textObject.GetComponent<TextMeshProUGUI>().color = color;

      Vector2 viewportPosition = Camera.main.WorldToViewportPoint(position);
      Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

      Vector2 canvasPosition = new Vector2(
          ((viewportPosition.x * canvasSize.x) - (canvasSize.x * 0.5f)),
          ((viewportPosition.y * canvasSize.y) - (canvasSize.y * 0.5f))
      );

      textObject.GetComponent<RectTransform>().anchoredPosition = canvasPosition;
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    // public static async Task SendData(string name, int score, float time)
    // {
    //   using var client = new HttpClient();

    //   int minutes = Mathf.FloorToInt(time / 60F);
    //   int seconds = Mathf.FloorToInt(time - minutes * 60);
    //   string formatTime = string.Format("{0:00}:{1:00}", minutes, seconds);

    //   var data = new Dictionary<string, string>
    //     {
    //         {"Name", name},
    //         {"Score", score.ToString()},
    //         {"Time", formatTime}
    //     };

    //   var content = new FormUrlEncodedContent(data);
    //   string URL = "https://script.google.com/macros/s/AKfycbxNykjKYd9VQXM91yDGtyAjz0VXrC2CKOyJ0fpkypSvm6z7DVhvM6kYENjpbBJ5LN9U/exec";

    //   try
    //   {
    //     var response = await client.PostAsync(URL, content);

    //     if (response.IsSuccessStatusCode)
    //     {
    //       var responseContent = await response.Content.ReadAsStringAsync();
    //       Console.WriteLine("Success: " + responseContent);
    //     }
    //     else
    //     {
    //       Console.WriteLine("Error: " + response.StatusCode);
    //     }
    //   }
    //   catch (Exception ex)
    //   {
    //     Console.WriteLine("Exception: " + ex.Message);
    //   }
    // }

    public static IEnumerator PostData(string name, int score, float time)
    {
      int minutes = Mathf.FloorToInt(time / 60F);
      int seconds = Mathf.FloorToInt(time - minutes * 60);
      string formatTime = string.Format("{0:00}:{1:00}", minutes, seconds);

      WWWForm form = new WWWForm();
      form.AddField("Name", name);
      form.AddField("Score", score.ToString());
      form.AddField("Time", formatTime);

      string URL = "https://script.google.com/macros/s/AKfycbxNykjKYd9VQXM91yDGtyAjz0VXrC2CKOyJ0fpkypSvm6z7DVhvM6kYENjpbBJ5LN9U/exec";
      using UnityWebRequest request = UnityWebRequest.Post(URL, form);
      yield return request.SendWebRequest();
    }

  }
}