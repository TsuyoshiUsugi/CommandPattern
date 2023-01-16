using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float _speed;

	private void Update()
	{
		if (CommandManager.Instance.Locked)
		{
			return;
		}

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		float r = 0;

		if(Input.GetKey(KeyCode.Z))
        {
			r = 1;
        }
		else if(Input.GetKey(KeyCode.X))
        {
			r = -1;
        }

		var move = new MoveCommand(this.transform, h, v, _speed);
		move.Execute();
		var rotate = new RotateCommand(this.transform, r);
		rotate.Execute();

		List<ICommand> excuteList = new() { move, rotate };

		CommandManager.Instance.AddCommand(excuteList);
	}
}
