using BobbyCarrot.Platforms;
using Cysharp.Threading.Tasks;


namespace BobbyCarrot.Movers
{
	public sealed class LotusLeaf : Mover, IPlatform
	{
		public bool CanEnter(Mover mover)
		{
			throw new System.NotImplementedException();
		}


		public UniTask OnEnter(Mover mover)
		{
			throw new System.NotImplementedException();
		}


		public bool CanExit(Mover mover)
		{
			throw new System.NotImplementedException();
		}
		

		public UniTask OnExit(Mover mover)
		{
			throw new System.NotImplementedException();
		}
	}
}