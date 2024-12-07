using UnityEngine;
using UnityEngine.UI;

public class MapToggleController : MonoBehaviour
{
    [SerializeField] private Button mapButton;     // The UI button for the map
    [SerializeField] private GameObject mapImage;  // The UI image representing the map
    [SerializeField] private AudioSource buttonAudio; // The audio source to play the sound

    private bool isMapVisible = false; // Tracks the visibility of the map

    void Start()
    {
        // Ensure the map is initially hidden
        if (mapImage != null)
        {
            mapImage.SetActive(false);
            
        }
        else
        {
            Debug.LogWarning("Map image is not assigned in the inspector.");
        }

        // Ensure the button is assigned and add the click listener
        if (mapButton != null)
        {
            mapButton.onClick.AddListener(ToggleMap);
        }
        else
        {
            Debug.LogWarning("Map button is not assigned in the inspector.");
        }
    }

    private void ToggleMap()
    {
        // Play the audio source if assigned
        if (buttonAudio != null)
        {
            buttonAudio.Play();
        }

        // Toggle the visibility of the map
        isMapVisible = !isMapVisible;
        if (mapImage != null)
        {
            mapImage.SetActive(isMapVisible);
        }
    }
}
