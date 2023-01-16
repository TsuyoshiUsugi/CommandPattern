using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�e���A���J���[�̐̒l���{0.2����R�}���h
/// </summary>
public class SetBlueCommand : ICommand
{
	private Material _target;

	public SetBlueCommand(Material target)
	{
		_target = target;
	}

	public void Execute()
	{
		var setColor = _target.color;
		setColor.b += 0.2f;
		_target.color = setColor;
	}

	public void Undo()
	{
		var setColor = _target.color;
		setColor.b -= 0.2f;
		_target.color = setColor;
	}
}
