﻿using Character.UI;
using UnityEngine;

namespace Managers
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] private HealthUI[] health = null;

        private void Awake()
        {
        }
    }
}
