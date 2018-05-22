using Survival;
using System.Collections;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Class that handles death.
    /// </summary>
    [RequireComponent(typeof(IHealth))]
    public class Death : MonoBehaviour
    {
        [SerializeField] private byte m_NumberOfLives = 2;

        public byte NumberOfLive { get { return m_NumberOfLives; } }
        public bool Dead { get; private set; }

        private void OnEnable()
        {
            GetComponent<IHealth>().HealthChange += HealthChange;
        }

        private void OnDisable()
        {
            GetComponent<IHealth>().HealthChange -= HealthChange;
        }

        private void HealthChange(float currentHealth)
        {
            if (currentHealth <= 0f && !Dead)
            {
                StopCoroutine(DeathRoutine());
                StartCoroutine(DeathRoutine());
            }
        }

        private IEnumerator DeathRoutine()
        {
            m_NumberOfLives--;
            Dead = true;

            yield return null;
        }
    }
}
