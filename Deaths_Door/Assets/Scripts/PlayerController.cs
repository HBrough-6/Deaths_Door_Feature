using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Brough, Heath
// Created 11/9/23
// last modified 11/13/23

public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;

    private Transform objectToLookAt;
    private Vector3 posToLookAt;
    public Quaternion ChildRot;

    Deaths_Door inputActions;

    // delegates
    delegate void ProjectileType();
    ProjectileType currentProjectile;

    // projectile references
    public GameObject ArrowPrefab;
    public GameObject FireballPrefab;

    // how long the player has to hold down the mouse to shoot
    private float currentShootDelay;

    //if the player is in shooting mode
    private bool inShootingMode = false;

    // if the player can shoot
    private bool canShoot = false;

    private void Awake()
    {
        inputActions = new Deaths_Door();
        inputActions.Enable();

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

    // player enters shooting mode
    // player holds down mouse to charge up shot
    // on release of mouse, shoot projectile
    // or on the release of the shooting mode button cancel the shot

    // enter shooting mode by pressing and holding space
    // on release, player leaves shooting mode
    public void enterShootingMode(InputAction.CallbackContext context)
    {
        // player pressed space
        if (context.started)
        {
            // enter shooting mode
            inShootingMode = true;
            Debug.Log("entered shooting mode");
            // disable the melee attack when the player enters shooting mode
            inputActions.Player.Attack.Disable();
            // disable movement in shootig mode
            inputActions.Player.Move.Disable();
        }

        // player released space
        if (context.canceled)
        {
            // exit shooting mode and ensure the player cannot shoot
            inShootingMode = false;
            canShoot = false;
            Debug.Log("exited shooting mode");
            inputActions.Player.Attack.Enable();
            inputActions.Player.Move.Enable();
        }
    }

    /// <summary>
    /// shoots if the player holds down the left mouse button down for the required duration 
    /// </summary>
    /// <param name="context"></param>
    public void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("work");
        if (inShootingMode)
        {
            if (context.performed)
            {
                // player successfully held down button for long enough and attack is ready
                canShoot = true;
                Debug.Log("attack ready");
            }
        
            if (context.canceled && inShootingMode && canShoot)
            {
                // player released the button after charging attack
                canShoot = false;
                Debug.Log("Attack Released");
            }
        }
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
