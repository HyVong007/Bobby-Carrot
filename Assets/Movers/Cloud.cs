using BobbyCarrot.Platforms;
using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;


namespace BobbyCarrot.Movers
{
	public sealed class Cloud : Mover, IPlatform
	{
		public Color color { get; private set; }

		[SerializeField] private SerializableDictionaryBase<Color, Sprite> sprites;
		private void Awake()
		{
			color = Platform.id == 96 ? Color.Red
				: Platform.id == 97 ? Color.Violet : Color.Green;
			spriteRenderer.sprite = sprites[color];
		}


		private bool moving;
		public bool CanEnter(Mover mover) => mover is Flyer || mover is Fireball ||
			(!moving && (mover is Bobby || mover is Mower));


		public async UniTask OnEnter(Mover mover) { }


		public bool CanExit(Mover mover) => true;


		public async UniTask OnExit(Mover mover) { }



	}
}