using BobbyCarrot.Movers;
using UnityEngine;


public class Test : MonoBehaviour
{
	private void Awake()
	{
		Mover m = null;
		print(m is Flyer or Fireball);
	}
}