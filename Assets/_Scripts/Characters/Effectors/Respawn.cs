using System.Collections;
using UnityEngine;

namespace Effectors
{
    public class Respawn : MonoBehaviour
    {
        [SerializeField] private float m_RespawnTime = 0f;

        private Transform m_Transform;

        private void Awake()
        {
            m_Transform = GetComponent<Transform>();
        }

        public void RespawnPosition(Transform respawnPosition)
        {
            m_Transform.position = respawnPosition.position;
        }

        private IEnumerator Respawning()
        {
            //Respawning message
            yield return new WaitForSeconds(m_RespawnTime);
            //Finished respawning message
        }
    }
}
