using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPearl : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] SpriteRenderer spriteRenderer;
    Power power;

    public void Initialize(Color color, Power power)
    {
        spriteRenderer.color = color;
        this.power = power;
    }

    public Color GetColor()=> power.ColorToPearl;

    public void Use(ShootLogic shootLogic)=> shootLogic.SetBullet(GenerateBullet());

    BulletLogic GenerateBullet()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, transform.rotation);
        BulletLogic bulletLogic = bulletGO.GetComponent<BulletLogic>();
        bulletLogic.Initialize(spriteRenderer.color, power);
        bulletGO.SetActive(false);
        return bulletLogic;
    }
}
