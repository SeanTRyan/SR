using Boxes;
using Characters;
using Survival;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Effectors
{
    /// <summary>
    /// Class that handles making the character immune.
    /// </summary>
    [RequireComponent(typeof(IHealth))]
    public class Immunity : MonoBehaviour
    {
        [SerializeField] private float m_ImmunityLength;

        private List<Hurtbox> m_Hurtboxes = new List<Hurtbox>();

        private void Start()
        {
            BoxArea[] boxAreas = Enum.GetValues(typeof(BoxArea)).Cast<BoxArea>().ToArray();
            for (int i = 0; i < boxAreas.Length; i++)
            {
                Hurtbox hurtbox = GetComponent<BoxManager>().GetBox(BoxType.Hurtbox, boxAreas[i]) as Hurtbox;
                if (hurtbox)
                    m_Hurtboxes.Add(hurtbox);
            }
        }

        private void OnEnable()
        {
            GetComponent<IHealth>().HealthChange += HealthChange;
            GetComponent<Evasion>().EvasionEvent += Immune;
        }

        private void OnDisable()
        {
            GetComponent<IHealth>().HealthChange -= HealthChange;
            GetComponent<Evasion>().EvasionEvent -= Immune;
        }

        private void Immune(bool immune, float immunityLength)
        {
            if(immune)
            {
                StopCoroutine(MakeImmune(immunityLength));
                StartCoroutine(MakeImmune(immunityLength));
            }
        }

        private void HealthChange(float currentHealth)
        {
            StopCoroutine(MakeImmune(m_ImmunityLength));
            StartCoroutine(MakeImmune(m_ImmunityLength));
        }

        private IEnumerator MakeImmune(float immunityLength)
        {
            EnableHurtboxes(false);
            gameObject.layer = (int)Layer.PlayerDynamic;
            yield return new WaitForSeconds(immunityLength);
            gameObject.layer = (int)Layer.PlayerStatic;
            EnableHurtboxes(true);
        }

        private void EnableHurtboxes(bool enable)
        {
            for (int i = 0; i < m_Hurtboxes.Count; i++)
                m_Hurtboxes[i].Enabled(enable);
        }
    }
}
