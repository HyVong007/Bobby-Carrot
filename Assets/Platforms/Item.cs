

namespace BobbyCarrot.Platforms
{
	public sealed class Item : Platform
	{
		public enum Type
		{
			Shoe, Glass, Key, YellowMap, BlueMap, Speaker, MusicNote, Bean, Shovel,
			Gas, Kite, GoldenCarrot, Coin, Gun
		}
		public Type type { get; private set; }
	}
}