using System;
using UnityEngine;

namespace Combat
{
    /// <summary>
    /// Class for the different attacks that the character can perform. 
    /// </summary>
    [Serializable]
    public class AttackMove
    {
        [SerializeField] private AttackAction[] attackActions = null;

        public void Initialise()
        {
            for (int i = 0; i < attackActions.Length; i++)
                attackActions[i].Load();
        }

        public AttackAction GetAttack(int attackID)
        {
            for (int i = 0; i < attackActions.Length; i++)
            {
                attackActions[i].UpdateAttack(attackID);
                if (attackActions[i].IsAttacking)
                    return attackActions[i];
            }
            return null;
        }
    }
}