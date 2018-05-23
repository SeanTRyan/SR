using UnityEngine;
using UnityEngine.UI;

namespace Survival.UI
{
    public class ShieldUI : MonoBehaviour
    {
        [SerializeField] private Image m_ShieldImage;
        [SerializeField] private Text m_ShieldText;

        private Shield m_Shield = null;

        public void Initialise(Shield shieldRef)
        {
            m_Shield = shieldRef;

            m_Shield.HealthChange += ShieldEffect;
        }

        private void OnDisable()
        {
            m_Shield.HealthChange -= ShieldEffect;
        }

        private void ShieldEffect(float currentShield)
        {
            m_ShieldText.text = "x" + currentShield.ToString("00");
        }
    }
}
