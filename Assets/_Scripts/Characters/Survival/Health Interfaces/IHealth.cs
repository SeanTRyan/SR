using System;

namespace Survival
{
    /// <summary>
    /// Interface for health.
    /// </summary>
    public interface IHealth
    {
        event Action<float> HealthChange;

        void RestoreHealth(float restoreAmount);
        float CurrentShield { get; }
        float MaxShield { get; }
    }
}
