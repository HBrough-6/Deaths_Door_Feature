using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 11/29/23
// simple enemy class that can take damage and die

public class Enemy : MonoBehaviour
{
    int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}