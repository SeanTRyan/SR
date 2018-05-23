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
        [SerializeField] [Range(0f, 2000f)] protected float m_MaxHealth;
        protected float m_CurrentHealth;

        //protected int m_HurtIndex;

        public float MaxShield { get { return m_MaxHealth; } }
        public float CurrentShield { get { return m_CurrentHealth; } }

        //An Action event that informs subscribers when health has changed
        public virtual event Action<float> HealthChange;

        private void Awake()
        {
            m_CurrentHealth = m_MaxHealth;
        }

        public virtual void RestoreHealth(float restoreAmount)
        {
            m_CurrentHealth += restoreAmount;

            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0f, m_MaxHealth);
        }

        public virtual void TakeDamage(float damage)
        {
            if (m_CurrentHealth <= 0)
            {
                m_CurrentHealth = 0f;
                return;
            }

            m_CurrentHealth -= damage;

            HealthChange?.Invoke(m_CurrentHealth);
        }

        public void HitArea(int hurtIndex)
        {
            //m_HurtIndex = hurtIndex;
        }
    }
}
