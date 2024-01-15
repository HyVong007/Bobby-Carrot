using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;


namespace BobbyCarrot.Movers
{
	public sealed class Flyer : Mover
	{
		[SerializeField] private SerializableDictionaryBase<Vector3, Sprite> sprites;

		private Vector3 Δdirection;
		public override Vector3 direction
		{
			get => Δdirection;
			set => spriteRenderer.sprite = sprites[Δdirection = value];
		}


		private async void OnEnable()
		{
			while (CanMove()) if (!await Move()) return;
			gameObject.SetActive(false);
			Show<Bobby>(transform.position, direction);
		}
	}
}