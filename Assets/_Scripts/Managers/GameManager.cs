using Player.Management;
using Spawners;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Image m_FadeOutImage = null;
        [SerializeField] private float m_FadeOutTime = 0.3f;

        private PlayerManager[] m_playerManager = null;
        private TimeManager m_TimeManager = null;

        private void Awake()
        {
            m_TimeManager = GetComponent<TimeManager>();
            m_playerManager = GetComponents<PlayerManager>();

            for (int i = 0; i < m_playerManager.Length; i++)
                m_playerManager[i].Initialise();

            m_FadeOutImage.CrossFadeAlpha(1f, 0f, true);
        }

        private IEnumerator Start()
        {
            m_FadeOutImage.CrossFadeAlpha(0f, m_FadeOutTime, false);
            yield return new WaitForSeconds(m_FadeOutTime + 0.2f);
        }

        private void Update()
        {
            for (int i = 0; i < m_playerManager.Length; i++)
                m_playerManager[i].Execute();

            UpdateTime();
        }

        private void UpdateTime()
        {
            if (!m_TimeManager)
                return;

            m_TimeManager.Execute();
        }
    }
}