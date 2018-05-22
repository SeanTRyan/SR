
using UnityEngine;

namespace Visual_FX
{
    public enum ParticleEffect { Land, Punch, Run, Idle, Kick, Hit, Hurt }

    [CreateAssetMenu(fileName = "Particle", menuName = "Particle", order = 0)]
    public class Particle : ScriptableObject
    {
        [SerializeField] private GameObject m_ParticleResource;
        [SerializeField] private ParticleEffect m_ParticleEffect;

        private Transform m_ParentTransform;
        private ParticleSystem m_Particle = null;
        private bool m_IsNull = true;

        public bool ParticleFound(ParticleEffect particleEffect)
        {
            return (particleEffect == m_ParticleEffect);
        }

        public void Instantiate(Transform parent)
        {
            m_ParentTransform = parent;

            if (m_IsNull)
            {
                m_ParticleResource = Instantiate(m_ParticleResource);
                m_Particle = m_ParticleResource.GetComponent<ParticleSystem>();
                m_IsNull = false;
                return;
            }

            m_ParticleResource.transform.position = m_ParentTransform.position;

            m_Particle.Clear();
            m_Particle.Play();
        }
    }
}
