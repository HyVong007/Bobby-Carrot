using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	[CreateAssetMenu(fileName = "WaterButton", menuName = "Platforms/WaterButton")]
	public sealed class WaterButton : Platform
	{
		private static readonly List<WaterButton> buttons = new();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			PlayGround.onAwake += () => buttons.Clear();
		}


		[SerializeField] private SerializableDictionaryBase<bool, Sprite> sprites;
		private bool on;
		public override Platform Create()
		{
			var p = base.Create() as WaterButton;
			p.sprites = sprites;
			p.on = id == 165;
			buttons.Add(p);
			return p;
		}


		public override bool CanEnter(Mover mover) =>
			mover is not LotusLeaf && mover is not Cloud;


		public override async UniTask OnEnter(Mover mover)
		{
			if (on || mover is not Bobby) return;

			foreach (var button in buttons)
			{
				button.on = !button.on;
				button.sprite = sprites[button.on];
			}

			WaterFlow.ChangeState();
		}
	}
}