using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // The location in space where the projectiles (or raycast) will be spawned.
    [SerializeField] protected Transform firePoint;

    // How much damage this weapon does.
    [SerializeField] protected float damage;

    // The range of this weapon.
    [SerializeField] protected float range;
    [SerializeField] protected float firerate;
    [SerializeField] protected int bulletCount;
    [SerializeField] protected int maxCapacity;     //TODO Rename to magCapacity to bulletCapacity

    //ADDED Fields --> later to become a magazine class
    [SerializeField] private PlayerController playerController;

    public int BulletCount
    {
        get => bulletCount;
        protected set
        {
            bulletCount = Math.Clamp(value,0,999);
        }
    }

    protected bool isReloading;

    //ADDED method
    protected virtual void Awake()
    {
        try
        {
            // Attempt to find the FirePoint and PlayerController
            firePoint = transform.Find("FirePoint");
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        catch (NullReferenceException e)
        {
            // Log the exception message along with the gameObject name to identify the problem
            Debug.LogError($"Error in {gameObject.name}: {e.Message}");
        }
    }

    //Changed to public virtual void Shoot() to allow for overriding
    public virtual void Shoot()
    {
        if (bulletCount <= 0 && !isReloading)
        {
            isReloading = true;
            Reload(playerController.SpareRounds);
            return;
        }
    }

    //Changed to public virtual void Shoot() to allow for overriding
    public virtual void Reload(int ammoInventory)
    {
        StartCoroutine(ReloadWeapon(ammoInventory));
    }

    //ADDED METHODS

    /**
     * Alternative way to implement fire rate
     *  if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            FireWeapon();
            nextFireTime = Time.time + fireRate; // Set next allowed fire time
        }
     */

    protected IEnumerator FireRate()
    {
        yield return new WaitForSeconds(firerate);
    }

    private IEnumerator ReloadWeapon(int ammoInventory)
    {
        Debug.Log("Reloading...");

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoUI(BulletCount, maxCapacity);
        }

        if (UIManagerEvent.Instance != null)
        {
            UIManagerEvent.Instance.OnAmmoChangeAction?.Invoke(BulletCount, maxCapacity);
        }

        yield return new WaitForSeconds(1.5f);

        if (ammoInventory >= maxCapacity)
        {
            bulletCount = maxCapacity;
            playerController.SpareRounds -= maxCapacity;
            Debug.Log("Reloaded with ammo to spare");
        }
        else
        {
            bulletCount = ammoInventory;
            playerController.SpareRounds = 0;
            Debug.Log("Reloaded -- Spare Rounds Depleted");
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoUI(BulletCount, maxCapacity);
        }

        //Warning if event is null, it will still be invoked
        //And the coroutine will not finish
        if (UIManagerEvent.Instance != null)
        {
            UIManagerEvent.Instance.OnAmmoChangeAction?.Invoke(BulletCount, maxCapacity);
        }

        isReloading = false;
    }
}
