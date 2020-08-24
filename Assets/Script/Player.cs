using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{

    [Header("Player Settings")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] int health = 5;
    [SerializeField] Text textField;

    [Header("Laser Settings")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = .1f;
    [SerializeField] AudioClip laserSound;

    [Header("Explosion Settings")]
    [SerializeField] GameObject exolosionVFX;
    [SerializeField] float exolosionDuration = 1.5f;

    [Header("Health Settings")]
    [SerializeField] AudioClip healthIncrese;
    [SerializeField] float pillVolume = 1f;

    Coroutine firing;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start(){
        moveBoundaries();
    }

    void Update(){
        Move();
        Fire();
    }

    private void Fire(){
        if (Input.GetButtonDown("Fire1")){
            firing = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1")){
            StopCoroutine(firing);
        }
    }

    IEnumerator FireContinuously(){
        while (true){
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(laserSound, this.gameObject.transform.position);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyLaser")
        {
            health -= 1;
            textField.text = health.ToString();
            Destroy(collision.gameObject);
            if (health == 0)
            {
                textField.text = "0";
                FindObjectOfType<SceneLoader>().playerIsDead(gameObject);
            }
        }
        else if (collision.gameObject.tag == "Pill")
        {
            health += 1;
            AudioSource.PlayClipAtPoint(healthIncrese, new Vector3(0,0,0), pillVolume);
            textField.text = health.ToString();
            Destroy(collision.gameObject);
        }
    }

    private void Move(){
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float xPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float yPos = Mathf.Clamp(transform.position.y + deltaY,yMin,yMax);

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
    private void moveBoundaries(){
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }
}