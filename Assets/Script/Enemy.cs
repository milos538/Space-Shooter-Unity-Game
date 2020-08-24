using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    [Header("Enemy Settings")]
    [SerializeField] int health = 500;

    [Header("Laser Settings")]
    [SerializeField] float minTimeBetweenShots = .3f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float shotCounter;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] GameObject laserPrefab;

    [Header("Explosion Settings")]
    [SerializeField] GameObject exolosionVFX;
    [SerializeField] float exolosionDuration = 1.5f;
    [SerializeField] float explosionVolume = 1f;
    public AudioClip explosionSound;
    

    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
    private void Update()
    {
        countDownAndShoot();
    }
    private void countDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0)
        {
            shoot();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }
    private void shoot()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            health -= collision.gameObject.GetComponent<Laser>().getLaserDamage();
            Destroy(collision.gameObject);
            if(health <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionSound, new Vector3(0,0,0), explosionVolume);
                Destroy(gameObject);
                GameObject explosion = Instantiate(exolosionVFX, transform.position, transform.rotation);
                Destroy(explosion, exolosionDuration);
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<SceneLoader>().playerIsDead(collision.gameObject);
        }
    }

}
