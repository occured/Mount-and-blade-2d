using System.Collections;
using UnityEngine;

namespace MountAndBlade2D.UI
{
    /// <summary>
    /// Simple canvas group fader for scene transitions.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneTransitionFader : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 0.5f;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public Coroutine FadeIn(MonoBehaviour owner)
        {
            return owner.StartCoroutine(FadeTo(0f));
        }

        public Coroutine FadeOut(MonoBehaviour owner)
        {
            return owner.StartCoroutine(FadeTo(1f));
        }

        private IEnumerator FadeTo(float target)
        {
            if (_canvasGroup == null)
            {
                yield break;
            }

            var start = _canvasGroup.alpha;
            var time = 0f;
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(start, target, time / fadeDuration);
                yield return null;
            }

            _canvasGroup.alpha = target;
        }
    }
}
