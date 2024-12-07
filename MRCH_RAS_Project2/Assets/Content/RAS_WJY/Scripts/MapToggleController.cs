using UnityEngine;
using UnityEngine.UI;

public class MapToggleController : MonoBehaviour
{
    [SerializeField] private Button mapButton;           // The UI button for toggling the map
    [SerializeField] private GameObject mapImage;        // The child object containing the map Image
    [SerializeField] private AudioSource buttonAudio;    // The audio source to play the sound
    [SerializeField] private Sprite[] mapVariations;     // Array of map variation sprites
    [SerializeField] private Collider[] triggerZones;    // Array of trigger zones

    private Image mapImageComponent;                     // Image component of the child map object
    private bool isMapVisible = false;                   // Tracks the visibility of the map

    void Start()
    {
        // Ensure the mapImage is assigned and has an Image component
        if (mapImage != null)
        {
            mapImageComponent = mapImage.GetComponent<Image>();
            if (mapImageComponent == null)
            {
                Debug.LogError("Map object does not have an Image component.");
            }
            else
            {
                // Set the initial map visibility to hidden
                mapImage.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Map object is not assigned in the inspector.");
        }

        // Ensure the mapButton is assigned and add the click listener
        if (mapButton != null)
        {
            mapButton.onClick.AddListener(ToggleMap);
        }
        else
        {
            Debug.LogError("Map Button is not assigned in the inspector.");
        }
    }

    private void ToggleMap()
    {
        // Play the button sound if assigned
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

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entered collider matches any trigger zone
        for (int i = 0; i < triggerZones.Length; i++)
        {
            if (other == triggerZones[i])
            {
                UpdateMapImage(i); // Update the map image based on the trigger zone index
                return;
            }
        }
    }

    private void UpdateMapImage(int index)
    {
        // Validate that the index is within bounds of the mapVariations array
        if (mapImageComponent != null && mapVariations != null && index < mapVariations.Length)
        {
            mapImageComponent.sprite = mapVariations[index]; // Change the map's source image
            Debug.Log($"Updated map to variation {index + 1}");
        }
        else
        {
            Debug.LogWarning($"Invalid map variation index {index}. Ensure arrays are set up correctly.");
        }
    }
}
