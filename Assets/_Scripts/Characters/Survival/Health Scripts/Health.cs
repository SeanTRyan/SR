using Broadcasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Survival
{
    /// <summary>
    /// Component for health
    /// </summary>
    public class Health : MonoBehaviour, IHealth, IDamagable
    {
        [SerializeField] [Range(0f, 2000f)] protected float m_maxHealth;
        protected float m_currentHealth;

        //protected int m_HurtIndex;

        public float MaxHealth { get { return m_maxHealth; } }
        public float CurrentHealth { get { return m_currentHealth; } }

        //An Action event that informs subscribers when health has changed
        public virtual event Action<float> HealthChange;

        private Shield m_shield;

        private void Awake()
        {
            m_currentHealth = m_maxHealth;

            m_shield = GetComponent<Shield>();
        }

        public virtual void RestoreHealth(float restoreAmount)
        {
            m_currentHealth += restoreAmount;

            m_currentHealth = Mathf.Clamp(m_currentHealth, 0f, m_maxHealth);

            HealthChange?.Invoke(m_currentHealth);
        }

        public virtual void TakeDamage(float damage)
        {
            if (m_shield)
                if (m_shield.Shielding)
                    return;

            if (m_currentHealth <= 0)
            {
                m_currentHealth = 0f;
                return;
            }

            m_currentHealth -= damage;

            HealthChange?.Invoke(m_currentHealth);
        }

        public void HitArea(int hurtIndex)
        {
            //m_HurtIndex = hurtIndex;
        }
    }
}
