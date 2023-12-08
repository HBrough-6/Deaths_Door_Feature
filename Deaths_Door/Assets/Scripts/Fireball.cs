using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 12/6/23
// class definition for the fireball projectile
public class Fireball : Projectile
{
    private int hits = 0;
    private bool canChain = true;

    private void Awake()
    {
        speed = 17;
        damage = 1;
    }

    /// <summary>
    /// each hit will decrease the size of the 
    /// </summary>
    /// <param name="other">Enemy that was hit</param>
    protected override void OnHit(Collider other)
    {
        // if the fireball can be chained
        if (canChain)
        {
            // looks at the next enemy to chain to
            // ignores the enemy that was just hit
            transform.GetChild(hits).GetComponent<AimAssist>().IgnoreEnemy(other.gameObject);
            // looks for the next closest enemy
            if (transform.GetChild(hits).GetComponent<AimAssist>().FindClosestEnemy(transform.position) != null)
            {
                transform.LookAt(transform.GetChild(hits).GetComponent<AimAssist>().FindClosestEnemy(transform.position).transform.position);
            }
        }
        
        
        // damage enemy
        other.gameObject.GetComponent<Enemy>().TakeDamage(damage);

        // increase hit counter by 1
        hits++;
        // changes the size of the area that the fire can chain hits together
        switch (hits)
        {
            case 1:
                // disables the first Assist cone
                transform.GetChild(0).gameObject.SetActive(false);
                // enables the second Assist cone
                transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                // disables the second Assist cone
                transform.GetChild(1).gameObject.SetActive(false);
                // enables the third Assist cone
                transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 3:
                // disables the third Assist cone 
                // no more chaining hits
                transform.GetChild(2).gameObject.SetActive(false);
                canChain = false;
                break;
            default:
                break;
        }

    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            OnHit(other);
        }
    }
}
