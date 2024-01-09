using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public sealed class Ground : Platform
	{
		public enum Type
		{
			Water, Sky, Ice, Exit, WindStop, DragonTail, Land
		}
		public Type type { get; private set; }

		public static Vector3 startPoint { get; private set; }

		private void Awake()
		{
			if (id == 245 || id == 246 || (251 <= id && id <= 253)) type = Type.Water;
			else if (263 <= id && id <= 268) type = Type.Sky;
			else if (id == 180) type = Type.Ice;
			else if (id == 181) startPoint = transform.position;
			else if (id == 182) type = Type.Exit;
			else if (id == 85) type = Type.WindStop;
			else if (id == 121) type = Type.DragonTail;
			else type = Type.Land;
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