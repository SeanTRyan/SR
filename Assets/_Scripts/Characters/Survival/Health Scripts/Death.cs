using Broadcasts;
using Survival;
using System.Collections;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Class that handles death.
    /// </summary>
    [RequireComponent(typeof(IHealth))]
    public class Death : MonoBehaviour, IBroadcast
    {
        [SerializeField] private byte m_NumberOfLives = 2;

        private Animator m_DeathAnimator;

        public delegate void DeathDelegate(byte numberOfLives);
        public event DeathDelegate DeathEvent;
        public byte NumberOfLives { get { return m_NumberOfLives; } }
        public bool Dead { get; private set; }

        private void Awake()
        {
            m_DeathAnimator = GetComponent<Animator>();
        }

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

            Broadcast.Send<IBroadcast>(gameObject, (x, y) => x.Inform(Broadcasts.BroadcastMessage.Dead));

            gameObject.layer = (int)Layer.Dead;

            DeathEvent?.Invoke(m_NumberOfLives);

            StopCoroutine(PlayDeath());
            StartCoroutine(PlayDeath());
        }

        private IEnumerator PlayDeath()
        {
            m_DeathAnimator.SetBool("Dead", true);

            yield return new WaitForEndOfFrame();

            m_DeathAnimator.SetBool("Dead", false);
        }

        public void Inform(BroadcastMessage message) { }
    }
}
