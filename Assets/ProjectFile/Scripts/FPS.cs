using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public static FPS instance;
    [SerializeField] private PlayerController playerController;
    [SerializeField] TextMeshProUGUI CashText;
    public TextMeshProUGUI fpsText; // Assign this in the Inspector
    private float deltaTime = 0.0f;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        // Calculate the time between frames
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        // Calculate FPS
        float fps = 1.0f / deltaTime;
        // Update the TextMeshPro text
        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
        
    }
    public void CashinHand()
    {
      
        CashText.text = playerController.GetMoney().ToString();
    }
}
