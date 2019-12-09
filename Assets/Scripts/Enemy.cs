using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float shotCounterMin = 0.2f;
    [SerializeField] float shotCounterMax = 0.7f;
    [SerializeField] float projectileSpeed = 3.0f;
    float shotCounter;

    [Header("Enemy Stats")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float health = 500f;
    [SerializeField] float durationOfDeath = 1f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(shotCounterMin, shotCounterMax);
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;          // shot counter - time that frame takes
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(shotCounterMin, shotCounterMax);
        }
    }

    void Fire()
    {
        GameObject laser = Instantiate(
                    laserPrefab,
                    transform.position,
                    Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    public void Damage(float damageDealt)
    {
        health -= damageDealt;
        if (health <= 0)
            StartCoroutine(Death()); 
    }

    IEnumerator Death()
    {
        Destroy(gameObject);
        var enemyExplosion = Instantiate(deathVFX, gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(durationOfDeath);
        Destroy(enemyExplosion);
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
}
