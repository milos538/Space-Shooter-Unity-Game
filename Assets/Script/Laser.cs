using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] int laserDamage = 100;

    public int getLaserDamage()
    {
        return laserDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyLaser")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
