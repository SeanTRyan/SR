using Characters;
using System.Collections;
using UnityEngine;

namespace Player.Management
{
    public class PlayerRespawner : MonoBehaviour
    {
        [SerializeField] private Transform m_transformPoint;
        [SerializeField] private float m_respawnDelay;

        private Animator m_respawnAnimator;
        private Transform m_target;

        private void Awake()
        {
            m_respawnAnimator = GetComponent<Animator>();
        }

        public void Initialise(Death deathRef)
        {
            m_target = deathRef.GetComponent<Transform>();

            deathRef.DeathEvent += Respawn;
        }

        private void Respawn(byte numberOfLives)
        {
            StopCoroutine(RespawnRoutine());
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(m_respawnDelay);
            m_target.position = m_transformPoint.position;
        }
    }
}
