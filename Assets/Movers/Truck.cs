using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;


namespace BobbyCarrot.Movers
{
	[RequireComponent(typeof(Animator))]
	public sealed class Truck : Mover
	{
		[SerializeField] private SerializableDictionaryBase<Vector3, RuntimeAnimatorController> anims;
		[SerializeField] private Animator animator;

		private Vector3 Δdirection;
		public override Vector3 direction
		{
			get => Δdirection;
			set => animator.runtimeAnimatorController = anims[Δdirection = value];
		}


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
			Vector3 dir = default;
			if (Input.GetKey(KeyCode.UpArrow)) dir = Vector3.up;
			else if (Input.GetKey(KeyCode.RightArrow)) dir = Vector3.right;
			else if (Input.GetKey(KeyCode.DownArrow)) dir = Vector3.down;
			else if (Input.GetKey(KeyCode.LeftArrow)) dir = Vector3.left;
			if (dir == default || task.isRunning()) return;

			direction = dir;
			if (CanMove()) (task = Move()).Forget();
		}


		private UniTask task;
	}
}