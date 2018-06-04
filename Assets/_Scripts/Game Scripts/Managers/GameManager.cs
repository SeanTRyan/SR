using Player.Management;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Image m_fadeOutImage = null;
        [SerializeField] private Text[] m_startText = null;
        [SerializeField] private float m_fadeOutTime = 0.3f;

        private PlayerManager m_playerManager = null;
        private TimeManager m_timeManager = null;

        public static int CharactersDead;

        private void Awake()
        {
            CharactersDead = 0;
            m_timeManager = GetComponent<TimeManager>();
            m_playerManager = GetComponent<PlayerManager>();

            //for (int i = 0; i < m_playerManager.Length; i++)
                m_playerManager.Initialise();

            m_fadeOutImage.CrossFadeAlpha(1f, 0f, true);
        }

        private IEnumerator Start()
        {
            m_fadeOutImage.CrossFadeAlpha(0f, m_fadeOutTime, false);
            yield return new WaitForSeconds(m_fadeOutTime + 0.2f);
        }

        private void Update()
        {
            UpdateTime();

            GameOver();
        }

        private void GameOver()
        {
            int charactersDead = (CharactersDead % m_playerManager.Length);

            bool gameOver = (charactersDead == 1);

            if (m_timeManager.TimeEnded || gameOver)
            {
                Debug.Log("Game Over");
                return;
            }
        }

        private void UpdateTime()
        {
            if (!m_timeManager)
                return;

            m_timeManager.Execute();
        }
    }
}