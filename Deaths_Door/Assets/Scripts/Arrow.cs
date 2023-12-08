using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 12/6/23
// class for the arrow projectile

public class Arrow : Projectile
{
    private void Awake()
    {
        speed = 25;
        damage = 1;
    }

    protected override void OnHit(Collider other)
    {
        other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(this.gameObject);
    }
}
