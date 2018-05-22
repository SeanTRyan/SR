using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Broadcasts;
using UnityEngine;

namespace Survival
{
    /// <summary>
    /// Shield class for characters that have shielding.
    /// </summary>
    [Serializable]
    public class Shield : MonoBehaviour, IHealth, IDamagable
    {
        [SerializeField] [Range(0f, 2000f)] private float m_MaxHealth = 100f;

        //Allows the character to not be effected by hits as much
        [SerializeField] protected float hitResist = 10f;

        private float m_CurrentHealth;

        public event Action<float> HealthChange;

        public bool Shielding { get; protected set; }

        public float CurrentHealth { get { return m_CurrentHealth; } }
        public float MaxHealth { get { return m_MaxHealth; } }

        private void Awake()
        {
            m_CurrentHealth = m_MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (m_CurrentHealth <= 0f)
            {
                m_CurrentHealth = 0f;

                return;
            }

            m_CurrentHealth -= damage;

            HealthChange?.Invoke(m_CurrentHealth);
        }

        public virtual void Execute(bool shield) { }

        public void RestoreHealth(float restoreAmount)
        {
            m_CurrentHealth += restoreAmount;
        }
    }
}
