using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class HammerChiselController : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector; // The timeline director
    [SerializeField] private Collider hammerChiselCollider;     // The capsule collider for interaction

    private bool isPlaying = false; // Tracks whether the timeline is currently playing

    void Start()
    {
        // Ensure the playableDirector is assigned
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }

        // Ensure the collider is assigned
        if (hammerChiselCollider == null)
        {
            hammerChiselCollider = GetComponent<Collider>();
        }

        // Set the playable director to start at the beginning and not play on awake
        if (playableDirector != null)
        {
            playableDirector.Stop();
        }
    }

    void Update()
    {
        // Check for user tap/click
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame) // Touch input for iPad
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == hammerChiselCollider)
                {
                    HandleTap();
                }
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame) // For desktop testing
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == hammerChiselCollider)
                {
                    HandleTap();
                }
            }
        }
    }

    private void HandleTap()
    {
        if (playableDirector != null)
        {
            // Stop the animation if it's playing
            if (isPlaying)
            {
                playableDirector.Stop();
                playableDirector.time = 0; // Reset to the beginning
            }

            // Play the animation
            playableDirector.Play();
            isPlaying = true;

            // After animation finishes, reset the isPlaying flag
            StartCoroutine(WaitForTimelineToEnd());
        }
    }

    private IEnumerator WaitForTimelineToEnd()
    {
        // Wait until the timeline is finished
        yield return new WaitForSeconds((float)playableDirector.duration);
        isPlaying = false;
    }
}
