using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 12/6/2023
// base class for projectiles
public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float speed = 10;

    [SerializeField]
    // how far away the projectile can be from the starting point
    protected float MaxDistAway = 100;

    // the starting position of the projectile
    protected Vector3 startPos;

    [SerializeField]
    // how much damage the projectile does
    protected int damage;


    // Start is called before the first frame update
    void Start()
    {
        // look at the closest enemy if it doesnt return null
        if (PlayerController.Instance.getClosestEnemy().transform.position != null)
        {
            MyOwnLookAt(PlayerController.Instance.getClosestEnemy().transform);
        }
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

    protected virtual void OnHit(Collider other)
    {
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            OnHit(other);
        }
    }

    protected void MyOwnLookAt(Transform target)
    {
        Vector3 _direction = target.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(_direction);

        transform.rotation = rotation;
    }

}
