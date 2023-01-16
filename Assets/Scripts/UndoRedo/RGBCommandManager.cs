using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBCommandManager : MonoBehaviour
{
	private static RGBCommandManager _instance;
	public static RGBCommandManager Instance
	{
		get
		{
			if (_instance == null)
			{
				//Ç‡Çµ_instance == nullÇ»ÇÁÇŒÉVÅ[ÉìÇ©ÇÁÇ∆Ç¡ÇƒÇ≠ÇÈ
				return FindObjectOfType<RGBCommandManager>() as RGBCommandManager;
			}
			return _instance;
		}
	}

	private List<ICommand> _commandBuffer = new List<ICommand>();

	int nowIndex = 0;

	private void Awake()
	{
		_instance = this;
	}

	public void AddCommand(ICommand command)
	{
		if (_commandBuffer.Count > nowIndex)
		{
			_commandBuffer.RemoveRange(nowIndex, _commandBuffer.Count - nowIndex);
		}

		_commandBuffer.Add(command);
		nowIndex++;
	}

	public void Undo()
	{
		if (nowIndex == 0) return;
		nowIndex--;
		_commandBuffer[nowIndex].Undo();
		Debug.Log("undo :" + nowIndex);
	}

	public void Redo()
	{
		if (_commandBuffer.Count == nowIndex) return;
		_commandBuffer[nowIndex].Execute();
		nowIndex++;
		Debug.Log("Redo :" + nowIndex);
	}

	public void UndoToFirstButton()
    {
		StartCoroutine(nameof(UndoToFirst));
    }

	public IEnumerator UndoToFirst()
    {
        for (int i = nowIndex; i > 0; i--)
        {
			Undo();
			yield return new WaitForSeconds(1f);
        }
	}

	public void RedoToLastButton()
    {
		StartCoroutine(nameof(RedoToLast));
    }

	public IEnumerator RedoToLast()
    {
        for (int i = nowIndex; i < _commandBuffer.Count; i++)
        {
			Redo();
			yield return new WaitForSeconds(1f);
		}
    }
}
