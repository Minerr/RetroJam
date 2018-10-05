using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Weapon weapon;
    private float cooldown = 0f;

    private bool canShoot = true;
    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {

        if (Input.GetAxis("FireMouse1") > 0.5)
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Reload());
        }
    }

    void Fire()
    {
        if (canShoot == true && Time.time >= cooldown)
        {
            cooldown = Time.time + 1f / weapon.FireRate;
            Shoot();
        }
        else
        {
            Debug.Log("i need to reload");
        }
    }

    void Shoot()
    {
        Debug.Log("Shot");
        weapon.BulletCount -= 1;
        if (weapon.BulletCount <= 0)
        {
            canShoot = false;
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(weapon.ReloadSpeed);

        Debug.Log("Done Reloading");
        weapon.BulletCount = weapon.ClipSize;
        canShoot = true;
    }
}
