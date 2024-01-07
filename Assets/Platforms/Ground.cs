
namespace BobbyCarrot.Platforms
{
	public sealed class Ground : Platform
	{
		public enum Type
		{
			Water, Sky, Land, Ice, Start, Exit, WindStop, DragonTail
		}
		public Type type { get; private set; }
	}
}