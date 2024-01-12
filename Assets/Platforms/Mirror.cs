using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace BobbyCarrot.Platforms
{
	[CreateAssetMenu(fileName = "Mirror", menuName = "Platforms/Mirror")]
	public sealed class Mirror : Platform
	{
		[SerializeField] private SerializableDictionaryBase<Vector3, Sprite> sprites;
		private Vector3 direction;

		public override Platform Clone()
		{
			var p = base.Clone() as Mirror;
			p.sprites = sprites;

			p.direction = id switch
			{
				145 => new(1, -1),
				146 => new(-1, -1),
				147 => new(1, 1),
				_ => new(-1, 1),
			};
			p.sprite = sprites[p.direction];

			return p;
		}


		public override bool CanEnter(Mover mover) =>
			mover is Flyer || mover is Bobby || mover is Fireball;


		public override async UniTask OnEnter(Mover mover)
		{
			if (mover is not Fireball) return;

			if (mover.direction.x != direction.x && mover.direction.y != direction.y)
				mover.direction = mover.direction.x == 0 ? new(direction.x, 0) : new(0, direction.y);
			else mover.gameObject.SetActive(false);
		}


		public override async UniTask OnExit(Mover mover)
		{
			if (mover is not Bobby) return;

			direction = (direction.x == -1 && direction.y == 1) ? new Vector3(1, 1)
				: (direction.x == 1 && direction.y == 1) ? new Vector3(1, -1)
				: (direction.x == 1 && direction.y == -1) ? new Vector3(-1, -1)
				: new Vector3(-1, 1);
			sprite = sprites[direction];
		}
	}
}