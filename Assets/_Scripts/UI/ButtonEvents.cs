using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public enum SceneLoad { MainMenu, CharacterSelect, Play, Quit }

    public class ButtonEvents : MonoBehaviour
    {
        [SerializeField] private float m_speed;
        [SerializeField] private float m_smoothTime;

        private Button m_button;

        // Use this for initialization
        private void Awake()
        {
            m_button = GetComponent<Button>();
        }

        public void FadeIn(CanvasGroup menuGroup)
        {
            StopCoroutine(MenuRoutine(0f, menuGroup, false));
            StartCoroutine(MenuRoutine(1f, menuGroup, true));
        }

        public void FadeOut(CanvasGroup menuGroup)
        {
            StopCoroutine(MenuRoutine(0f, menuGroup, false));
            StartCoroutine(MenuRoutine(0f, menuGroup, false));
        }

        public void LoadScene(string sceneName)
        {
            if (sceneName == "Quit" || sceneName == "quit")
                Application.Quit();

            SceneManager.LoadScene(sceneName);
        }

        private IEnumerator MenuRoutine(float alphaValue, CanvasGroup menuGroup, bool active)
        {
            float currentVelocity = 0f;
            while (menuGroup.alpha != alphaValue)
            {
                float fadeValue = Mathf.SmoothDamp(menuGroup.alpha, alphaValue, ref currentVelocity, m_smoothTime, m_speed, Time.unscaledTime);
                menuGroup.alpha = fadeValue;

                yield return null;
            }

            menuGroup.gameObject.SetActive(active);
        }
    }
}