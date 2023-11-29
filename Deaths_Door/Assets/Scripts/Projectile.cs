using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 11/29/2023
// base class for projectiles
public class Projectile : MonoBehaviour
{
    protected float speed;
    protected float assistDist;
    protected float assistWidth;

    protected int damage;

    // intial rotation
    protected Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        // set the rotation equal to the players rotation on creation
        transform.rotation = PlayerController.Instance.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Enemy>().TakeDamage(damage);
            // deal damage
        }
    }
}
