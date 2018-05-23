using Actions;
using Boxes;
using System;
using UnityEngine;

namespace Combat
{
    [Serializable]
    public class AttackAction
    {
        [SerializeField] private string tagID = "Jab One";
        [SerializeField] private Act attackAct = new Act();
        public Damage damage = null;
        [SerializeField] private Hitbox hitbox = null;

        private bool isAttacking = false;

        public bool IsAttacking { get { return isAttacking; } }
        public int AttackID { get; private set; }

        public void Load()
        {
            AttackID = Animator.StringToHash(tagID);
        }

        public void UpdateAttack(int attackID)
        {
            if (AttackID == attackID)
                isAttacking = true;

            if(hitbox.Hit)
            {
                attackAct.Reset();
                hitbox.Enabled(false);
                isAttacking = false;
                return;
            }

            if (!isAttacking)
                return;

            if (isAttacking)
                EnableHitbox();
        }

        private void EnableHitbox()
        {
            attackAct.Perform(ref isAttacking);

            hitbox.damage = damage.damage;

            hitbox.Enabled(isAttacking);
        }
    }
}