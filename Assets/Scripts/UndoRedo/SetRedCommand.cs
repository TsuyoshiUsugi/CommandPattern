using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�e���A���J���[�̐Ԃ̒l���{0.2����R�}���h
/// </summary>
public class SetRedCommand : ICommand
{
	private Material _target;

	public SetRedCommand(Material target)
	{
		_target = target;
	}

	public void Execute()
	{
		var setColor = _target.color;
		setColor.r += 0.2f;
		_target.color = setColor;
	}

	public void Undo()
	{
		var setColor = _target.color;
		setColor.r -= 0.2f;
		_target.color = setColor;
	}
}
