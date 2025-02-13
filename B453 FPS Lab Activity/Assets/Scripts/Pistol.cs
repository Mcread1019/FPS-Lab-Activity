using UnityEngine;

public class Pistol : Weapon
{
    protected override void Start()
    {
        base.Start();
        firePoint = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    protected override void Shoot()
    {
        // Shoot out a raycast that's visible within the game view.
        Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 1f);

        // This fires a raycast from the firepoint in the forward direction, storing the hit data in our hit variable.
        // The length of the raycast is determined by the range of the weapon.
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
        #region Alternative Raycast Method
        //bool hitTarget = Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, range);
        //if (hitTarget)
        //{
        //    Debug.Log(hit.transform.name);
        //}
        #endregion
    }
}