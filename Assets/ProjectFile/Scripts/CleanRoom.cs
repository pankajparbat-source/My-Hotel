using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanRoom : MonoBehaviour
{
    [SerializeField] private Image room1Canvas;
    [SerializeField] private Image room2Canvas;
    [SerializeField] private Image room3Canvas;

    private Coroutine fillCoroutine;
    private bool isFilling = false;

    private void Start()
    {
        // Set the Image color to black for the filled part
        room1Canvas.color = Color.black;

        // Ensure the background of the Image sprite is white
        room1Canvas.sprite = GenerateWhiteSprite(); // Optional: If no suitable sprite is available
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isFilling)
        {
            isFilling = true;
            fillCoroutine = StartCoroutine(FillImageCircularly());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isFilling)
        {
            isFilling = false;
            StopCoroutine(fillCoroutine);
            room1Canvas.fillAmount = 0f; // Reset the fill amount when the player leaves
        }
    }

    private IEnumerator FillImageCircularly()
    {
        float duration = 1.0f; // Total fill time
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            room1Canvas.fillAmount = Mathf.Clamp01(elapsedTime / duration);
            yield return null;

            if (!isFilling) yield break;
        }

        room1Canvas.fillAmount = 1.0f; // Ensure it's fully filled at the end
    }

    private Sprite GenerateWhiteSprite()
    {
        Texture2D whiteTexture = new Texture2D(1, 1);
        whiteTexture.SetPixel(0, 0, Color.white);
        whiteTexture.Apply();
        return Sprite.Create(whiteTexture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
    }
}
