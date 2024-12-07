using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundAndDisplay : MonoBehaviour
{
    private AudioSource source; // 音频源
    private BoxCollider triggerZone; // 触发区域

    public AudioClip soundToPlay; // 要播放的音频
    public List<GameObject> objectsToDisplay; // 要显示的对象列表

    void Awake()
    {
        source = GetComponent<AudioSource>();
        triggerZone = GetComponent<BoxCollider>();

        // 确保所有对象在开始时隐藏
        foreach (GameObject obj in objectsToDisplay)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
            else
            {
                Debug.LogWarning("One of the objects in objectsToDisplay is not assigned!");
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!source.isPlaying && soundToPlay != null) // 确保音频只播放一次
        {
            source.clip = soundToPlay; // 设置要播放的音频
            source.Play();
        }

        // 显示所有对象
        foreach (GameObject obj in objectsToDisplay)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (source.isPlaying)
        {
            source.Stop(); // 停止播放音频
        }

        // 隐藏所有对象
        foreach (GameObject obj in objectsToDisplay)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
