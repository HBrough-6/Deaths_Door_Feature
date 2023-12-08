using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 12/6/23
// holds functions to create aim assist

public class AimAssist : MonoBehaviour
{
    private GameObject closestEnemy = null;
    private List<GameObject> EnemiesInSight;

    private void Awake()
    {
        EnemiesInSight = new List<GameObject>();
    }

    /// <summary>
    /// Finds the closest enemy inside of the AimAssist object and returns it
    /// </summary>
    /// <param name="initialPos">the point to measure from to find the closest enemy</param>
    /// <returns>returns the closest enemy inside of the AimAssist object or returns null if the list is empty</returns>
    public GameObject FindClosestEnemy(Vector3 initialPos)
    {
        float closestDistance = 1000;
        // get the players position at function call
        // playerPos = PlayerController.Instance.transform.position;
        if (EnemiesInSight.Count <= 0)
        {
            return null;
        }
        
        // loop through the list and compare distances, saving the closest one
        for (int index = 0; index < EnemiesInSight.Count; index++)
        {
            if ((initialPos - EnemiesInSight[index].transform.position).magnitude < closestDistance)
            {
                // save the distance
                closestDistance = (initialPos - EnemiesInSight[index].transform.position).magnitude;
                // set the new closest enemy
                closestEnemy = EnemiesInSight[index];
            }
        }

        return closestEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if an enemy is within the bounds of the aim assist, add them to the list
        if (other.CompareTag("Enemy"))
        {
            EnemiesInSight.Add(other.gameObject);
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        // if an enemy is ouside the bounds of the aim assist, remove them from the list
        if (other.CompareTag("Enemy"))
        {
            EnemiesInSight.Remove(other.gameObject);
            if (EnemiesInSight.Count == 0)
            {
                // if there are no more enemies in the list, set closestEnemy to null as there should be no aim assist
                closestEnemy = null;
            }
        }
    }

    /// <summary>
    /// when the AimAssist object is disabled, clear the list
    /// </summary>
    private void OnDisable()
    {
        EnemiesInSight.Clear();
    }

    /// <summary>
    /// ignores an enemy that is in the sight of the AimAssist
    /// </summary>
    /// <param name="toIgnore"></param>
    public void IgnoreEnemy(GameObject toIgnore)
    {
        // if the GameObject passed through is an enemy, remove the enemy from the list of seen enemies
        if (toIgnore.CompareTag("Enemy"))
        {
            EnemiesInSight.Remove(toIgnore);
        }
    }
}
