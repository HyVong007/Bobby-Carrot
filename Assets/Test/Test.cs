using BobbyCarrot;
using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class Test : MonoBehaviour
{
	private async void Start()
	{
		//await UniTask.Yield();
		//while (PlayGround.taskList.Count > 0) await UniTask.Yield();

		//await UniTask.Delay(3000);
		//Mover.Show<Bobby>(new Vector3(1, 1), Vector3.left);
		//var b = Mover.GetSingleton<Bobby>();
		//for (int i = 0; i < 10; ++i)
		//{
		//	b.input = Vector3.right;
		//	await UniTask.Yield();
		//}

		//b.input = default;
	}
}