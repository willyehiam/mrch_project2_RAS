using System.Collections;
using UnityEngine;

public class GuidingLetter : MonoBehaviour
{
    [SerializeField] private Transform[] triggerZones;
    private int currentZoneIndex = 0;
    public float moveSpeed = 2.0f; // Speed of movement
    public float bobbingAmplitude = 0.2f; // Height of the bobbing effect
    public float bobbingSpeed = 2.0f; // Speed of the bobbing effect

    private bool isMoving = false;
    private float initialY; // The initial Y position for the bobbing effect

    void Start()
    {
        // Ensure the sphere starts at the first trigger zone
        if (triggerZones.Length > 0 && triggerZones[0] != null)
        {
            transform.position = triggerZones[currentZoneIndex].position;
            initialY = transform.position.y;
        }
        else
        {
            Debug.LogWarning("Trigger zones not assigned or missing the first trigger zone.");
        }
    }

    void Update()
    {
        // Apply bobbing effect when idle
        if (!isMoving)
        {
            float newY = initialY + Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        // Check if player enters the trigger and if there's another zone to move to
        if (collider.CompareTag("Player") && currentZoneIndex < triggerZones.Length - 1 && !isMoving)
        {
            currentZoneIndex++; // Move to the next zone
            StartCoroutine(MoveToNextZone());
        }
    }

    private IEnumerator MoveToNextZone()
    {
        isMoving = true;
        Vector3 targetPosition = triggerZones[currentZoneIndex].position;

        // Move the sphere gradually towards the target position with bobbing
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // Calculate the bobbing offset
            float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Apply the bobbing effect to the Y position
            newPosition.y = targetPosition.y + bobbingOffset;

            // Update the sphere's position
            transform.position = newPosition;
            yield return null; // Wait for the next frame
        }

        // Update initialY for bobbing at the new trigger zone
        initialY = transform.position.y;
        isMoving = false;
        Debug.Log("Letter arrived at zone index: " + currentZoneIndex);
    }
}
