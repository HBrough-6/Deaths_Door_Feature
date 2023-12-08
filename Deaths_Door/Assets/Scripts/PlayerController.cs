using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

// Brough, Heath
// Created 11/9/23
// last modified 12/6/23
// handles the movement, shooting, and melee attacks of the player

enum AttackType
{
    Empty = -1,
    Arrow = 2,
    Fireball,
    Bomb,
    Hookshot,
    
}

public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;

    private Transform objectToLookAt;
    private Vector3 posToLookAt;

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
    private AttackType activeSpell = AttackType.Empty;

    // aimAssist cone reference
    private GameObject aimAssistRef;

    // how much mana the player has left
    private int Mana = 4;

    private int currentManaCost;

    // actions reference
    private Component playerInputComponent;
    private void Awake()
    {
        inputActions = new Deaths_Door();
        inputActions.Enable();

        // assign the reference to the object
        aimAssistRef = transform.GetChild(0).gameObject;
        // disable the cone until needed
        aimAssistRef.SetActive(false);

        //                                     ActionMap     action
        // GetComponent<PlayerInput>().actions.actionMaps[0].actions[0].Disable();

        // disables shooting when the game starts
        GetComponent<PlayerInput>().actions.actionMaps[(int)AttackType.Arrow].Disable();
        GetComponent<PlayerInput>().actions.actionMaps[(int)AttackType.Fireball].Disable();
        objectToLookAt = GameObject.Find("PointToLookAt").transform;
        Instance = this;


        // equips arrow as the first projectile
        currentSpell = FireArrow;
        activeSpell = AttackType.Arrow;
        currentManaCost = 1;
    }

    // Update is called once per frame
    void Update()
    {
        rotatePlayer();
        UIManager.Instance.AttackStatus(canShoot);
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
            GetComponent<PlayerInput>().actions.actionMaps[(int)activeSpell].Enable();
            // disable attacking
            GetComponent<PlayerInput>().actions.actionMaps[0].actions[1].Disable();
            // disable movement 
            GetComponent<PlayerInput>().actions.actionMaps[0].actions[0].Disable();
            // enter shooting mode
            inShootingMode = true;
            //activate the cone
            aimAssistRef.SetActive(true);
            Debug.Log("entered shooting mode");


        }

        // player released space
        if (context.canceled)
        {
            // exit shooting mode and ensure the player cannot shoot
            inShootingMode = false;
            canShoot = false;
            // disable shooting
            GetComponent<PlayerInput>().actions.actionMaps[(int)activeSpell].Disable();
            // Enable attacking
            GetComponent<PlayerInput>().actions.actionMaps[0].actions[1].Enable();
            // Enable movement 
            GetComponent<PlayerInput>().actions.actionMaps[0].actions[0].Enable();
            // deactivate the cone
            aimAssistRef.SetActive(false);
            Debug.Log("exited shooting mode");
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
        
            if (context.canceled && inShootingMode && canShoot && Mana >= currentManaCost)
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
                ManageMana(1);
            }
        }
    }

    /// <summary>
    /// disables the last projectile that was equipped, skipping the disable if there was no previous projectile (lastProj = -1)
    /// </summary>
    private void DisableLastAttack(int toDisable)
    {
        // if there was no previous projectile equipped, skip disabling a projectile
        GetComponent<PlayerInput>().actions.actionMaps[toDisable].Disable();
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
                // change the cost for shooting
                currentManaCost = 1;

                // Reflect weapon selection change in the UI
                UIManager.Instance.ChangeWeapon(1);
                Debug.Log("equips arrow");

                // disable the previous projectile type
                DisableLastAttack((int)AttackType.Fireball);

                // set the current spell
                currentSpell = FireArrow;
                activeSpell = AttackType.Arrow;

                // enable the action Map for arrows
                GetComponent<PlayerInput>().actions.actionMaps[(int)activeSpell].Enable();
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
                // change the cost for shooting
                currentManaCost = 1;

                // Reflect weapon selection change in the UI
                UIManager.Instance.ChangeWeapon(2);
                Debug.Log("equips fireball");

                // disable the previous projectile type
                DisableLastAttack((int)AttackType.Arrow);

                // set the current spell
                currentSpell = FireFireball;
                activeSpell = AttackType.Fireball;

                // enable the action map for Fireballs
                GetComponent<PlayerInput>().actions.actionMaps[(int)activeSpell].Enable();
            }
            else Debug.Log("fireball already equipped");
        }
    } 

    /// <summary>
    /// returns the current closestEnemy, returns null if AimAssist is disabled
    /// </summary>
    /// <returns></returns>
    public GameObject getClosestEnemy()
    {
        if (!aimAssistRef.activeSelf) return null;
        return aimAssistRef.transform.GetComponent<AimAssist>().FindClosestEnemy(transform.position);
    }

    /*
     Action Functions
     */

    /// <summary>
    /// Shoots an arrow
    /// </summary>
    public void FireArrow()
    {
        ManageMana(-1);
        Debug.Log("shoots arrow");
        Instantiate(ArrowPrefab, transform.position, transform.rotation);
    }

    /// <summary>
    /// Shoots a fireball
    /// </summary>
    public void FireFireball()
    {
        ManageMana(-1);
        Debug.Log("shoots fireball"); 
        Instantiate(FireballPrefab, transform.position, transform.rotation);
    }

    /// <summary>
    /// Swings a Sword
    /// </summary>
    public void SwingSword()
    {
        Debug.Log("Swoosh");
    }

    public void ManageMana(int costOrGain)
    {
        Mana += costOrGain;
        if (Mana < 0)
        {
            Mana = 0;
        }
        else if (Mana > 4)
        {
            Mana = 4;
        }
        UIManager.Instance.ManageManainUI();
    }

    public int ManageMana()
    {
        return Mana;
    }
}
