using System.Collections.Generic;
using UnityEngine;

namespace Visual_FX
{
    public class ParticleStorage : MonoBehaviour
    {
        [SerializeField] private List<Particle> m_Particles = new List<Particle>();

        private ParticleEffect m_PreviousEffect;
        private ushort m_ParticleIndex = 0;

        public void PlayEffect(ParticleEffect particleEffect)
        {
            if (m_PreviousEffect != particleEffect)
            {
                for (ushort i = 0; i < m_Particles.Count; i++)
                {
                    if (m_Particles[i].ParticleFound(particleEffect))
                    {
                        m_ParticleIndex = i;
                        break;
                    }
                }
            }

            m_PreviousEffect = particleEffect;

            m_Particles[m_ParticleIndex].Instantiate(transform);
        }
    }
}
