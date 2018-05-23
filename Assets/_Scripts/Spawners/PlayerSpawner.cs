using Characters;
using Managers;
using Survival;
using Survival.UI;
using UnityEngine;

namespace Spawners
{
    public class PlayerSpawner : Spawner
    {
        [SerializeField] protected Transform[] m_ObjectParents = null;
        [SerializeField] private HealthUI[] m_HealthUI = null;
        [SerializeField] private ShieldUI[] m_ShieldUI = null;
        [SerializeField] private LifeTokenUI[] m_LifeToken = null;

        public override void Initialise()
        {
            if (!m_ManualSpawn)
            {
                m_ObjectsToSpawn = new GameObject[PlayerSettings.NumberOfCharacters];
                for (int i = 0; i < PlayerSettings.NumberOfCharacters; i++)
                    m_ObjectsToSpawn[i] = PlayerSettings.GetCharacter(i);
            }
            Spawn();
        }

        private void Spawn()
        {
            for (int i = 0; i < m_ObjectsToSpawn.Length; i++)
            {
                Transform parent = m_ObjectParents[i];
                if (parent)
                    m_ObjectsToSpawn[i] = Instantiate(m_ObjectsToSpawn[i], parent, m_LoadInWorldSpace) as GameObject;
                else
                    m_ObjectsToSpawn[i] = Instantiate(m_ObjectsToSpawn[i]) as GameObject;

                InitialiseControl(m_ObjectsToSpawn[i].GetComponent<CharacterManager>(), i);
                InitialiseHealth(m_ObjectsToSpawn[i], i);
                InitialiseShield(m_ObjectsToSpawn[i], i);
                //InitialiseLifeTokens(m_ObjectsToSpawn[i], i);
            }
        }

        private void InitialiseHealth(GameObject gameObject, int index)
        {
            IHealth healthRef = gameObject.GetComponent<IHealth>();
            if (healthRef != null)
                m_HealthUI[index].Initialise(healthRef);
        }

        private void InitialiseLifeTokens(GameObject gameObject, int index)
        {
            Death deathRef = gameObject.GetComponent<Death>();
            if (deathRef != null)
                m_LifeToken[index].Initialise(deathRef);
        }

        private void InitialiseShield(GameObject gameObject, int index)
        {
            Shield shieldRef = gameObject.GetComponent<Shield>();
            if (shieldRef != null)
                m_ShieldUI[index].Initialise(shieldRef);
        }

        private void InitialiseControl(CharacterManager controller, int number)
        {
            if (!controller)
                return;

            controller.InitialiseDevice(number);
        }

        public override void Execute()
        {
            return;
        }
    }

}

