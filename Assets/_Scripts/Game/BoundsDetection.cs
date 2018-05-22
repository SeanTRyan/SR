using Survival;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IDamagable health = collision.collider.GetComponent<IDamagable>();
        if (health != null)
            health.TakeDamage(100000);
    }
}
