using Broadcasts;
using Controls;
using UnityEngine;

namespace Managers
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject m_pausePanel;

        public static bool IsPaused { get; private set; }

        private Device[] m_devices;

        public void Inform(BroadcastMessage message) { }

        private void Start()
        {
            m_pausePanel.SetActive(IsPaused);

            m_devices = new Device[InputManager.Length];

            for (int i = 0; i < m_devices.Length; i++)
                m_devices[i] = InputManager.GetDevice(i);
        }

        private void Update()
        {
            for (int i = 0; i < m_devices.Length; i++)
            {
                if (m_devices[i].Start.Press)
                {
                    IsPaused = !IsPaused;
                    Time.timeScale = (IsPaused) ? 0 : 1;
                    m_pausePanel.SetActive(IsPaused);
                }
            }
        }
    }
}
