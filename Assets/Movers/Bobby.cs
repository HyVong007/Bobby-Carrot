using BobbyCarrot.Platforms;
using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using System.Threading;
using UnityEngine;


namespace BobbyCarrot.Movers
{
	[RequireComponent(typeof(Animator))]
	public sealed class Bobby : Mover
	{
		[SerializeField] private Animator animator;
		[SerializeField] private SerializableDictionaryBase<Vector3, Sprite> sprites;
		[SerializeField] private SerializableDictionaryBase<Vector3, RuntimeAnimatorController> walkAnims, shovelAnims;
		[SerializeField] private RuntimeAnimatorController relaxAnim, dieAnim;


		private void OnEnable()
		{
			animator.runtimeAnimatorController = null;
			spriteRenderer.sprite = sprites[direction];
			WaitRelax();
			// Đăng ký input
		}


		private new void OnDisable()
		{
			base.OnDisable();
			// Hủy input
		}


		private CancellationTokenSource cancelRelax = new();
		[SerializeField] private int delayRelax;
		private async void WaitRelax()
		{
			if (await UniTask.Delay(delayRelax, cancellationToken: CancellationTokenSource
				.CreateLinkedTokenSource(cancelRelax.Token, Token, PlayGround.Token).Token)
				.SuppressCancellationThrow()) return;

			animator.runtimeAnimatorController = relaxAnim;
		}


		private Vector3? input;
		private UniTask task;

		private async UniTask Press()
		{
			cancelRelax.Cancel();
			cancelRelax.Dispose();
			cancelRelax = new();

			while (input != null && CanMove(direction = input.Value))
			{
				if (Platform.Peek(transform.position + direction) is Obstacle p
					&& (p.type == Obstacle.Type.Snow))
				{
					// Shovel
					animator.runtimeAnimatorController = shovelAnims[direction];
					using var token = CancellationTokenSource.CreateLinkedTokenSource(Token, PlayGround.Token);
					await UniTask.Delay((int)(shovelAnims[direction].animationClips[0].length * 3000));
					if (token.IsCancellationRequested) return;
				}

				// Walk
				animator.runtimeAnimatorController = walkAnims[direction];
				if (!await Move()) return;
			}

			// Idle
			animator.runtimeAnimatorController = null;
			spriteRenderer.sprite = sprites[direction];
			WaitRelax();
		}


		public async void Die()
		{

		}


		// Test
		private void Update()
		{
			if (!enableInput)
			{
				input = null;
				return;
			}

			if (Input.GetKey(KeyCode.UpArrow))
			{
				input = Vector3.up;
				if (!task.isRunning()) (task = Press()).Forget();
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				input = Vector3.right;
				if (!task.isRunning()) (task = Press()).Forget();
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				input = Vector3.down;
				if (!task.isRunning()) (task = Press()).Forget();
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				input = Vector3.left;
				if (!task.isRunning()) (task = Press()).Forget();
			}

			if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow)
				|| Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
				input = null;
		}
	}
}