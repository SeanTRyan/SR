using Cameras;
using Character.UI;
using Characters;
using Managers;
using Spawners;
using UnityEngine;

namespace Player.Management
{
    /// <summary>
    /// Class that handles player instantiation.
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_characterPrefabs = null;
        [SerializeField] private CameraController m_cameraManager = null;
        [SerializeField] private PlayerCreator[] m_playerSpawner = null;
        [SerializeField] private CharacterUI[] m_playerUI = null;
        [SerializeField] private bool m_manualInstantiation = false;

        private int currentIndex = 0;

        public int Length { get { return m_characterPrefabs.Length; } }

        public void Initialise()
        {
            if (!m_manualInstantiation)
            {
                m_characterPrefabs = new GameObject[PlayerSettings.Length];
                for (int i = 0; i < PlayerSettings.Length; i++)
                    m_characterPrefabs[i] = PlayerSettings.GetCharacter(i);
            }
            CreateCharacter();

            m_cameraManager.Initialise(this);
        }

        private void CreateCharacter()
        {
            currentIndex = Length;
            for (int i = 0; i < Length; i++)
            {
                m_playerSpawner[i].Initialise(m_characterPrefabs[i]);

                m_characterPrefabs[i] = m_playerSpawner[i].Player;

                InitialiseControl(m_characterPrefabs[i].GetComponent<CharacterManager>(), i);
                m_playerUI[i].Initialise(m_characterPrefabs[i]);
            }
        }

        private void InitialiseControl(CharacterManager controller, int number)
        {
            if (!controller)
                return;

            controller.InitialiseDevice(number);
        }

        public GameObject GetPlayer(int i)
        {
            return m_characterPrefabs[i];
        }
    }
}

