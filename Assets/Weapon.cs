using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]

public class Weapon : ScriptableObject
{
    public bool AutoFire = false;
    public float DamagePerBullet = 10f;
    public float FireRate = 1f;
    public int ClipSize = 30;
    [Range(0,1)]
    public float Accuracy = 1f;
    public float Recoil = 1f;
    public float BulletSpread = 1f;
    public int BulletsPerShot = 1;
    public float BulletLife = 1f;
    public float BulletSpeed = 1f;

    public float ReloadSpeed = 1f;

    public Vector2 BulletSpawnPoint = new Vector2(0,0);
    public Sprite sprite;
}
