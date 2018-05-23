using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Survival.UI
{
    /// <summary>
    /// Class for displaying the health.
    /// </summary>
    public class HealthUI : MonoBehaviour
    {
        //Visuals for displaying the health bar.
        [SerializeField] private Image m_HealthForeground;
        [SerializeField] private Image m_HealthSecondary;
        [SerializeField] private Image m_HealthBackground;

        //Smoothing for the health decrease
        [SerializeField] protected float m_SmoothDamp = 0.3f;

        private float m_HealthVelocity = 0f;

        private float m_MaxHealth;
        private float m_TargetHealth;
        private float m_PreviousHealth;

        //Initialises the health and subscribes it to a character's health
        public void Initialise(IHealth healthRef)
        {
            m_MaxHealth = healthRef.MaxShield;
            m_PreviousHealth = m_TargetHealth = (m_MaxHealth / m_MaxHealth);

            m_HealthSecondary.fillAmount = m_HealthForeground.fillAmount = m_PreviousHealth;

            healthRef.HealthChange += (d) =>
            {
                m_TargetHealth = ((d) / m_MaxHealth);

                StopCoroutine(DecreaseHealth());
                StartCoroutine(DecreaseHealth());
            };
        }

        private IEnumerator DecreaseHealth()
        {
            m_HealthForeground.fillAmount = m_TargetHealth;
            while (m_PreviousHealth != m_TargetHealth)
            {
                m_PreviousHealth = Mathf.SmoothDamp(m_PreviousHealth, m_TargetHealth, ref m_HealthVelocity, m_SmoothDamp);

                m_HealthSecondary.fillAmount = m_PreviousHealth;

                yield return null;
            }
        }
    }
}
