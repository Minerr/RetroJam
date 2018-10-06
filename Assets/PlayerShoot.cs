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
            if (Input.GetAxisRaw("Fire1") > 0.5)
            {
                Fire();
            }
        }
        else
        {
            if (Input.GetAxisRaw("Fire1") > 0.5)
            {
                Fire();
            }
        }

        if (Input.GetButtonDown("Reload"))
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
                if (weapon.BulletsPerShot > 1)
                {
                    MultiShot();
                }
                else
                {
                    Shoot(transform.rotation);
                }
            }
        }
        else
        {
            Debug.Log("i need to reload");
        }
    }

    void MultiShot()
    {

        for (int i = 0; i < weapon.BulletsPerShot; i++)
        {
            float weaponAccuracy = Random.Range(-1 + weapon.Accuracy, 1 - weapon.Accuracy);
            float vinkel =  (weapon.BulletSpread / 2) - (weapon.BulletSpread / (weapon.BulletsPerShot - 1)) * i;

            Quaternion angle = Quaternion.Euler(0, 0, vinkel);
            var angleOffset = transform.rotation * angle;

            Shoot(angleOffset);


        }
    }

    void Shoot(Quaternion angle)
    {
        float weaponAccuracy = Random.Range(-1 + weapon.Accuracy, 1 - weapon.Accuracy);
        Quaternion angleOffset = Quaternion.Euler(0, 0, weaponAccuracy*20);

        var test = new Vector3(weapon.BulletSpawnPoint.x, weapon.BulletSpawnPoint.y, 0);
        var spawnPoint = transform.position + transform.TransformDirection(test);
        var bulletObject = Instantiate(bullet,spawnPoint,angle * angleOffset);

        var bulletScript = bulletObject.GetComponent<BulletBehavior>();
        bulletScript.Damage = weapon.DamagePerBullet;
        bulletScript.Speed = weapon.BulletSpeed;
        bulletScript.lifeTime = weapon.BulletLife;
        bulletScript.SpriteRenderer.sprite = weapon.BulletSprite;

        //Debug.Log("Shot");
        bulletCount -= 1;
        if (bulletCount <= 0)
        {
            canShoot = false;
        }

        Quaternion recoil = Quaternion.Euler(0, 0, weapon.Recoil * 20);
        transform.rotation = transform.rotation * recoil;
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
