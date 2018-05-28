using Survival;
using UnityEngine;

namespace Boxes
{
    /// <summary>
    /// Class that is responsible for areas where the character can be hurt.
    /// </summary>
    public class Hurtbox : Box
    {
        public delegate void HurtDelegate(int hurtIndex);
        public event HurtDelegate HurtEvent;

        private int m_hurtIndex = 0;
        private int m_previousIndex = 0;

        private void Awake()
        {
            if (BoxArea == BoxArea.MidTorso || BoxArea == BoxArea.RightThigh || BoxArea == BoxArea.LeftThigh)
                m_hurtIndex = 2;
            else if (BoxArea == BoxArea.LeftCalf || BoxArea == BoxArea.RightCalf)
                m_hurtIndex = 3;
            else
                m_hurtIndex = 1;

            m_active = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            Update_HitEvent(m_hurtIndex);
        }

        private void OnTriggerExit(Collider other)
        {
            Update_HitEvent(0);
        }

        private void Update_HitEvent(int hurtIndex)
        {
            HurtEvent?.Invoke(hurtIndex);
        }

        public void Enabled(bool enabled)
        {
            m_active = enabled;
            gameObject.layer = (enabled) ? (int)Layer.Hurtbox : (int)Layer.Dead;
        }
    }
}