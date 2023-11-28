using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// Created 11/9/23
// last modified 11/13/23

public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;

    private Transform objectToLookAt;
    private Vector3 posToLookAt;
    public Quaternion ChildRot;

    // delegates
    delegate void ProjectileType();
    ProjectileType currentProjectile;

    // projectile references
    public GameObject ArrowPrefab;
    public GameObject FireballPrefab;


    private void Awake()
    {
        objectToLookAt = GameObject.Find("PointToLookAt").transform;
        Instance = this;

        currentProjectile = FireArrow;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotatePlayer();
    }

    private void rotatePlayer()
    {
        // looks at the x and y position of the object to look at, and looks at the current height of the player
        posToLookAt = new Vector3(objectToLookAt.position.x, transform.position.y, objectToLookAt.position.z);

        // looks at the position to look at
        transform.LookAt(posToLookAt);
    }

    // shoots the current projectile
    public void shootProjectile()
    {
        currentProjectile();
    }

    // shoots an arrow
    public void FireArrow()
    {
        Instantiate(ArrowPrefab, transform.position, transform.rotation);
    }

    // shoots a fireball
    public void FireFireball()
    {
        Instantiate(FireballPrefab, transform.position, transform.rotation);
    }
}
