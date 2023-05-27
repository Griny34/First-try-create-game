using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private int _orderInLayer;

    private void Update()
    {
        if(_player != null)
        {
            transform.position = new Vector3(_player.position.x, transform.position.y, _orderInLayer);
        }
    }
}
