using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour
{
    [SerializeField] Transform shootSpawnTransform;
    [SerializeField] AimLogic aimLogic;
    [SerializeField] BulletLogic bulletLogic;
    [SerializeField] SelectionPearl selectionPearl;
    [SerializeField] float shootSpeed = 1f;
    [SerializeField] float waitAfterShot = 0.5f;

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
     
        if (Input.GetMouseButtonDown(0) && IsTherePearlSelected())
        {
            FreezeAim();
            UsePearlSelected();
            LaunchBullet();            
        }
    }

    public void SetPearl(SelectionPearl selectionPearl)
    {
        this.selectionPearl = selectionPearl;
    }

    public void SetBullet(BulletLogic bulletLogic)
    { 
        this.bulletLogic = bulletLogic; 
    }


    bool IsTherePearlSelected() => 
        selectionPearl != null;

    void FreezeAim() => 
        aimLogic.WaitThisSeconds(waitAfterShot);

    void UsePearlSelected()
    {
        selectionPearl.Use(this);
        Destroy(selectionPearl.gameObject);
        selectionPearl = null;
    }
    void LaunchBullet()
    {
        bulletLogic.gameObject.SetActive(true);
        bulletLogic.Launch(shootSpawnTransform, aimLogic.GetAimPosition(),shootSpeed);
        bulletLogic = null;
    }
}
