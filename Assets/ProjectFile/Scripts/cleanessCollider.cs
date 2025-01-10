using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cleanessCollider : MonoBehaviour
{
    [SerializeField] private Collider cleanerCollider;
    [SerializeField] private Image image; // Assign a circular UI Image in the Inspector
    [SerializeField] private Color startColor = Color.red; // Color at the beginning
    [SerializeField] private Color endColor = Color.black; // Color when the circle is full
    private bool isWaiting = false;
    private float remainingTime = 4f; // Total time required
    private float totalTime = 4f; // Total time for resetting
    private Coroutine timerCoroutine;
    public bool iscolled = false;

    private void Awake()
    {
        image.enabled = false;
        cleanerCollider = GetComponent<Collider>();
        if (image != null)
        {
            image.type = Image.Type.Filled;
            image.fillMethod = Image.FillMethod.Radial360;
            image.fillAmount = 0f; // Start with an empty circle
            image.color = startColor; // Set the initial color
        }
    }

    public void EnableCollider()
    {
        cleanerCollider.enabled = true;
        image.enabled = true;
    }
    public Transform PositionOfCollider()
    {
        return gameObject.transform;
    }
    public void DisableCollider()
    {
        cleanerCollider.enabled = false;
        image.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Owner") || other.CompareTag("Cleaner")) && !isWaiting)
        {
            // Start or continue the timer
            timerCoroutine = StartCoroutine(StartTimer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Owner") || other.CompareTag("Cleaner"))
        {
            // Stop the timer and preserve remaining time
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
            isWaiting = false;
            // Reset the circular fill effect and color
            if (image != null)
            {
                image.fillAmount = 0f;
                image.color = startColor; // Reset to initial color
            }
            remainingTime = totalTime;
        }
    }

    private IEnumerator StartTimer()
    {
        isWaiting = true;
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(0.1f); // Update every 0.1 seconds
            remainingTime -= 0.1f;

            // Update the circular fill effect
            if (image != null)
            {
                float progress = 1f - (remainingTime / totalTime);
                image.fillAmount = progress;
                // Interpolate the color based on progress
                image.color = Color.Lerp(startColor, endColor, progress);
            }
           
        }
        // When the timer ends, execute the method
      
        if (image != null)
        {
            image.fillAmount = 1f; // Ensure the circle is completely filled
            image.color = endColor; // Final color
        }
        if (remainingTime <= 0)
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
            image.fillAmount = 0f;
            image.color = startColor; // Reset to initial color
          
           
            isWaiting = false;
            iscolled = true;
            DisableCollider();
        }
    }
}
