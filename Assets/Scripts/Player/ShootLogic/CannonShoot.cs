using System;
using UnityEngine;

public class CannonShoot: IShootLogic
{
    [SerializeField] Transform shootSpawnTransform;
    [SerializeField] BulletLogic bulletLogic;
    [SerializeField] SelectionPearl selectionPearl;
    [SerializeField] float shootSpeed = 1f;
    [SerializeField] float waitAfterShot = 0.5f;
    [SerializeField] float distance = 2f;
    [SerializeField] AudioSource shootAudioSource;
    [SerializeField] ParticleSystem particleSystem;

    public event Action<float> OnShoot;
    float shootTimer;

    private void Update()
    {
        shootTimer -= Time.deltaTime;
    }

    public void Shoot()
    {

        if (IsTherePearlSelected() && shootTimer<0)
        {
            InvokeOnShoot();
            UsePearlSelected();
            LaunchBullet();
            shootAudioSource.Play();
            particleSystem.Play();
        }
    }

    public override void  SetPearl(SelectionPearl selectionPearl)
    {
        this.selectionPearl = selectionPearl;
    }

    public override void SetBullet(BulletLogic bulletLogic)
    {
        this.bulletLogic = bulletLogic;
    }


    bool IsTherePearlSelected() =>
        selectionPearl != null;

    void InvokeOnShoot()
    {
        OnShoot?.Invoke(waitAfterShot);
        shootTimer = waitAfterShot;
    }

    void UsePearlSelected()
    {
        selectionPearl.Use(this);
        Destroy(selectionPearl.gameObject);
        selectionPearl = null;
    }
    void LaunchBullet()
    {
        bulletLogic.gameObject.SetActive(true);
        bulletLogic.Launch(shootSpawnTransform, shootSpawnTransform.position + shootSpawnTransform.up * distance, shootSpeed);
        bulletLogic = null;
    }
}
