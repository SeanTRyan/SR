using Survival;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Effectors
{
    /// <summary>
    /// Component that handles stuns and freezes
    /// </summary>
    public class Stun : MonoBehaviour
    {
        [SerializeField] private float m_StunLength = 0f;
        [SerializeField] private float m_FreezeLength = 0f;

        public bool IsStunned { get; private set; }

        private void OnEnable()
        {
            GetComponent<IHealth>().HealthChange += Freeze;
        }

        private void OnDisable()
        {
            GetComponent<IHealth>().HealthChange -= Freeze;
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

        public void StartStun(float stunLength)
        {

        }

        private IEnumerator StunRoutine(float stunLength)
        {
            IsStunned = true;
            yield return new WaitForSeconds(stunLength);
            IsStunned = false;
        }
    }
}
