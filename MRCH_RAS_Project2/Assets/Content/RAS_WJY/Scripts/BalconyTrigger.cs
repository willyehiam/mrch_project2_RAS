using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalconyTrigger : MonoBehaviour
{
    // Assign the sprite GameObject in the Inspector
    [SerializeField] private GameObject spriteToDisplay;

    void Awake()
    {
        if (spriteToDisplay != null)
        {
            spriteToDisplay.SetActive(false);  // Ensure the sprite is initially hidden
        }
        else
        {
            Debug.LogWarning("Sprite GameObject not assigned in the Inspector.");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (spriteToDisplay != null && collider.CompareTag("Player"))
        {
            spriteToDisplay.SetActive(true);  // Show the sprite
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (spriteToDisplay != null && collider.CompareTag("Player"))
        {
            spriteToDisplay.SetActive(false);  // Hide the sprite
        }
    }
}
