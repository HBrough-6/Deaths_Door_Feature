using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

// Brough, Heath
// Created 11/9/23
// last modified 11/13/23

enum AttackType
{
    Empty = -1,
    Arrow,
    Fireball
    
}

public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;

    private Transform objectToLookAt;
    private Vector3 posToLookAt;
    public Quaternion ChildRot;

    Deaths_Door inputActions;

    // delegates
    delegate void CurrentAttack();
    CurrentAttack currentSpell;

    // projectile references
    public GameObject ArrowPrefab;
    public GameObject FireballPrefab;

    //if the player is in shooting mode
    private bool inShootingMode = false;

    // if the player can shoot
    private bool canShoot = false;

    // if the player charged up their melee attack
    private bool meleeCharged = false;

    // tracks the last equipped projectile type
    AttackType lastAttack = AttackType.Empty;

    // actions reference
    private Component playerInputComponent;
    private void Awake()
    {
        inputActions = new Deaths_Door();
        inputActions.Enable();

        //                                     ActionMap     action
        // GetComponent<PlayerInput>().actions.actionMaps[0].actions[0].Disable();

        // disables shooting when the game starts
        GetComponent<PlayerInput>().actions.actionMaps[2].Disable();
        objectToLookAt = GameObject.Find("PointToLookAt").transform;
        Instance = this;

        currentSpell = FireArrow;
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
            // enable shooting
            GetComponent<PlayerInput>().actions.actionMaps[2].Enable();
            // disable attacking
            GetComponent<PlayerInput>().actions.actionMaps[0].actions[1].Disable();
            // disable movement 
            GetComponent<PlayerInput>().actions.actionMaps[0].actions[0].Disable();
            // enter shooting mode
            inShootingMode = true;
            Debug.Log("entered shooting mode");


        }

        // player released space
        if (context.canceled)
        {
            // exit shooting mode and ensure the player cannot shoot
            inShootingMode = false;
            canShoot = false;
            Debug.Log("exited shooting mode");
            // disable shooting
            GetComponent<PlayerInput>().actions.actionMaps[2].Disable();
            // Enable attacking
            GetComponent<PlayerInput>().actions.actionMaps[0].actions[1].Enable();
            // Enable movement 
            GetComponent<PlayerInput>().actions.actionMaps[0].actions[0].Enable();
        }
    }

    /// <summary>
    /// shoots if the player holds down the left mouse button down for the required duration 
    /// Attacks if the player is not in shooting mode
    /// </summary>
    /// <param name="context"></param>
    public void Attack(InputAction.CallbackContext context)
    {
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
                // shoots current
                currentSpell();
            }
        }
        else
        {
            // when the player is outside of shooting mode, melee attack
            if (context.performed)
            {
                SwingSword();
            }
            
        }
    }


    /// <summary>
    /// disables the last projectile that was equipped, skipping the disable if there was no previous projectile (lastProj = -1)
    /// </summary>
    private void DisableLastAttack()
    {
        // if there was no previous projectile equipped, skip disabling a projectile
        if ((int)lastAttack == -1) return;
        GetComponent<PlayerInput>().actions.actionMaps[2].actions[(int)lastAttack].Disable();
    }

    /*
     Equipping Projectile Functions
     */

    /// <summary>
    /// Equips the Arrow Attack if not already equipped
    /// </summary>
    public void EquipArrow(InputAction.CallbackContext context)
    {
        // only run when button is pressed initially
        if (context.started)
        {
            // if the arrow is not the current spell, equip it
            if (currentSpell != FireArrow)
            {
                Debug.Log("equips arrow");
                // disable the previous projectile type
                DisableLastAttack();
                currentSpell = FireArrow;
                lastAttack = AttackType.Arrow;
            }
            else Debug.Log("arrow already equipped");
        }
    }

    /// <summary>
    /// Equips the Fireball Attack if not already equipped
    /// </summary>
    public void EquipFireBall(InputAction.CallbackContext context)
    {
        // only run when button is pressed initially
        if (context.started)
        {
            // if the fireball is not the currentSpell, equip it
            if (currentSpell != FireFireball)
            {
                Debug.Log("equips fireball");
                // disable the previous projectile type
                DisableLastAttack();
                currentSpell = FireFireball;
                lastAttack = AttackType.Arrow;
            }
            else Debug.Log("fireball already equipped");
        }
    }

    /*
     Action Functions
     */

    /// <summary>
    /// Shoots an arrow
    /// </summary>
    public void FireArrow()
    {
        Debug.Log("shoots arrow");
                    //Instantiate(ArrowPrefab, transform.position, transform.rotation);
    }

    /// <summary>
    /// Shoots a fireball
    /// </summary>
    public void FireFireball()
    {
        Debug.Log("shoots fireball");
                    //Instantiate(FireballPrefab, transform.position, transform.rotation);
    }

    /// <summary>
    /// Swings a Sword
    /// </summary>
    public void SwingSword()
    {
        Debug.Log("Swoosh");
    }

    
}
