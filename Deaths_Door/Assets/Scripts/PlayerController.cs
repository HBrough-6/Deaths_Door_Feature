using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    // used to look at the mouse position
    private Quaternion _lookRotation;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log("x: " +mousePos.x);
        Debug.Log("y: " + mousePos.y);
        Debug.Log("z: " + mousePos.z);
        Debug.Log(" ");
        /*

        //find the vector pointing from our position to the target
        _direction = (mousePos - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.y));
        
        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 100);*/



    }
}
