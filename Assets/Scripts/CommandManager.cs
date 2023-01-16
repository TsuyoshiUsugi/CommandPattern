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
				//�L���X�g���ƃL���X�g�o���Ȃ��ꍇ�G���[���o�邪���̏ꍇ��null��������B�p�t�H�[�}���X�I�ɂ������炵���H
                return FindObjectOfType<CommandManager>() as CommandManager;
            }
            return _instance;
        }
    }

	//���̂����̓v���O���}�[�I�ɂ̓L���C�炵���B�N���X�̕������₷��
	//�����������̂悤�ɂ������R�͓����t���[���œ�̃R�}���h�����͂��ꂽ�ہA������ICommand���Ǝ��̃t���[���ɓ��t���[����
	//���͂������z����Ă��܂�����
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
		///List���t����Ƃ�BList.Reverse()��List�̒��g��ς��Ă��܂��̂�Enumerable.Revers���g��
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
