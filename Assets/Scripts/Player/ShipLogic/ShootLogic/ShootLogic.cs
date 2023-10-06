using UnityEngine;

public class ShootLogic :  IShootLogic
{
    [SerializeField] Transform shootSpawnTransform;
    [SerializeField] AimLogic aimLogic;
    [SerializeField] BulletLogic bulletLogic;
    [SerializeField] SelectionPearl selectionPearl;
    [SerializeField] float shootSpeed = 1f;
    [SerializeField] float waitAfterShot = 0.5f;
    [SerializeField] AudioSource shootAudioSource;
    [SerializeField] ParticleSystem particleSystem;
    
    public void Shoot()
    {     
        if (IsTherePearlSelected())
        {
            FreezeAim();
            UsePearlSelected();
            LaunchBullet();       
            shootAudioSource.Play();
            particleSystem.Play();
        }
    }

    public override void SetPearl(SelectionPearl selectionPearl)
    {
        this.selectionPearl = selectionPearl;
    }

    public override void SetBullet(BulletLogic bulletLogic)
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

public abstract class IShootLogic: MonoBehaviour
{
    public abstract void SetBullet(BulletLogic bulletLogic);
    public abstract void SetPearl(SelectionPearl selectionPearl);
}