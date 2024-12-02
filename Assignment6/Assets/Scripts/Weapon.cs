using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;  // Assign the bullet prefab in the Inspector
    public Transform firePoint;      // Assign a fire point in the Inspector (where the bullet will be instantiated)
    public float bulletSpeed = 20f;  // Speed of the bullet
    public Animator animator;

    void Update()
    {
        if (GameObject.Find("Game Manager").GetComponent<GameManager>().hpCount == 0
            || GameObject.Find("Game Manager").GetComponent<GameManager>().killCount == 10) {
            return;
        } 
        
        // Check if the left mouse button is clicked
        /* if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        } */
    }

    public void OnFire()
    {
        animator.SetTrigger("isAttacking");
        FireBullet();
    }

    void FireBullet()
    {
        // Instantiate the bullet at the fire point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Get the Rigidbody component from the instantiated bullet
        /* Rigidbody rb = bullet.GetComponent<Rigidbody>();
        
        // Set the velocity of the bullet to shoot it forward
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }*/ 

        // Destroy the bullet after 10 seconds
        Destroy(bullet, 10f);
    }
}
