using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDownLvl2 : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<PlayerMovement>(out var playerMovement) == true)
        {
            Destroy(collision.gameObject);
            _panel.SetActive(true);
            Time.timeScale = 0;
        }

        if (collision.transform.TryGetComponent<RangedEnemy>(out var enemyAI) == true)
        {
            Destroy(collision.gameObject);
        }
    }
}
