using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private SpawnerMissile _spawnerMissile;

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        _playerMovement.Move(moveX);
        _playerMovement.CheckSide(moveX);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerMovement.JumpPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _spawnerMissile.Create();
        }
    }
}
