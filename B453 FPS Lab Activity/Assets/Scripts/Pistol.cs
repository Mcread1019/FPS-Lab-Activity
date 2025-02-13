using UnityEngine;

public class Pistol : Weapon
{
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    protected override void Shoot()
    {
        // Create a local variable to store raycast hit data.
        RaycastHit hit;
        // Shoot out a raycast that's visible within the game view.
        Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 1f);
        // This fires a raycast from the firepoint in the forward direction, storing the hit data in our hit variable.
        // The length of the raycast is determined by the range of the weapon.
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}