using UnityEngine;
using UnityEngine.UI;

public class PlayClickSound : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // 确保按钮存在
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlaySound);  // 给按钮添加点击事件
        }
    }

    void PlaySound()
    {
        audioSource.Play();  // 播放音效
    }
}
