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

        public bool IsStunned { get; private set; }

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
            //Maybe add a message alert here to indicate that character is stunned
            IsStunned = true;
            yield return new WaitForSeconds(m_StunLength);
            IsStunned = false;
            //Play another message to indicate that the character is not longer stunned
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
            IsStunned = true;
            Broadcast.Send<IBroadcast>(gameObject, (x, y) => x.Inform(Broadcasts.BroadcastMessage.Stunned));
            yield return new WaitForSeconds(stunLength);
            Broadcast.Send<IBroadcast>(gameObject, (x, y) => x.Inform(Broadcasts.BroadcastMessage.None));
            IsStunned = false;
        }

        public void Inform(BroadcastMessage message) { }
    }
}
