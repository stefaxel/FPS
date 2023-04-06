using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class Gun : MonoBehaviour
{
    private PlayerInput1 playerInput;

    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;

    [SerializeField] float timeBetweenShots;
    [SerializeField] float spread;
    [SerializeField] float reloadTime;
    [SerializeField] float timeBetweenShooting;
    [SerializeField] int magazineSize;
    [SerializeField] int bulletsPerTap;
    [SerializeField] bool allowHoldButton;

    private int bulletsLeft;
    private int bulletsShot;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private bool isShooting;
    private bool singleShot;
    private bool readyToShoot;
    private bool reloading;
    private bool allowInvoke = true;

    [SerializeField] Camera fpsCamera;

    [SerializeField] TextMeshProUGUI ammoDisplay;

    private void Awake()
    {
        playerInput = new PlayerInput1();

        bulletsLeft = magazineSize;
        readyToShoot = true;

        playerInput.Player.Fire.performed += ctx => StartShot();
        playerInput.Player.Fire.canceled += ctx => EndShot();
        playerInput.Player.Reloading.performed += ctx => Reload();

    }

    private void Update()
    {
        if (allowHoldButton)
        {
            if (isShooting && readyToShoot && !reloading && bulletsLeft >0)
            {
                bulletsShot = 0;

                OnShoot();
            }
        }
        else
        {
            if (singleShot && readyToShoot && !reloading && bulletsLeft > 0)
            {
                bulletsShot = 0;

                OnShoot();
                singleShot = false;
            }

        }
        if(readyToShoot && isShooting && !reloading && bulletsLeft <= 0)
            Reload();

        if (ammoDisplay!= null)
        {
            ammoDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }
    }

    private void StartShot()
    {
        isShooting = true;
        if (!allowHoldButton)
        {
            singleShot = true;
        }
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
        readyToShoot = false;

        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetPoint - firePoint.position;

        float xSpread = UnityEngine.Random.Range(-spread, spread);
        float ySpread = UnityEngine.Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(-xSpread, -ySpread, 0);

        Debug.Log("fire gun");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.transform.forward = directionWithSpread.normalized;
        bullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        bullet.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.up * upwardForce, ForceMode.Impulse);
        Destroy(bullet, 1f);

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if(bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("OnShoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        Debug.Log("reload button pressed");
            reloading = true;
            Invoke("ReloadFinished", reloadTime);
        
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}
