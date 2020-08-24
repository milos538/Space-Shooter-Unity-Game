using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyLaser" || collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);
        }
    }
}
