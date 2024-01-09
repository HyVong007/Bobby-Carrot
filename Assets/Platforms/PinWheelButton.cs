using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public sealed class PinWheelButton : Platform
	{
		private static readonly IReadOnlyDictionary<Color, List<PinWheelButton>> dict
			= new Dictionary<Color, List<PinWheelButton>>
			{
				[Color.Yellow] = new List<PinWheelButton>(),
				[Color.Red] = new List<PinWheelButton>(),
				[Color.Green] = new List<PinWheelButton>(),
				[Color.Violet] = new List<PinWheelButton>()
			};

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			PlayGround.onAwake += () =>
			{
				foreach (Color color in Enum.GetValues(typeof(Color))) dict[color].Clear();
			};
		}


		private Color color;
		private bool on;
		[SerializeField] private SerializableDictionaryBase<Color, SerializableDictionaryBase<bool, Sprite>> sprites;

		private void Awake()
		{
			switch (id)
			{
				case 167:
					color = Color.Yellow;
					on = true;
					break;

				case 168:
					color = Color.Yellow;
					on = false;
					break;

				case 169:
					color = Color.Red;
					on = true;
					break;

				case 170:
					color = Color.Red;
					on = false;
					break;

				case 171:
					color = Color.Green;
					on = true;
					break;

				case 172:
					color = Color.Green;
					on = false;
					break;

				case 173:
					color = Color.Violet;
					on = true;
					break;

				case 174:
					color = Color.Violet;
					on = false;
					break;
			}

			spriteRenderer.sprite = sprites[color][on];
			dict[color].Add(this);
		}


		public override bool CanEnter(Mover mover) => mover is not LotusLeaf && mover is not Cloud;


		public override async UniTask OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is Fireball) return;

			foreach (var button in dict[color])
				button.spriteRenderer.sprite = sprites[color][button.on = !button.on];

			PinWheel.ChangeState(color, on);
		}
	}
}