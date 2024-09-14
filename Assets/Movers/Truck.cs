using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;


namespace BobbyCarrot.Movers
{
	[RequireComponent(typeof(Animator))]
	public sealed class Truck : Mover
	{
		private UniTask<bool> task;
		public async UniTask<bool> Move(Vector3? direction = null)
		{
			if (task.isRunning()) return true;
			if (direction != null)
			{
				if (direction.Value == default) return true;
				direction.Value.CheckValidDpad();
				this.direction = direction.Value;
			}

			return await (task = base.Move());
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

			Vector3 dir = default;
			if (Input.GetKey(KeyCode.UpArrow)) dir = Vector3.up;
			else if (Input.GetKey(KeyCode.RightArrow)) dir = Vector3.right;
			else if (Input.GetKey(KeyCode.DownArrow)) dir = Vector3.down;
			else if (Input.GetKey(KeyCode.LeftArrow)) dir = Vector3.left;
			if (dir == default || task.isRunning()) return;

			direction = dir;
			if (CanMove()) Move().Forget();
		}
	}
}