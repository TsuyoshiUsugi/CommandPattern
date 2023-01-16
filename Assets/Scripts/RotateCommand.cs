using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : ICommand
{
    private Transform _player;
    float _r;

    public RotateCommand(Transform player, float rotate)
    {
        _player = player;
        _r = rotate;
    }

    public void Execute()
    {
        _player.Rotate(new Vector3(0, 0, _r));
    }

    public void Undo()
    {
        _player.Rotate(new Vector3(0, 0, -_r));
    }
}
