using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "Obtainable Objects/Weapon")]
public class Weapon : Obtainable
{
    public bool AutoFire = false;
    [Range(0,100)]
    public float DamagePerBullet = 10f;
    [Range(0.1f,50)]
    public float FireRate = 1f;
    [Range(1,100)]
    public int ClipSize = 30;
    [Range(0,1)]
    public float Accuracy = 1f;
    //[Range(0,1)]
    //public float Recoil = 1f;
    [Range(0,180)]
    public float BulletSpread = 1f;
    [Range(1,10)]
    public int BulletsPerShot = 1;
    [Range(0.1f,10)]
    public float BulletLife = 1f;
    [Range(50,200)]
    public float BulletSpeed = 1f;
    [Range(1,10)]
    public float ReloadSpeed = 1f;

    public Vector2 BulletSpawnPoint = new Vector2(0,0);
    public Sprite BulletSprite;
    public AudioClip FireSound;

}
