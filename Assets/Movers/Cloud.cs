using BobbyCarrot.Platforms;
using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using UnityEngine;


namespace BobbyCarrot.Movers
{
	public sealed class Cloud : Mover, IPlatform
	{
		public Color color { get; private set; }


		[SerializeField] private SerializableDictionaryBase<Color, Sprite> sprites;
		private new void Awake()
		{
			base.Awake();
			color = Platform.id == 96 ? Color.Red
				: Platform.id == 97 ? Color.Violet : Color.Green;
			spriteRenderer.sprite = sprites[color];
		}


		public bool CanEnter(Mover mover) => mover is Flyer or Fireball ||
			(direction == default && mover is Bobby or Truck);


		public async UniTask OnEnter(Mover mover) { }


		public bool CanExit(Mover mover) => true;


		public async UniTask OnExit(Mover mover) { }


		public async new void Move()
		{
			while (CanMove())
			{
				if (!await base.Move()) return;

				// Quét tìm PinWheel > Cập nhật direction
				var pos = transform.position;
				Vector3 newDir = default;
				if (direction != PinWheel.directions[Color.Yellow])
					foreach (var pinWheel in PinWheel.dict[Color.Yellow])
						if (pinWheel.on && Find(Color.Yellow, pos, pinWheel.index))
						{
							newDir = PinWheel.directions[Color.Yellow];
							break;
						}

				if (direction != PinWheel.directions[color])
					foreach (var pinWheel in PinWheel.dict[color])
						if (pinWheel.on && Find(color, pos, pinWheel.index))
						{
							newDir = PinWheel.directions[color];
							break;
						}

				if (newDir == default) continue;
				if (-newDir == direction)
				{
					// Hướng di chuyển mới đối ngược với hướng hiện tại
					// Mây đứng yên hoặc di chuyển tùy theo các chong chóng đối nghịch
					throw new NotImplementedException();
				}
				else direction = newDir;
			}
			direction = default;

			static bool Find(Color color, in Vector3 pos, in Vector3 wheelPos) => color switch
			{
				Color.Green => pos.x < wheelPos.x && pos.y == wheelPos.y,
				Color.Red => pos.x == wheelPos.x && pos.y < wheelPos.y,
				Color.Violet => pos.x > wheelPos.x && pos.y == wheelPos.y,
				_ => pos.x == wheelPos.x && pos.y > wheelPos.y,
			};
		}
	}
}