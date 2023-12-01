using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 11/29/2023
// base class for projectiles
public class Projectile : MonoBehaviour
{
    protected float speed = 10;
    protected float assistDist;
    protected float assistWidth;

    // how far away the projectile can be from the starting point
    protected float MaxDistAway = 100;

    // the starting position of the projectile
    protected Vector3 startPos;

    // how much damage the projectile does
    protected int damage;


    // Start is called before the first frame update
    void Start()
    {
        // set the rotation equal to the players rotation on creation
        transform.rotation = PlayerController.Instance.transform.rotation;

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if ((startPos-transform.position).magnitude > MaxDistAway)
        {
            Debug.Log("destroyed projectile");
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            OnHit();

            // deal damage
        }
    }

    public virtual void OnHit()
    {
        Debug.Log("hit an enemy");
        Destroy(this.gameObject);

    }
}
