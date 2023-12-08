using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Brough, Heath
// 12/6/23
// handles the changing of UI

public class UIManager : MonoBehaviour
{
    static public UIManager Instance;

    // used to store the weapon selection elements
    private Transform[] weaponSelectionArr;
    public Transform[] DisplayMana;

    public GameObject attackStatusRef;

    private void Awake()
    {
        // singleton assignment
        Instance = this;

        // get the Parent object of the Weapon Selections in the UI
        GameObject tempVar = GameObject.Find("SelectedWeaponUI");
        
        weaponSelectionArr = new Transform[4];
        DisplayMana = new Transform[4];
        // get each of the Weapon Selections and store them in weaponSelectionArr
        for (int childIndex = 0; childIndex < weaponSelectionArr.Length; childIndex++)
        {
            weaponSelectionArr[childIndex] = tempVar.transform.GetChild(childIndex);
        }

        tempVar = GameObject.Find("MagicChargesCountUI");
        // get each of the Weapon Selections and store them in weaponSelectionArr
        for (int childIndex = 0; childIndex < DisplayMana.Length; childIndex++)
        {
            DisplayMana[childIndex] = tempVar.transform.GetChild(childIndex);
        }

        Instance = this;
    }

    private void Start()
    {
        ChangeWeapon(1);
    }


    // reflects the weapon selection change in the UI
    public void ChangeWeapon(int WeaponInput)
    {
        // loops through the array of UI elements that correspond to the currently selected weapon
        for (int i = 0; i < weaponSelectionArr.Length; i++)
        {
            if (i == WeaponInput-1)
            {
                // if the current index is the selected weapon, set it to red
                weaponSelectionArr[i].gameObject.GetComponent<Image>().color = Color.red;
            }
            else
            {
                // if the current index is not the selected weapon, set it white
                if (weaponSelectionArr[i].gameObject.GetComponent<Image>().color == Color.red)
                {
                    weaponSelectionArr[i].gameObject.GetComponent<Image>().color = Color.white;
                }
            }
        }
    }

    public void ManageManainUI()
    {
        int tempMana = PlayerController.Instance.ManageMana();
        if (tempMana == 0)
        {
            // DisplayMana[0].gameObject.SetActive(false);
        }
        for (int i = 0; i < DisplayMana.Length; i++)
        {
            if (i < tempMana)
            {
                DisplayMana[i].gameObject.SetActive(true);
            }
            else
            {
                DisplayMana[i].gameObject.SetActive(false);
            }
        }
    }
    
    public void AttackStatus(bool readyOrNot)
    {
        if (readyOrNot)
        {
            attackStatusRef.GetComponent<Image>().color = Color.green;
        }
        else
        {
            attackStatusRef.GetComponent<Image>().color = Color.red;
        }
    }
}
