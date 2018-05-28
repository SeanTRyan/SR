using UI;
using UnityEngine;

namespace Spawners
{
    public class CursorSpawner : Spawner
    {
        [SerializeField] private Transform[] m_CursorParents = null;

        public void Awake()
        {
            if (!m_ManualSpawn)
            {
                int numberOfControllers = Input.GetJoystickNames().Length;

                Spawn(numberOfControllers);

                return;
            }

            int numberOfObjects = m_objectsToSpawn.Length;

            Spawn(numberOfObjects);
        }

        private void Spawn(int length)
        {
            for (int i = 0; i < length; i++)
            {
                GameObject cursor = Instantiate(m_objectsToSpawn[i], m_CursorParents[i], m_LoadInWorldSpace) as GameObject;

                if (!cursor.GetComponent<CursorUI>())
                    return;

                cursor.GetComponent<CursorUI>().Initialise(PlayerNumberUtility.m_PlayerNumbers[i + 1]);
            }
        }

        public override void Execute()
        {
            return;
        }

        public override void Initialise()
        {
            throw new System.NotImplementedException();
        }
    }
}
