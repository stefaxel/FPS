using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    //PlayerInput playerInput;

    StarterAssetsInputs playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = transform.root.GetComponent<StarterAssetsInputs>();
        //input = transform.root.GetComponent<StarterAssetsInputs>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Shoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (input.canShoot)
        //{
        //    Shoot();
        //    input.canShoot = false;
        //}
    }

    

    void Shoot()
    {
        Debug.Log("Gun is firing");
    }
    
}
