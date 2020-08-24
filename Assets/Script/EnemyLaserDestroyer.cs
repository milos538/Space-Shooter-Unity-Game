using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyLaser")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Pill")
        {
            Destroy(collision.gameObject);
        }
    }
}
