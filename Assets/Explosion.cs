using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour 
{
    private float delay = 3f;
    private float countdown;
    private float radius = 5f;
    private float force = 700f;

    public GameObject explosionEffect;

    private bool hasExploded = false;


    void Start()
    {
        countdown = delay;
    }


    public void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }


    void Explode()
    {
        // create explosion effect at grenade's position
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // get all nearby objects to destroy
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        // loop through each object to find a rigidbody
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Destructible dest = nearbyObject.GetComponent<Destructible>();

            if (dest != null)
            {
                dest.Destroy();
            }
        }

        // get all nearby objects to move 
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        // loop through each object to find a rigidbody
        foreach (Collider nearbyObject in  collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            // add force if object has rigidbody
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(gameObject);
    }
}
