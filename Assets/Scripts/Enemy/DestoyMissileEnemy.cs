using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyMissileEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<MissileBoss>(out var missile) == true)
        {
            Destroy(collision.gameObject);
        }
    }
}
