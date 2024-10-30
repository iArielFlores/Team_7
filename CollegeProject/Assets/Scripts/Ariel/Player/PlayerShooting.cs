using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet; // Bullet prefab with Bullet script attached

    // Bullet force
    public float shootForce;

    // Gun stats
    public float timeBetweenShoot, spread, reloadTime, timeBetweenShot;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonShot;

    int bulletsLeft, bulletsShot;

    // Bools
    bool shooting, readyToShoot, reloading;

    // Reference
    public Transform attackPoint; // Position where the bullet spawns

    // Graphics
    public TextMeshProUGUI ammunitionDisplay;

    public bool allowInvoke = true;

    void Awake()
    {
        // Magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        // Set ammo display
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }

    void MyInput()
    {
        // Check if allowed to hold down button and take corresponding input
        if (allowButtonShot)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        // Reload automatically when trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        // Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            // Set bullets shot to 0
            bulletsShot = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        readyToShoot = false;

        // Calculate the direction to shoot based on the player's facing direction
        Vector2 directionWithoutSpread = attackPoint.right; // Uses the attack point’s right direction

        // Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Apply spread to direction
        Vector2 directionWithSpread = directionWithoutSpread + new Vector2(x, y);

        // Instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        // Set bullet velocity for 2D Rigidbody
        currentBullet.GetComponent<Rigidbody2D>().AddForce(directionWithSpread.normalized * shootForce, ForceMode2D.Impulse);

        bulletsLeft--;
        bulletsShot++;

        // Invoke resetShot function
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShoot);
            allowInvoke = false;
        }

        // If more bullets are allowed per tap
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShot);
    }

    private void ResetShot()
    {
        // Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
