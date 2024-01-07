

namespace BobbyCarrot.Platforms
{
	public sealed class Obstacle : Platform
	{
		public enum Type
		{
			Lock, Wind, Snow, Rock, Grass
		}
		public Type type { get; private set; }
	}
}