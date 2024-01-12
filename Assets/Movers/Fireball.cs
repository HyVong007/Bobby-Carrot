using UnityEngine;
using UnityEngine.AddressableAssets;


namespace BobbyCarrot.Movers
{
	public sealed class Fireball : Mover
	{
		private static Fireball ball;
		private void Awake()
		{
			ball = ball ? throw new System.Exception() : this;
		}


		public static void Show(in Vector3 position, in Vector3 direction)
		{
			if (ball)
			{
				ball.transform.position = position;
				ball.direction = direction;
				ball.gameObject.SetActive(true);
			}
			else
			{
				ball = Addressables.InstantiateAsync("Assets/Movers/Prefab/Fireball.prefab",
					position, Quaternion.identity).WaitForCompletion().GetComponent<Fireball>();
				ball.direction = direction;
				ball.enabled = true;
			}
		}


		private async void OnEnable()
		{
			var token = Token;
			while (!token.IsCancellationRequested && CanMove()) await Move();
			if (!token.IsCancellationRequested) gameObject.SetActive(false);
		}
	}
}