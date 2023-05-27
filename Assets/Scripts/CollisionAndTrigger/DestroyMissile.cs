using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMissile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<MissileMovement>(out var missile) == true)
        {
            Destroy(collision.gameObject);
        }

        if(collision.transform.TryGetComponent<MissileBoss>(out var missileBoss) == true)
        {
            Destroy(collision.gameObject);
        }
    }
}
