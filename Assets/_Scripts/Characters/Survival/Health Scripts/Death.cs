using Survival;
using System;
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

        public byte NumberOfLives { get { return m_NumberOfLives; } }
        public bool Dead { get; private set; }

        public delegate void DeathDelegate(byte numberOfLives);
        public event DeathDelegate DeathEvent;

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
                LoseLife(1);
        }

        private void LoseLife(byte numberOfLivesLost)
        {
            if (m_NumberOfLives <= 0)
                return;

            m_NumberOfLives -= numberOfLivesLost;

            DeathEvent?.Invoke(m_NumberOfLives);
        }
    }
}
