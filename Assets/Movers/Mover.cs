using BobbyCarrot.Platforms;
using Cysharp.Threading.Tasks;
using System;
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

		[SerializeField][HideInInspector]
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
			return Platform.array[(int)pos.x][(int)pos.y].Peek().CanExit(this)
				&& Platform.array[(int)(pos.x + direction.x)][(int)(pos.y + direction.y)].Peek().CanEnter(this);
		}


		protected async UniTask Move(Action startMoving = null)
		{
			var pos = transform.position;
			var token = Token;
			float originalSpeed = speed;
			if (Platform.array[(int)pos.x][(int)pos.y].Peek().OnExit(this).Status == UniTaskStatus.Pending
				|| token.IsCancellationRequested || direction == default || speed <= 0) return;

			startMoving?.Invoke();
			pos += direction;
			while (transform.position != pos)
			{
				transform.position = Vector3.MoveTowards(transform.position, pos, speed);
				await UniTask.Delay(delay);
				if (token.IsCancellationRequested) return;
			}
			transform.position = pos;

			if (Platform.array[(int)pos.x][(int)pos.y].Peek().OnEnter(this).Status == UniTaskStatus.Pending
				|| token.IsCancellationRequested || direction == default || speed <= 0) return;

			speed = originalSpeed;
		}
	}
}