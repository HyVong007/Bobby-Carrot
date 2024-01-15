using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	[CreateAssetMenu(fileName ="TruckStation", menuName ="Platforms/TruckStation")]
	public sealed class TruckStation : Platform
	{
		[SerializeField] private SerializableDictionaryBase<bool, Sprite> sprites;
		private bool hasTruck;

		public override Platform Create()
		{
			var p = base.Create() as TruckStation;
			p.sprites = sprites;
			p.hasTruck = id == 375;
			p.sprite = sprites[p.hasTruck];

			return p;
		}


		public override bool CanEnter(Mover mover) =>
			mover is not LotusLeaf and not Cloud
			&& (mover is Flyer or Fireball || (mover is Truck ? !hasTruck :
			!hasTruck || PlayGround.itemCount[Item.Type.Gas] != 0));


		public override async UniTask OnEnter(Mover mover)
		{
			if (mover is Flyer or Fireball || (mover is Bobby && !hasTruck)) return;

			sprite = sprites[hasTruck = !hasTruck];
			mover.gameObject.SetActive(false);
			if (mover is Bobby) Mover.Show<Truck>(mover.transform.position, mover.direction);
			else Mover.Show<Bobby>(mover.transform.position, mover.direction);
		}
	}
}