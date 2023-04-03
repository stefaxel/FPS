using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using System;

public class Gun : MonoBehaviour
{
    private PlayerInput1 playerInput;

    public GameObject firePoint;
    public GameObject bulletPrefab;

    private bool isShooting;

    private void Awake()
    {
        playerInput = new PlayerInput1();

        playerInput.Player.Fire.started += ctx => StartShot();
        playerInput.Player.Fire.canceled += ctx => EndShot();
    }

    private void Update()
    {
        if (isShooting)
        {
            OnShoot();
        }
    }

    private void StartShot()
    {
        isShooting = true;
    }

    private void EndShot()
    {
        isShooting = false;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void OnShoot()
    {
        Debug.Log("fire gun");
        //GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation);
        //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 600f);
        //Destroy(bullet, 1f);
    }

}
