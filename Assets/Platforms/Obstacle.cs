using BobbyCarrot.Movers;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public sealed class Obstacle : Platform
	{
		public enum Type
		{
			Normal, Border, Lock, Wind, Snow, Rock, Grass
		}
		public Type type { get; private set; }

		[SerializeField] private SerializableDictionaryBase<Type, RuntimeAnimatorController> anims;
		private void Awake()
		{
			switch (id)
			{
				case 84:
					type = Type.Wind;
					gameObject.AddComponent<Animator>().runtimeAnimatorController = anims[type];
					break;

				case 109:
					type = Type.Rock;
					break;

				case 135:
					type = Type.Grass;
					gameObject.AddComponent<Animator>().runtimeAnimatorController = anims[type];
					break;

				case 141:
					type = Type.Lock;
					break;

				case 269:
					type = Type.Snow;
					break;

				case 374:
					type = Type.Border;
#if !UNITY_EDITOR
					spriteRenderer.enabled = false;
#endif
					break;

				default:
					type = Type.Normal;
					break;
			}
		}


		public override bool CanEnter(Mover mover)
		{
			if (type == Type.Border) return false;

			return true;
		}
	}
}