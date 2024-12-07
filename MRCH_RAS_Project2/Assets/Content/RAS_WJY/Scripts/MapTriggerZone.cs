using UnityEngine;

public class MapTriggerZone : MonoBehaviour
{
    public int mapIndex;                      // The map variation index associated with this trigger
    public MapController mapController;       // Reference to the MapController script

    private void OnTriggerEnter(Collider other)
    {
        if (mapController != null)
        {
            mapController.UpdateMap(mapIndex);
            Debug.Log($"Triggered map update to index {mapIndex}.");
        }
        else
        {
            Debug.LogWarning("MapController is not assigned to this trigger zone.");
        }
    }
}
