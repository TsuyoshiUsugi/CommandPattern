using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
	[SerializeField] MeshRenderer renderer;

	Material meshMaterial
	{
		get
		{
			return renderer.material;
		}
	}

	public void SetBlue()
	{
		var command = new SetBlueCommand(meshMaterial);
		command.Execute();
		RGBCommandManager.Instance.AddCommand(command);
	}

	public void SetRed()
	{
		var command = new SetRedCommand(meshMaterial);
		command.Execute();
		RGBCommandManager.Instance.AddCommand(command);
	}

	public void SetGreen()
	{
		var command = new SetGreenCommand(meshMaterial);
		command.Execute();
		RGBCommandManager.Instance.AddCommand(command);
	}
}
