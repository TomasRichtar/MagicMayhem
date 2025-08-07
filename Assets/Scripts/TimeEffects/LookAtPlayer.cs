using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private bool invertRotation = true;

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player není nastaven v HealthBarLookAt!");
            return;
        }

        transform.LookAt(player);

        if (invertRotation)
        {
            transform.Rotate(0, 180, 0);
        }
    }
}
