using Characters;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Survival.UI
{
    public class LifeTokenUI : MonoBehaviour
    {
        [SerializeField] private Text m_numberOfLivesText;
        [SerializeField] private Text m_lifeLostText;

        [SerializeField] private Vector3 m_targetPosition;
        [SerializeField] private float m_smoothDamp;

        private Vector3 m_textVelocity;

        public void Initialise(Death deathRef)
        {
            m_lifeLostText.gameObject.SetActive(false);

            m_numberOfLivesText.text = deathRef.NumberOfLives.ToString();

            deathRef.DeathEvent += LostLifeToken;
        }

        private void LostLifeToken(byte livesRemaining)
        {
            m_numberOfLivesText.text = livesRemaining.ToString();

            StopCoroutine(LostTokenRoutine(livesRemaining));
            StartCoroutine(LostTokenRoutine(livesRemaining));
        }

        private IEnumerator LostTokenRoutine(byte numberOfLives)
        {
            m_lifeLostText.gameObject.SetActive(true);

            Vector3 startPosition = m_lifeLostText.transform.position;
            Vector3 originalPosition = startPosition;
            Vector3 endPosition = startPosition - m_targetPosition;

            m_lifeLostText.transform.position = startPosition;
            while (startPosition != endPosition)
            {
                startPosition = Vector3.SmoothDamp(startPosition, endPosition, ref m_textVelocity, m_smoothDamp);

                m_lifeLostText.transform.position = startPosition;

                yield return null;
            }

            m_lifeLostText.transform.position = originalPosition;
            m_lifeLostText.gameObject.SetActive(false);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
