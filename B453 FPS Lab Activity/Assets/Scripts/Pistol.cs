using UnityEngine;

public class Pistol : Weapon
{
    protected override void Start()
    {
        // Call the base class Start method.
        base.Start();

        // Find the child object named "FirePoint" and get its Transform component.
        firePoint = transform.Find("FirePoint").transform;
    }

    private void Update()
    {
        // Check for input to shoot the weapon.
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    protected override void Shoot()
    {
        // Calculate the target position based on the camera's forward direction and the weapon's range.
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * range;
        Vector3 shootDirection = (targetPosition - firePoint.position).normalized;

        // Shoot out a raycast that's visible within the game view.
        Debug.DrawRay(firePoint.position, shootDirection * range, Color.red, 1f);

        // This fires a raycast from the firepoint in the forward direction, storing the hit data in our hit variable.
        // The length of the raycast is determined by the range of the weapon.
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, shootDirection, out hit, range))
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