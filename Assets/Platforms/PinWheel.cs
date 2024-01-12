using BobbyCarrot.Movers;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	[CreateAssetMenu(fileName = "PinWheel", menuName = "Platforms/PinWheel")]
	public sealed class PinWheel : Platform
	{
		public static readonly IReadOnlyDictionary<Color, IReadOnlyList<PinWheel>> dict = new Dictionary<Color, IReadOnlyList<PinWheel>>
		{
			[Color.Yellow] = new List<PinWheel>(),
			[Color.Red] = new List<PinWheel>(),
			[Color.Green] = new List<PinWheel>(),
			[Color.Violet] = new List<PinWheel>()
		};

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			PlayGround.onAwake += () =>
				{
					foreach (Color color in Enum.GetValues(typeof(Color)))
						(dict[color] as List<PinWheel>).Clear();
				};
		}


		public Color color { get; private set; }

		[SerializeField] private SerializableDictionaryBase<Color, Sprite> sprites;
		[SerializeField] private SerializableDictionaryBase<Color, RuntimeAnimatorController> anims;
		public override Platform Clone()
		{
			var p = base.Clone() as PinWheel;
			p.sprites = sprites;
			p.anims = anims;

			p.color = id == 112 ? Color.Yellow
				: id == 113 ? Color.Red
				: id == 114 ? Color.Green : Color.Violet;
			p.sprite = sprites[p.color];
			(dict[p.color] as List<PinWheel>).Add(p);

			return p;
		}


		public override bool CanEnter(Mover mover) => mover is Flyer || mover is Fireball;


		public static readonly IReadOnlyDictionary<Color, Vector3> directions = new Dictionary<Color, Vector3>
		{
			[Color.Yellow] = Vector3.up,
			[Color.Red] = Vector3.down,
			[Color.Green] = Vector3.left,
			[Color.Violet] = Vector3.right
		};
		[SerializeField] private SerializableDictionaryBase<Color, GameObject> UIs;
		public bool on { get; private set; }


		public void ChangeState(bool on)
		{
			//UIs[color].SetActive(this.on = on);
			//animator.runtimeAnimatorController = on ? anims[color] : null;
			if (!on)
			{
				sprite = sprites[color];
				return;
			}

			// ON STATE
			Vector3 pos = index;
			var dir = directions[color];
			IPlatform platform;

			do
			{
				pos += dir;
				platform = Peek(pos);
				if (platform is not Cloud) continue;

				var cloud = platform as Cloud;
				if (cloud.speed > 0 && cloud.direction == default &&
					(color == Color.Yellow || color == cloud.color))
				{
					cloud.direction = dir;
					cloud.Move();
				}
			} while (platform is not Obstacle || (platform as Obstacle).type != Obstacle.Type.Border);
		}
	}
}