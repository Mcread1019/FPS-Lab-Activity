using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected float firerate;
    [SerializeField] protected int bulletCount;
    [SerializeField] protected int maxCapacity;

    protected virtual void Shoot() { }
    protected virtual void Reload() { }
}
