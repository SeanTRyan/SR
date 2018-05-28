using Cameras;
using Characters;
using Managers;
using Player.UI;
using Spawners;
using UnityEngine;

namespace Player.Management
{
    /// <summary>
    /// Class that handles player instantiation.
    /// </summary>
    public class PlayerManager : Spawner
    {
        [SerializeField] private CameraController m_cameraManager = null;
        [SerializeField] private PlayerSpawner[] m_playerSpawner = null;
        [SerializeField] private PlayerUI[] m_playerUI = null;

        private int currentIndex = 0;

        public int Length { get { return m_objectsToSpawn.Length; } }

        public override void Initialise()
        {
            if (!m_ManualSpawn)
            {
                m_objectsToSpawn = new GameObject[PlayerSettings.NumberOfCharacters];
                for (int i = 0; i < PlayerSettings.NumberOfCharacters; i++)
                    m_objectsToSpawn[i] = PlayerSettings.GetCharacter(i);
            }
            Spawn();

            m_cameraManager.Initialise(this);
        }

        private void Spawn()
        {
            currentIndex = m_objectsToSpawn.Length;
            for (int i = 0; i < m_objectsToSpawn.Length; i++)
            {
                m_playerSpawner[i].Initialise(m_objectsToSpawn[i]);

                m_objectsToSpawn[i] = m_playerSpawner[i].Player;

                InitialiseControl(m_objectsToSpawn[i].GetComponent<CharacterManager>(), i);
                m_playerUI[i].Initialise(m_objectsToSpawn[i]);
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
            return m_objectsToSpawn[i];
        }


        public override void Execute()
        {
            return;
        }
    }

}

