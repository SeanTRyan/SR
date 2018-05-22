using Boxes;
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
        }

        private void OnDisable()
        {
            GetComponent<IHealth>().HealthChange -= HealthChange;
        }

        private void HealthChange(float currentHealth)
        {
            StopCoroutine(MakeImmune());
            StartCoroutine(MakeImmune());
        }

        private IEnumerator MakeImmune()
        {
            EnableHurtboxes(false);
            yield return new WaitForSeconds(m_ImmunityLength);
            EnableHurtboxes(true);
        }

        private void EnableHurtboxes(bool enable)
        {
            for (int i = 0; i < m_Hurtboxes.Count; i++)
                m_Hurtboxes[i].Enabled(enable);
        }
    }
}
