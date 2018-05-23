using Broadcasts;
using Survival;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Effectors
{
    /// <summary>
    /// Component that handles stuns and freezes
    /// </summary>
    public class Stun : MonoBehaviour, IBroadcast
    {
        [SerializeField] private float m_StunLength = 0f;
        [SerializeField] private float m_FreezeLength = 0f;

        private Animator m_Animator;
        private Rigidbody m_Rigidbody;
        private float m_PreviousSpeed = 0;

        public bool IsStunned { get; private set; }

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            GetComponent<IHealth>().HealthChange += Freeze;
            GetComponent<Shield>().HealthChange += ShieldStun;
        }

        private void OnDisable()
        {
            GetComponent<IHealth>().HealthChange -= Freeze;
            GetComponent<Shield>().HealthChange -= ShieldStun;
        }

        private void Freeze(float damage)
        {
            StopCoroutine(StartFreeze());
            StartCoroutine(StartFreeze());
        }

        private IEnumerator StartFreeze()
        {
            if (m_Animator.speed != 0)
                m_PreviousSpeed = m_Animator.speed;
            m_Animator.speed = 0f;
            m_Rigidbody.isKinematic = true;
            yield return new WaitForSeconds(m_FreezeLength);
            m_Animator.speed = m_PreviousSpeed;
            m_Rigidbody.isKinematic = false;
        }

        private void ShieldStun(float currentShield)
        {
            if (currentShield <= 0f)
            {
                StopCoroutine(StunRoutine(m_StunLength));
                StartCoroutine(StunRoutine(m_StunLength));
            }
        }

        public void StartStun(float stunLength)
        {
            StopCoroutine(StunRoutine(stunLength));
            StartCoroutine(StunRoutine(stunLength));
        }

        private IEnumerator StunRoutine(float stunLength)
        {
            //Maybe add a message alert here to indicate that character is stunned
            IsStunned = true;
            Broadcast.Send<IBroadcast>(gameObject, (x, y) => x.Inform(Broadcasts.BroadcastMessage.Stunned));
            yield return new WaitForSeconds(stunLength);
            Broadcast.Send<IBroadcast>(gameObject, (x, y) => x.Inform(Broadcasts.BroadcastMessage.None));
            IsStunned = false;
            //Play another message to indicate that the character is not longer stunned
        }

        public void Inform(BroadcastMessage message) { }
    }
}
