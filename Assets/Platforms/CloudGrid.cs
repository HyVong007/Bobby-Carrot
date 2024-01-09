using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public sealed class CloudGrid : Platform
	{
		private Color color;
		[SerializeField] private SerializableDictionaryBase<Color, Sprite> sprites;
		private void Awake()
		{
			color = id switch
			{
				80 => Color.Red,
				81 => Color.Violet,
				_ => Color.Green
			};

			spriteRenderer.sprite = sprites[color];
		}


		public override bool CanEnter(Mover mover) =>
			mover is Flyer || mover is Fireball || mover is Cloud;


		public override async UniTask OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is Fireball || (mover as Cloud).color != color) return;

			var pos = transform.position;
			var cloud = array[(int)pos.x][(int)pos.y].Pop() as Cloud;
			cloud.direction = default;
			cloud.speed = 0;
			Destroy(this);
			array[(int)pos.x][(int)pos.y].Push(cloud);
		}
	}
}