using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnter : MonoBehaviour
{
    AudioSource source;
    BoxCollider SoundTrigger;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        SoundTrigger = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!source.isPlaying)  // 确保只播放一次
        {
            source.Play();
            source.loop = true;  // 设置循环播放
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (source.isPlaying)
        {
            source.Stop();  // 停止播放
            source.loop = false;  // 取消循环播放
        }
    }
}
