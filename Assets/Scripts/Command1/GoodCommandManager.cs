using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ������̕����g���ꍇ�Apalyer�̍ŏI�s��ς��邱��
/// </summary>
public class GoodCommandManager : MonoBehaviour
{
	private static GoodCommandManager _instance;
	public static GoodCommandManager Instance
	{
		get
		{
			if (_instance == null)
			{
				//����_instance == null�Ȃ�΃V�[������Ƃ��Ă���
				return FindObjectOfType<GoodCommandManager>() as GoodCommandManager;
			}
			return _instance;
		}
	}

	//�N���X�������Ă��̒��ɃR�}���h��˂�����ł���
	//���̕��j�ɂ����ꍇList<List<ICommand>>��茩�₷���Ȃ�B
	//��̓I�ɂ�FrameCommand�Ƃ������O���瓯�t���[���̓���ł��邱�Ƃ�������A���̒���Undo��Excute���s���Ă��邽�߉������Ă��邩��
	//������₷��
	public class FrameCommand
	{
		private List<ICommand> _commandBuffer = new List<ICommand>();

		public IEnumerable<ICommand> CommandBuffer
		{
			get
			{
				return _commandBuffer;
			}
		}

		public void AddCommands(List<ICommand> commands)
		{
			_commandBuffer = commands;
		}

		public void Execute()
		{
			foreach (var command in _commandBuffer)
			{
				command.Execute();
			}
		}

		public void Undo()
		{
			foreach (var command in _commandBuffer)
			{
				command.Undo();
			}
		}

	}

	private List<FrameCommand> _frameCommandBuffer = new List<FrameCommand>();

	public bool Locked;

	private void Awake()
	{
		_instance = this;
	}

	public void AddCommands(List<ICommand> commands)
	{
		var frameCommand = new FrameCommand();
		frameCommand.AddCommands(commands);

		_frameCommandBuffer.Add(frameCommand);
	}

	public void Rewind()
	{
		// ���b�N����Ă���Ƃ��͉������Ȃ�
		if (Locked) return;

		Locked = true;
		StartCoroutine(RewindCoroutine());
	}

	public void PlayBack()
	{
		// ���b�N����Ă���Ƃ��͉������Ȃ�
		if (Locked) return;

		Locked = true;
		StartCoroutine(PlayBackCoroutine());
	}

	private IEnumerator PlayBackCoroutine()
	{
		Debug.Log("Playback Start");
		foreach (var frameCommand in _frameCommandBuffer)
		{
			frameCommand.Execute();
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("Playback End");
		Locked = false;
	}

	private IEnumerator RewindCoroutine()
	{
		Debug.Log("Rewind Start");
		///List���t����Ƃ�BList.Reverse()��List�̒��g��ς��Ă��܂��̂�Enumerable.Revers���g��
		foreach (var frameCommand in Enumerable.Reverse(_frameCommandBuffer))
		{
			frameCommand.Undo();
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("Rewind End");
		Locked = false;
	}
}
