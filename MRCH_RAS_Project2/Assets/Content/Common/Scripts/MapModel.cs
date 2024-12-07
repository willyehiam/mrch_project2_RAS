using UnityEngine;

namespace MRCH.Common.Tool
{
    [AddComponentMenu("MRCH-Interact/Tool/Map Model")]
    public class MapModel : MonoBehaviour
    {
        void Awake()
        {
#if !UNITY_EDITOR
            // Disable the GameObject and its children for builds
            gameObject.SetActive(false);
#endif
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            // Ensure this GameObject and its children are hidden in the Editor, but don't save them in builds
            gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.DontSaveInBuild;
            foreach (Transform child in transform)
            {
                child.gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.DontSaveInBuild;
            }
        }
#endif
    }
}