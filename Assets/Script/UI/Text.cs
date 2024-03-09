using System.Collections;
using TMPro;
using UnityEngine;

namespace Shmup
{
  public class Text : MonoBehaviour
  {
    float fadeOutTime = 1f;
    float moveSpeed = 20f;
    float moveTime = 0.5f;
    private TextMeshProUGUI textComponent;

    void Start()
    {
      textComponent = GetComponent<TextMeshProUGUI>();
      textComponent.fontSharedMaterial.EnableKeyword("OUTLINE_ON");

      textComponent.outlineColor = new Color32(117, 3, 42, 255);
      textComponent.outlineWidth = 0.2f;

      StartCoroutine(MoveUpOverTime());
    }
    void Update()
    {
    }

    IEnumerator FadeOutAndDestroy()
    {
      float elapsedTime = 0;

      Color startColor = textComponent.color;
      Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

      while (elapsedTime < fadeOutTime)
      {
        textComponent.color = Color.Lerp(startColor, endColor, (elapsedTime / fadeOutTime));
        elapsedTime += Time.deltaTime;
        yield return null;
      }

      textComponent.color = endColor;

      Destroy(gameObject);
    }

    IEnumerator MoveUpOverTime()
    {
      float elapsedTime = 0;

      Vector2 startPosition = GetComponent<RectTransform>().anchoredPosition;
      Vector2 endPosition = startPosition + new Vector2(0, moveSpeed);

      while (elapsedTime < moveTime)
      {
        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / moveTime);
        elapsedTime += Time.deltaTime;
        yield return null;
      }

      GetComponent<RectTransform>().anchoredPosition = endPosition;

      StartCoroutine(FadeOutAndDestroy());
    }
  }
}