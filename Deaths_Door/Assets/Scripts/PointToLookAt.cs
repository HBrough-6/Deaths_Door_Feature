using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// Created 11/13/23
// last modified 11/13/23
// moves an object around the game based on where the mouse is on the screen

public class PointToLookAt : MonoBehaviour
{
    public Vector3 worldPosition;
    public Vector3 screenPostion;

    // Plane used for finding the position on screen to look at
    Plane plane = new Plane(Vector3.down, 1.5f);

    // Update is called once per frame
    void Update()
    {
        screenPostion = Input.mousePosition;
        worldPosition = new Vector3(0, 0, 0);

        Ray ray = Camera.main.ScreenPointToRay(screenPostion);

        // checks if the raycast hits the plane and returns the position it hits
        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        transform.position = worldPosition;
    }
}
