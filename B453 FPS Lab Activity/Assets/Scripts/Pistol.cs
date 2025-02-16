using System.Collections;
using UnityEngine;

public class Pistol : Weapon
{
    //ADDED new method
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Shoot()
    {
        //ADD
        if (isReloading)
        {
            Debug.Log("Cannot shoot while reloading");
            return;
        }

        base.Shoot();
        //------------------------------------------------

        //Added to reduce bullet count
        BulletCount--;

        RaycastHit hit;
        Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 1f);
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoUI(BulletCount, maxCapacity);
        }

        if (UIManagerEvent.Instance != null)
        {
            UIManagerEvent.Instance.OnAmmoChangeAction?.Invoke(BulletCount, maxCapacity);
        }

        Debug.Log("Pew pew");
    }
    public override void Reload(int ammoInventory)
    {
        //ADD
        base.Reload(ammoInventory);
        //------------------------------------------------
    }
}
