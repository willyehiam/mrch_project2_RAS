using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    // Assign the sprite GameObject in the Inspector
    [SerializeField] private GameObject selectSprite;
    [SerializeField] private GameObject quizContent;
    [SerializeField] private GameObject orb;
    void Awake()
    {
        if (selectSprite != null || quizContent != null || orb != null)
        {
            selectSprite.SetActive(false);  // Ensure the sprite is initially hidden
            quizContent.SetActive(false); // Ensure quizContent is initially hidden
            orb.SetActive(true); // Ensure orb is shown
        }
        else
        {
            Debug.LogWarning("selectSprite GameObject not assigned in the Inspector.");
            Debug.LogWarning("quizContent GameObject not assigned in the Inspector.");
            Debug.LogWarning("orb GameObject not assigned in the Inspector.");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (selectSprite != null && collider.CompareTag("Player") && quizContent != null && orb != null)
        {
            selectSprite.SetActive(true);  // Show the sprite
            quizContent.SetActive(true); // Show quiz content
            orb.SetActive(false); //Hide Orb
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (selectSprite != null && collider.CompareTag("Player") && quizContent != null && orb != null)
        {
            selectSprite.SetActive(false);  // Hide the sprite
            quizContent.SetActive(false); // Hide quiz content
            orb.SetActive(true); // orb reappear


        }
    }
}
