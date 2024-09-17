using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;


namespace BobbyCarrot.Movers
{
	[RequireComponent(typeof(Animator))]
	public sealed class Truck : Mover, IGamepadControl
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
					while (input != default && CanMove(direction = input))
					{
						animator.runtimeAnimatorController = anims[direction];
						if (!await Move()) return;
					}
				}
			}
		}


		private Vector3 _direction;
		public override Vector3 direction
		{
			get => _direction;
			protected set => animator.runtimeAnimatorController = anims[_direction = value];
		}


		[SerializeField] private SerializableDictionaryBase<Vector3, RuntimeAnimatorController> anims;
		[SerializeField] private Animator animator;

		private void OnEnable()
		{
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