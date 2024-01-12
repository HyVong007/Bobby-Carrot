using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	[CreateAssetMenu(fileName = "Ground", menuName = "Platforms/Ground")]
	public sealed class Ground : Platform
	{
		public enum Type
		{
			Water, Sky, Ice, Exit, WindStop, DragonTail, Land
		}
		public Type type { get; private set; }

		public static Vector3 startPoint { get; private set; }


		public override Platform Clone()
		{
			var p = base.Clone() as Ground;

			if (id == 245 || id == 246 || (251 <= id && id <= 253)) p.type = Type.Water;
			else if (263 <= id && id <= 268) p.type = Type.Sky;
			else if (id == 180) p.type = Type.Ice;
			else if (id == 182) p.type = Type.Exit;
			else if (id == 85) p.type = Type.WindStop;
			else if (id == 121) p.type = Type.DragonTail;
			else if (id == 181) startPoint = index;
			else p.type = Type.Land;

			return p;
		}


		public override bool CanEnter(Mover mover) =>
			mover is Flyer || mover is Fireball || ((mover is LotusLeaf) ? (type == Type.Water)
			: (mover is Cloud) ? (type == Type.Sky) : (type == Type.Exit) ? (mover is Bobby)
			: (type != Type.Sky && type != Type.Water));


		public override async UniTask OnEnter(Mover mover)
		{
		}
	}
}