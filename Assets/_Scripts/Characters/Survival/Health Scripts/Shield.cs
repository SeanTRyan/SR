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
        public float CurrentShield { get; private set; }
        public float MaxShield { get { return m_MaxShield; } }

        private void Awake()
        {
            CurrentShield = m_MaxShield;

            m_ShieldAnimator = GetComponent<Animator>();
        }

        public void Execute(bool shield)
        {
            if (shield)
                TakeDamage(m_ShieldDepleteRate);
            else
                RestoreHealth(m_ShieldReplenishRate);

            Shielding = (shield && CurrentShield > 0);

            AnimateShield(Shielding);
        }

        public void TakeDamage(float damage)
        {
            if (!Shielding)
                return;

            if (CurrentShield <= 0f)
            {
                CurrentShield = 0f;
                return;
            }

            CurrentShield -= damage;

            HealthChange?.Invoke(CurrentShield);
        }

        public void RestoreHealth(float restoreAmount)
        {
            if (CurrentShield >= m_MaxShield)
            {
                CurrentShield = m_MaxShield;
                return;
            }

            CurrentShield += restoreAmount;

            HealthChange?.Invoke(CurrentShield);
        }

        private void AnimateShield(bool shield)
        {
            m_ShieldAnimator.SetBool("Shielding", shield);
        }
    }
}
