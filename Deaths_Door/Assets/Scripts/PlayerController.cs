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

    // used to look at the mouse position
    private Transform playerRotStorageRef;

    private void Awake()
    {
        objectToLookAt = GameObject.Find("PointToLookAt").transform;
        playerRotStorageRef = GameObject.Find("playerRotatorStorage").transform;
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
        posToLookAt = new Vector3(objectToLookAt.position.x, 1.5f, objectToLookAt.position.z);
        // playerRotStorageRef.LookAt(posToLookAt);
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
