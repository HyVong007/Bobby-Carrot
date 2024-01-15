using UnityEngine;


namespace BobbyCarrot.Movers
{
	[RequireComponent(typeof(Animator))]
	public sealed class Fireball : Mover
	{
		private async void OnEnable()
		{
			while (CanMove()) if (!await Move()) return;
			gameObject.SetActive(false);
		}
	}
}