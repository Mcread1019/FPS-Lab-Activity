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
            //TODO: Decals to be pooled
            SpawnBulletHole(hit, new Ray(firePoint.position, firePoint.forward));
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

    void SpawnBulletHole(RaycastHit hit, Ray ray)
    {
        float positionMultiplier = 0.5f;
        float spawnX = hit.point.x - ray.direction.x * positionMultiplier;
        float spawnY = hit.point.y - ray.direction.y * positionMultiplier;
        float spawnZ = hit.point.z - ray.direction.z * positionMultiplier;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        GameObject spawnedObject = Instantiate(bulletDecals, hit.point, Quaternion.identity);
        Quaternion targetRotation = Quaternion.LookRotation(ray.direction);

        spawnedObject.transform.rotation = targetRotation;
        spawnedObject.transform.SetParent(bulletDecals.transform);
        spawnedObject.transform.Rotate(Vector3.forward, Random.Range(0f, 360f));
        //Destroy(spawnedObject, destroyDelay);
    }
}
