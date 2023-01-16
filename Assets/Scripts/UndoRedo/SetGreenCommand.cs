using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マテリアルカラーの緑の値を＋0.2するコマンド
/// </summary>
public class SetGreenCommand : ICommand
{
	private Material _target;

	public SetGreenCommand(Material target)
	{
		_target = target;
	}

	public void Execute()
	{
		var setColor = _target.color;
		setColor.g += 0.2f;
		_target.color = setColor;
	}

	public void Undo()
	{
		var setColor = _target.color;
		setColor.g -= 0.2f;
		_target.color = setColor;
	}
}
