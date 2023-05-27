using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BorderDown : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<PlayerMovement>(out var playerMovement) == true)
        {
            Destroy(collision.gameObject);
            _panel.SetActive(true);
        }
    }
}
