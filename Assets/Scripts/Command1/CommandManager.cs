using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    static CommandManager _instance;
    public static CommandManager Instance
    {
        get
        {
            if (_instance == null)
            {
				//キャストだとキャスト出来ない場合エラーが出るがこの場合はnullがかえる。パフォーマンス的にもいいらしい？
                return FindObjectOfType<CommandManager>() as CommandManager;
            }
            return _instance;
        }
    }

	//このやり方はプログラマー的にはキモイらしい。クラスの方が見やすい
	//そもそもこのようにした理由は同じフレームで二つのコマンドが入力された際、ただのICommandだと次のフレームに同フレームの
	//入力が持ち越されてしまうため
	private List<List<ICommand>> _commandBuffer = new();

	public bool Locked;

	private void Awake()
	{
		_instance = this;
	}

	public void AddCommand(List<ICommand> command)
	{
		_commandBuffer.Add(command);
	}

	public void Rewind()
	{
		// ロックされているときは何もしない
		if (Locked) return;

		Locked = true;
		StartCoroutine(RewindCoroutine());
	}

	public void PlayBack()
	{
		// ロックされているときは何もしない
		if (Locked) return;

		Locked = true;
		StartCoroutine(PlayBackCoroutine());
	}

	private IEnumerator PlayBackCoroutine()
	{
		Debug.Log("Playback Start");
		foreach (var command in _commandBuffer)
		{
			foreach (var com in command)
			{
				com.Execute();
			}
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("Playback End");
		Locked = false;
	}

	private IEnumerator RewindCoroutine()
	{
		Debug.Log("Rewind Start");
		///Listを逆からとる。List.Reverse()はListの中身を変えてしまうのでEnumerable.Reversを使う
		foreach (var command in Enumerable.Reverse(_commandBuffer))
		{
            foreach (var com in command)
            {
				com.Undo();
            }
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("Rewind End");
		Locked = false;
	}
}
