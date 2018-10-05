using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    public Weapon weapon;
    private float cooldown = 0f;
    private int bulletCount;
    public GameObject bullet;

    private bool canShoot = true;
    // Use this for initialization
    void Start()
    {
        bulletCount = weapon.ClipSize;
    }

    void Update()
    {
        if (weapon.AutoFire)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Fire();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Fire()
    {
        if (canShoot == true)
        {
            if (Time.time >= cooldown)
            {
                cooldown = Time.time + 1f / weapon.FireRate;
                Shoot();
            }
        }
        else
        {
            Debug.Log("i need to reload");
        }
    }

    void Shoot()
    {
        float test = Random.Range(-1 + weapon.Accuracy, 1 - weapon.Accuracy);
        Debug.Log(test);
        Quaternion test2 = Quaternion.Euler(0, 0, test*20);
        Debug.Log(test2);

        var spawnPoint = transform.position + new Vector3(weapon.BulletSpawnPoint.x, weapon.BulletSpawnPoint.y, 0);
        var bulletObject = Instantiate(bullet,spawnPoint,transform.rotation * test2);

        var bulletScript = bulletObject.GetComponent<BulletBehavior>();
        bulletScript.Damage = weapon.DamagePerBullet;
        bulletScript.Speed = weapon.BulletSpeed;
        bulletScript.lifeTime = weapon.BulletLife;

        //Debug.Log("Shot");
        bulletCount -= 1;
        if (bulletCount <= 0)
        {
            canShoot = false;
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        canShoot = false;
        yield return new WaitForSeconds(weapon.ReloadSpeed);

        Debug.Log("Done Reloading");
        bulletCount = weapon.ClipSize;
        canShoot = true;
    }
}
