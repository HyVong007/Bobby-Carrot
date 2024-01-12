using BobbyCarrot.Platforms;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;


namespace BobbyCarrot.Movers
{
	[RequireComponent(typeof(SpriteRenderer))]
	public abstract class Mover : MonoBehaviour
	{
		[field: SerializeField] // test
		public virtual Vector3 direction { get; set; }

		public float speed;

		[SerializeField] private int delay;

		[SerializeField]
		[HideInInspector]
		protected SpriteRenderer spriteRenderer;
		private void Reset()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}



		private CancellationTokenSource cts = new();
		public CancellationToken Token => cts.Token;

		protected void OnDisable()
		{
			cts.Cancel();
			cts.Dispose();
			cts = new();
		}


		protected bool CanMove()
		{
			var pos = transform.position;
			return Platform.Peek(pos + direction).CanEnter(this)
				&& (this is IPlatform || Platform.Peek(pos).CanExit(this));
		}


		/// <returns><see langword="true"/>: Kết thúc di chuyển bình thường<br/>
		/// <see langword="false"/>: Mover bị hủy hoặc PlayGround kết thúc hoặc Platform chiếm quyền điều khiển Mover
		///</returns>
		protected async UniTask<bool> Move()
		{
			var pos = transform.position;
			using var token = CancellationTokenSource.CreateLinkedTokenSource(Token, PlayGround.Token);
			float originalSpeed = speed;
			if (this is not IPlatform && (Platform.Peek(pos).OnExit(this).Status == UniTaskStatus.Pending
				|| token.IsCancellationRequested || direction == default || speed <= 0)) return false;

			IPlatform p = null;
			if (this is IPlatform)
			{
				Platform.Pop(pos);
				pos += direction;
				p = Platform.Peek(pos);
				Platform.Push(pos, this as IPlatform);
			}
			else pos += direction;

			while (transform.position != pos)
			{
				transform.position = Vector3.MoveTowards(transform.position, pos, speed);
				await UniTask.Delay(delay);
				if (token.IsCancellationRequested) return false;
			}
			transform.position = pos;

			if ((p ?? Platform.Peek(pos)).OnEnter(this).Status == UniTaskStatus.Pending
				|| token.IsCancellationRequested || direction == default || speed <= 0) return false;

			speed = originalSpeed;
			return true;
		}
	}
}