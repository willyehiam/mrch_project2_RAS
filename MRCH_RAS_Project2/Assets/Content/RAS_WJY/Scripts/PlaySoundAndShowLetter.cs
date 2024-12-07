using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundAndShowLetter : MonoBehaviour
{
    private AudioSource source;
    private BoxCollider soundTrigger;
    public GameObject letter; // Letter对象引用
    private bool letterShown = false; // 确保信只显示一次

    void Awake()
    {
        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<BoxCollider>();

        if (letter != null)
        {
            letter.SetActive(false); // 确保信件在开始时不可见
        }
        else
        {
            Debug.LogWarning("Letter object is not assigned!");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!source.isPlaying) // 确保声音只播放一次
        {
            source.Play();
            source.loop = false; // 防止循环播放
        }

        if (letter != null && !letterShown)
        {
            letter.SetActive(true); // 显示信件
            //letterShown = true; // 确保信件只显示一次
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (source.isPlaying)
        {
            source.Stop(); // 停止播放
            source.loop = false; // 取消循环播放
        }

        // 可选功能：隐藏信件（如果需要）
        
        if (letter != null)
        {
            letter.SetActive(false);
        }
        
    }
}
