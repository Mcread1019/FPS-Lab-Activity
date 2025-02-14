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
    [SerializeField] protected int maxCapacity;

    protected virtual void Shoot()
    {
        // Code to shoot the weapon.
    }
    protected virtual void Reload()
    {
        // Code to reload the weapon.
    }
}
