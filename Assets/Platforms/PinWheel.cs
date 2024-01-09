using BobbyCarrot.Movers;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	[RequireComponent(typeof(Animator))]
	public sealed class PinWheel : Platform
	{
		private static readonly Dictionary<Color, List<PinWheel>> dict = new()
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
					foreach (Color color in Enum.GetValues(typeof(Color))) dict[color].Clear();
				};
		}


		public Color color { get; private set; }

		[SerializeField] private SerializableDictionaryBase<Color, Sprite> sprites;
		[SerializeField] private SerializableDictionaryBase<Color, RuntimeAnimatorController> anims;
		[SerializeField] private Animator animator;
		private void Awake()
		{
			color = id == 112 ? Color.Yellow
				: id == 113 ? Color.Red
				: id == 114 ? Color.Green : Color.Violet;
			spriteRenderer.sprite = sprites[color];
			dict[color].Add(this);
		}


		public override bool CanEnter(Mover mover) => mover is Flyer || mover is Fireball;


		public static void ChangeState(Color color, bool on)
		{
			foreach (var pinWheel in dict[color])
			{

			}
		}
	}
}