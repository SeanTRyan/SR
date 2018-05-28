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
        [SerializeField] [Range(0f, 2000f)] private float m_MaxShield = 100f;
        [SerializeField] private float m_ShieldReplenishRate;
        [SerializeField] private float m_ShieldDepleteRate;

        //Allows the character to not be effected by hits as much
        [SerializeField] private float m_HitResist = 10f;

        public event Action<float> HealthChange;

        private Animator m_ShieldAnimator;

        public bool Shielding { get; private set; }
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get { return m_MaxShield; } }

        private void Awake()
        {
            CurrentHealth = m_MaxShield;

            m_ShieldAnimator = GetComponent<Animator>();
        }

        public void Execute(bool shield)
        {
            if (shield)
                TakeDamage(m_ShieldDepleteRate);
            else
                RestoreHealth(m_ShieldReplenishRate);

            Shielding = (shield && CurrentHealth > 0);

            AnimateShield(Shielding);
        }

        public void TakeDamage(float damage)
        {
            if (!Shielding)
                return;

            if (CurrentHealth <= 0f)
            {
                CurrentHealth = 0f;
                return;
            }

            CurrentHealth -= damage;

            HealthChange?.Invoke(CurrentHealth);
        }

        public void RestoreHealth(float restoreAmount)
        {
            if (CurrentHealth >= m_MaxShield)
            {
                CurrentHealth = m_MaxShield;
                return;
            }

            CurrentHealth += restoreAmount;

            HealthChange?.Invoke(CurrentHealth);
        }

        private void AnimateShield(bool shield)
        {
            m_ShieldAnimator.SetBool("Shielding", shield);
        }
    }
}
