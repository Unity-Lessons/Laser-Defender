using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration parameters
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFiring = 0.2f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject firePoint;

    Coroutine firingCoroutine;

    float xMin, xMax, yMin, yMax;
    bool alive = true;
    
    void Start()
    {
        setUpMoveBoundaries();          // set up boundaries of camera which player can move in
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            Damage(damageDealer.GetDamage());
            Destroy(other.gameObject);
        }
    }

    IEnumerator Death()
    {
        // Play some animation
        alive = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void Damage(int damageDealt)
    {
        health -= damageDealt;
        if (health <= 0)
            StartCoroutine(Death());
    }

    private void setUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;     // what is world space value of x element
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        if (alive)
        {
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * (moveSpeed * .5f);

            var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
            var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

            transform.position = new Vector2(newXPos, newYPos);
        }
    }

    private void Fire()
    {
        if (alive)
        {
            if (Input.GetButtonDown("Fire1"))
                firingCoroutine = StartCoroutine(FireContinuously());

            if (Input.GetButtonUp("Fire1"))
                StopCoroutine(firingCoroutine);
        }
        else
            StopCoroutine(firingCoroutine);
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                    laserPrefab,
                    firePoint.transform.position,
                    Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiring);
        }
    }
}
