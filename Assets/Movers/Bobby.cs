using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine;


namespace BobbyCarrot.Movers
{
	[RequireComponent(typeof(Animator))]
	public sealed class Bobby : Mover, IGamepadControl
	{
		private UniTask task;
		private Vector3 _input;
		public Vector3 input
		{
			get => _input;
			set
			{
				(_input = value).CheckValidDpad();
				if (task.isRunning()) return;

				(task = Check()).Forget();
				async UniTask Check()
				{
					cancelRelax.Cancel();
					cancelRelax.Dispose();
					cancelRelax = new();
					while (input != default && CanMove(direction = input))
					{
						animator.runtimeAnimatorController = walkAnims[direction];
						if (!await Move()) return;
					}

					Idle();
				}
			}
		}


		private CancellationTokenSource cancelRelax = new();
		[SerializeField] private int delayRelax;
		private async void Idle()
		{
			animator.runtimeAnimatorController = null;
			spriteRenderer.sprite = sprites[direction];
			using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancelRelax.Token, Token, PlayGround.Token);
			if (!await UniTask.Delay(delayRelax, cancellationToken: cts.Token).SuppressCancellationThrow())
				animator.runtimeAnimatorController = relaxAnim;
		}


		public async void Die()
		{
			throw new NotImplementedException();
		}


		[SerializeField] private Animator animator;
		[SerializeField] private SerializableDictionaryBase<Vector3, Sprite> sprites;
		[SerializeField] private SerializableDictionaryBase<Vector3, RuntimeAnimatorController> walkAnims, shovelAnims;
		[SerializeField] private RuntimeAnimatorController relaxAnim, dieAnim;

		private void OnEnable()
		{
			Idle();
			// Đăng ký input
		}


		private new void OnDisable()
		{
			base.OnDisable();
			// Hủy input
		}


		// Test
		private void Update()
		{
			if (!enableInput) return;

			if (Input.GetKey(KeyCode.UpArrow)) input = Vector3.up;
			else if (Input.GetKey(KeyCode.RightArrow)) input = Vector3.right;
			else if (Input.GetKey(KeyCode.DownArrow)) input = Vector3.down;
			else if (Input.GetKey(KeyCode.LeftArrow)) input = Vector3.left;

			if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow)
				|| Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
				input = default;
		}
	}
}