using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] private Sprite[] mapVariations;     // Array of map variation sprites
    [SerializeField] private Button mapToggleButton;     // Button to toggle map visibility
    [SerializeField] private AudioSource buttonAudio;    // Audio source for button sound

    private Image mapImageComponent;                     // Image component on the map UI
    private int currentMapIndex = 0;                     // Tracks the current map variation
    private bool isMapVisible = false;                   // Tracks whether the map is visible

    void Start()
    {
        // Get the Image component on this object
        mapImageComponent = GetComponent<Image>();
        if (mapImageComponent == null)
        {
            Debug.LogError("MapController is missing an Image component.");
            return;
        }

        // Ensure the map starts hidden
        gameObject.SetActive(false);

        // Attach toggle functionality to the button
        if (mapToggleButton != null)
        {
            mapToggleButton.onClick.AddListener(ToggleMap);
        }
        else
        {
            Debug.LogError("Map toggle button is not assigned in the inspector.");
        }
    }

    private void ToggleMap()
    {
        // Play button click sound
        if (buttonAudio != null)
        {
            buttonAudio.Play();
        }

        // Toggle map visibility
        isMapVisible = !isMapVisible;
        gameObject.SetActive(isMapVisible);

        Debug.Log($"Map is now {(isMapVisible ? "visible" : "hidden")}.");
    }

    public void UpdateMap(int index)
    {
        // Validate index
        if (index >= 0 && index < mapVariations.Length)
        {
            currentMapIndex = index;
            mapImageComponent.sprite = mapVariations[index];
            Debug.Log($"Map updated to variation {index + 1}.");
        }
        else
        {
            Debug.LogWarning($"Invalid map variation index: {index}.");
        }
    }
}
