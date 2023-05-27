using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMissile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent<MissileMovement>(out var missileMovement) == true)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
