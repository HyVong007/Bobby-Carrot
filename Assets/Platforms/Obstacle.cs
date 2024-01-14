using BobbyCarrot.Movers;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	[CreateAssetMenu(fileName = "Obstacle", menuName = "Platforms/Obstacle")]
	public sealed class Obstacle : Platform
	{
		public enum Type
		{
			Normal, Border, Lock, Wind, Snow, Rock, Grass
		}
		public Type type { get; private set; }

		[SerializeField] private SerializableDictionaryBase<Type, RuntimeAnimatorController> anims;
		public override Platform Create()
		{
			var p = base.Create() as Obstacle;
			p.anims = anims;

			switch (id)
			{
				case 84:
					p.type = Type.Wind;
					// animation
					break;

				case 109:
					p.type = Type.Rock;
					break;

				case 135:
					p.type = Type.Grass;
					break;

				case 141:
					p.type = Type.Lock;
					break;

				case 269:
					p.type = Type.Snow;
					break;

				case 374:
					p.type = Type.Border;
#if !UNITY_EDITOR
					p.sprite=null;
#endif
					break;

				default:
					p.type = Type.Normal;
					break;
			}

			return p;
		}


		public override bool CanEnter(Mover mover)
		{
			if (type == Type.Border) return false;

			return true;
		}
	}
}