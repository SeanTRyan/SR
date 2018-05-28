using UnityEngine;

namespace Spawners
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] protected GameObject[] m_objectsToSpawn = null;
        [SerializeField] protected bool m_LoadInWorldSpace = true;
        [SerializeField] protected bool m_ManualSpawn = false;

        public int Length { get { return m_objectsToSpawn.Length; } }

        public abstract void Initialise();
        public abstract void Execute();

        //public GameObject GetObject(int index) { return m_objectsToSpawn[index]; }

    }
}
