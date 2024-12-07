using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem; // Import Input System

public class LookAtLionTrigger : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;  // The main AR camera
    [SerializeField] private GameObject lion;    // The lion GameObject
    [SerializeField] private GameObject circle;  // The circle that appears
    [SerializeField] private TMP_Text feedbackText;  // The UI text for feedback
    [SerializeField] private LayerMask lionLayerMask; // LayerMask for lions
    [SerializeField] private bool isCorrectLion = true; // Toggle for Correct or Incorrect

    private bool isLookingAtLion = false;  // Tracks if the user is looking at the lion

    void Start()
    {
        // Ensure feedback text is hidden at the start
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera is not assigned.");
            return;
        }

        // Perform a raycast from the center of the screen
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, lionLayerMask))
        {
            if (hit.collider.gameObject == lion)
            {
                if (!isLookingAtLion)
                {
                    circle.SetActive(true);  // Show the circle
                    isLookingAtLion = true;
                }

                // Check for tap input using Input System
                if (Mouse.current.leftButton.wasPressedThisFrame) // For desktop testing
                {
                    ShowFeedback();
                }

                // For touch input (iPad)
                if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
                {
                    ShowFeedback();
                }
            }
        }
        else
        {
            if (isLookingAtLion)
            {
                circle.SetActive(false);  // Hide the circle
                isLookingAtLion = false;
            }
        }
    }

    private void ShowFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(true); // Activate feedback text

            // Show Correct or Incorrect based on toggle
            feedbackText.text = isCorrectLion ? "Correct!" : "Incorrect!";
            StartCoroutine(HideFeedbackText());  // Hide it after a short delay
        }
    }

    // Coroutine to hide the feedback text after 2 seconds
    private IEnumerator HideFeedbackText()
    {
        yield return new WaitForSeconds(2.0f);
        feedbackText.gameObject.SetActive(false);  // Deactivate feedback text
    }
}
