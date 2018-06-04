using System;
using UnityEngine;

namespace Particles
{
    public enum ParticleType { None, Hit, Shield }

    [Serializable]
    public class Particle
    {
        [SerializeField] private GameObject m_particlePrefab = null;
        [SerializeField] private ParticleType m_particleType = ParticleType.None;

        private ParticleSystem m_particleSystem = null;
        private bool m_isNull = true;

        public ParticleType ParticleType { get { return m_particleType; } }

        public void InstantiateParticle(Transform parent, Vector3 position)
        {
            if (m_isNull)
            {
                m_particlePrefab = GameObject.Instantiate(m_particlePrefab, parent) as GameObject;

                m_particleSystem = m_particlePrefab.GetComponent<ParticleSystem>();

                m_isNull = false;

                return;
            }

            m_particleSystem.Clear();
            m_particleSystem.Play();
        }
    }
}
