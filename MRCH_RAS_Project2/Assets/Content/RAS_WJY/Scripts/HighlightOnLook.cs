using UnityEngine;

public class HighlightOnLook : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject[] poemSprites; // Array of poem sprites
    [SerializeField] private GameObject[] moreInfoSprites; // Array of more info sprites
    [SerializeField] private GameObject[] highlightOutlines; // Array of highlight outlines
    [SerializeField] private LayerMask spriteLayerMask; // LayerMask for sprites

    private GameObject currentHighlighted;
    private int currentIndex = -1; // Index of the currently highlighted sprite

    void Start()
    {
        // Ensure all highlight outlines and more info sprites are disabled initially
        if (highlightOutlines.Length != poemSprites.Length || moreInfoSprites.Length != poemSprites.Length)
        {
            Debug.LogError("The number of highlight outlines, poem sprites, and more info sprites must match.");
            return;
        }

        foreach (var outline in highlightOutlines)
        {
            if (outline != null)
                outline.SetActive(false);
        }

        foreach (var info in moreInfoSprites)
        {
            if (info != null)
                info.SetActive(false);
        }
    }

    void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera is not assigned.");
            return;
        }

        // Perform a 3D raycast from the center of the camera's view
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, spriteLayerMask))
        {
            // Check if the hit object is one of the poem sprites
            for (int i = 0; i < poemSprites.Length; i++)
            {
                if (hit.collider.gameObject == poemSprites[i] || hit.collider.gameObject == moreInfoSprites[i])
                {
                    // Enable the highlight outline for the looked-at sprite
                    if (currentHighlighted != highlightOutlines[i])
                    {
                        DisableAllOutlines();
                        highlightOutlines[i].SetActive(true);
                        currentHighlighted = highlightOutlines[i];
                        currentIndex = i;
                    }
                    return;
                }
            }
        }

        // Disable highlight if looking away from all sprites
        DisableAllOutlines();
        currentHighlighted = null;
        currentIndex = -1;
    }

    public void OnSelectButtonPressed()
    {
        if (currentIndex == -1)
            return;

        // Swap the visibility of the poem sprite and the more info sprite
        bool isPoemVisible = poemSprites[currentIndex].activeSelf;

        poemSprites[currentIndex].SetActive(!isPoemVisible);
        moreInfoSprites[currentIndex].SetActive(isPoemVisible);

        Debug.Log("Swapped visibility for index: " + currentIndex);
    }

    private void DisableAllOutlines()
    {
        foreach (var outline in highlightOutlines)
        {
            if (outline != null)
                outline.SetActive(false);
        }
    }
}
