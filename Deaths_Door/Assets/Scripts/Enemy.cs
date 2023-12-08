using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 12/6/23
// simple enemy class that can take damage and die

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            PlayerController.Instance.transform.Find("AimAssistCone").GetComponent<AimAssist>().IgnoreEnemy(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExplosionRadius"))
        {
            TakeDamage(3);
        }
    }
}