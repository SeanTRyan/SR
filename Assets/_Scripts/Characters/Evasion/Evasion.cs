using System;
using System.Collections;
using UnityEngine;

namespace Characters
{
    public class Evasion : MonoBehaviour
    {
        [SerializeField] private float m_RollSpeed;
        [SerializeField] private float m_RollLength;

        [SerializeField] private float m_SpotDodgeLength;

        private Rigidbody m_Rigidbody;
        private Animator m_EvasionAnimator;

        public Action<bool, float> EvasionEvent;

        private bool m_Rolling = false;
        private bool m_Dodging = false;

        public bool Rolling { get { return m_Rolling; } }
        public bool Dodging { get { return m_Dodging; } }

        // Use this for initialization
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_EvasionAnimator = GetComponent<Animator>();
        }

        public void Execute(Vector2 direction, bool shield)
        {
            //Spot Dodge
            if ((direction.y <= -0.5f && shield) && !Dodging)
            {
                StopCoroutine(SpotDodge());
                StartCoroutine(SpotDodge());
            }
            //Roll
            else if ((direction.x > 0.2f || direction.x < -0.2f) && shield && !Rolling)
            {
                StopCoroutine(Roll(direction));
                StartCoroutine(Roll(direction));
            }

            int evasionIndex = (Rolling) ? 2 : (Dodging) ? 1 : 0;

            AnimateEvasion(evasionIndex);
        }

        private IEnumerator Roll(Vector2 direction)
        {
            Evade(true, m_RollLength, ref m_Rolling);
            yield return new WaitForSeconds(m_RollLength);
            Evade(false, m_RollLength, ref m_Rolling);
        }

        private IEnumerator SpotDodge()
        {
            Evade(true, m_SpotDodgeLength, ref m_Dodging);
            yield return new WaitForSeconds(m_SpotDodgeLength);
            Evade(false, m_SpotDodgeLength, ref m_Dodging);
        }

        private void Evade(bool evasion, float length, ref bool evadeType)
        {
            if (evadeType == evasion)
                return;

            evadeType = evasion;

            EvasionEvent?.Invoke(evasion, length);
        }

        private void AnimateEvasion(int evasionIndex)
        {
            m_EvasionAnimator.SetInteger("EvasionIndex", evasionIndex);
            evasionIndex = 0;
        }
    }
}
