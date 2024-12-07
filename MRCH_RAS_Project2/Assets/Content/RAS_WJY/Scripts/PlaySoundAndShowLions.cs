using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundAndShowLions : MonoBehaviour
{
    private AudioSource source; // 音频源
    private BoxCollider LionTrigger; // 触发区域
    public GameObject femaleLion; // FemaleLion 对象
    public GameObject maleLion; // MaleLion 对象

    void Awake()
    {
        source = GetComponent<AudioSource>();
        LionTrigger = GetComponent<BoxCollider>();

        // 确保狮子模型在开始时隐藏
        if (femaleLion != null)
        {
            femaleLion.SetActive(false);
        }
        else
        {
            Debug.LogWarning("FemaleLion object is not assigned!");
        }

        if (maleLion != null)
        {
            maleLion.SetActive(false);
        }
        else
        {
            Debug.LogWarning("MaleLion object is not assigned!");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!source.isPlaying) // 确保语音只播放一次
        {
            source.Play();
            source.loop = false; // 防止循环播放
        }

        // 显示狮子模型
        if (femaleLion != null)
        {
            femaleLion.SetActive(true);
        }
        if (maleLion != null)
        {
            maleLion.SetActive(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (source.isPlaying)
        {
            source.Stop(); // 停止语音
            source.loop = false; // 取消循环播放
        }

        // 隐藏狮子模型
        if (femaleLion != null)
        {
            femaleLion.SetActive(false);
        }
        if (maleLion != null)
        {
            maleLion.SetActive(false);
        }
    }
}
