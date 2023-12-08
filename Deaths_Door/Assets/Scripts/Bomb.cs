using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 12/6/23
// class for a bomb projectile that did not make it into the final build

public class Bomb : Projectile
{
    public GameObject explosionPrefab;

    private void Awake()
    {
        speed = 25;
    }

    protected override void OnHit(Collider other)
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        StartCoroutine(Exploded(explosion));
    }

    private IEnumerator Exploded(GameObject explosion)
    {
        Debug.Log("explode");
        yield return new WaitForSeconds(0.1f);
        Destroy(explosion);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.CompareTag("BreakableWall"))
        {
            OnHit(other);
        }
    }

}
