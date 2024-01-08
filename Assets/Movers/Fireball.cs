namespace BobbyCarrot.Movers
{
	public sealed class Fireball : Mover
	{
		public static Fireball instance { get; private set; }
		private void Awake()
		{
			instance = instance ? throw new System.Exception() : this;
		}


		private async void OnEnable()
		{
			var token = Token;
			while (!token.IsCancellationRequested && CanMove()) await Move();
			if (!token.IsCancellationRequested) gameObject.SetActive(false);
		}
	}
}