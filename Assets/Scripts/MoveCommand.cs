using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Transform _player;
    float _v;
    float _h;
    float _speed;

    public MoveCommand(Transform player, float h, float v, float speed)
    {
        _player = player;
        _v = v;
        _h = h;
        _speed = speed;
    }

    public void Execute()
    {
        var SetPos = _player.position;
        SetPos.y += _v * Time.deltaTime * _speed;
        SetPos.x += _h * Time.deltaTime * _speed;

        _player.position = SetPos;
    }

    public void Undo()
    {
        var SetPos = _player.position;
        SetPos.y -= _v * Time.deltaTime * _speed;
        SetPos.x -= _h * Time.deltaTime * _speed;

        _player.position = SetPos;
    }
}
