using Characters;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Survival.UI
{
    public class LifeTokenUI : MonoBehaviour
    {
        [SerializeField] private Text m_LivesRemainingText;
        [SerializeField] private Text m_LifeLostText;

        [SerializeField] private Vector3 m_TargetPosition;
        [SerializeField] private float m_Speed;

        private Vector3 m_TextVelocity;

        public void Initialise(Death deathRef)
        {
            m_LifeLostText.gameObject.SetActive(false);

            m_LifeLostText.transform.position = m_LivesRemainingText.transform.position;
            m_LivesRemainingText.text = deathRef.NumberOfLives.ToString();

            deathRef.DeathEvent += LostLifeToken;
        }

        private void LostLifeToken(byte livesRemaining)
        {
            m_LivesRemainingText.text = livesRemaining.ToString();

            StopCoroutine(LostTokenRoutine(livesRemaining));
            StartCoroutine(LostTokenRoutine(livesRemaining));
        }

        private IEnumerator LostTokenRoutine(byte numberOfLives)
        {
            m_LifeLostText.gameObject.SetActive(true);

            Vector3 startPosition = m_LivesRemainingText.transform.position;
            Vector3 endPosition = startPosition - m_TargetPosition;

            m_LifeLostText.transform.position = startPosition;
            while (startPosition != endPosition)
            {
                startPosition = Vector3.SmoothDamp(startPosition, endPosition, ref m_TextVelocity, m_Speed);

                m_LifeLostText.transform.position = startPosition;

                yield return null;
            }

            m_LifeLostText.gameObject.SetActive(false);
        }
    }
}
