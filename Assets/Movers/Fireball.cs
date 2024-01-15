namespace BobbyCarrot.Movers
{
	public sealed class Fireball : Mover
	{
		private async void OnEnable()
		{
			while (CanMove()) if (!await Move()) return;
			gameObject.SetActive(false);
		}
	}
}