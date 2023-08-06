using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPearl : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] SpriteRenderer spriteRenderer;
    public PearlCollector pearlCollector;
    PowerSO powerData;

    public void Initialize(PowerSO powerData)
    {
        this.powerData = powerData;
        spriteRenderer.color = powerData.PowerColor;
    }

    public Color GetColor()
    {
        return powerData.PowerColor;
    }
    public PowerSO GetPowerData()
    {
        return powerData;
    }

    public void Use(ShootLogic shootLogic)
    {
        shootLogic.SetBullet(GenerateBullet());
        pearlCollector.SetEmpty();
    }

    BulletLogic GenerateBullet()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, transform.rotation);
        BulletLogic bulletLogic = bulletGO.GetComponent<BulletLogic>();
        bulletLogic.Initialize(powerData);
        bulletGO.SetActive(false);
        return bulletLogic;
    }
}
