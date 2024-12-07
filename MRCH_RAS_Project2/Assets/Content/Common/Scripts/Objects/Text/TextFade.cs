using System.Collections;
using UnityEngine;
using TMPro;

namespace MRCH.Common.Interact
{
    public abstract class TextFade : MonoBehaviour
    {
        public float fadeDuration;
        [ReadOnly, SerializeField] protected TMP_Text _text;
        [ReadOnly, SerializeField] protected TextMeshProUGUI _textUGUI;

        protected virtual void Awake()
        {
            TryGetComponent(out _text);
            if (!_text)
                TryGetComponent(out _textUGUI);
            if (_textUGUI == null)
                Debug.LogError("TextMeshPro not found in " + gameObject.name);
        }


        public virtual void FadeIn(float fadeDurationParam = 0)
        {
            Debug.Log("FadeIn on FadeTextWorldSpace of " + gameObject.name + " with duration " + fadeDurationParam);
            var fadeDurationToRun = fadeDurationParam == 0 ? fadeDuration : fadeDurationParam;
            if (_text)
                StartCoroutine(TextFadeHelper(_text, 1f, fadeDurationToRun));
            else if (_textUGUI)
                StartCoroutine(TextFadeHelper(_textUGUI, 1f, fadeDurationToRun));
        }

        public virtual void FadeOut(float fadeDurationParam = 0)
        {
            Debug.Log("FadeOut on FadeTextWorldSpace of " + gameObject.name + " with duration " + fadeDurationParam);
            var fadeDurationToRun = fadeDurationParam == 0 ? fadeDuration : fadeDurationParam;
            StartCoroutine(TextFadeHelper(_text, 0f, fadeDurationToRun));
        }

        protected static IEnumerator TextFadeHelper(TMP_Text text, float targetAlpha, float duration)
        {
            var originalAlpha = text.color.a;
            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var alpha = Mathf.Lerp(originalAlpha, targetAlpha, elapsed / duration);
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
                yield return null;
            }
        }
    }
}