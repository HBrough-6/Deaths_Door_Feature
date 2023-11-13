using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// Created 11/9/23
// last modified 11/13/23

public class PlayerController : MonoBehaviour
{

    private Transform objectToLookAt;
    private Vector3 posToLookAt;
    public Quaternion ChildRot;

    // used to look at the mouse position
    private Transform playerRotStorageRef;

    // Start is called before the first frame update
    void Start()
    {
        objectToLookAt = GameObject.Find("PointToLookAt").transform;
        playerRotStorageRef = GameObject.Find("playerRotatorStorage").transform;
    }

    // Update is called once per frame
    void Update()
    {
        posToLookAt = new Vector3(objectToLookAt.position.x, 1.5f, objectToLookAt.position.z);
        // playerRotStorageRef.LookAt(posToLookAt);
        transform.LookAt(posToLookAt);
    }
}
