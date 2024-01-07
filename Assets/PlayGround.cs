using Cysharp.Threading.Tasks;
using System;
using UnityEngine;


namespace BobbyCarrot
{
	public sealed class PlayGround : MonoBehaviour
	{
		public static event Action onAwake;
		/// <summary>
		/// Game đang khởi tạo, task đang chạy thì ++taskCount<br/>
		/// Khởi tạo xong, task kết thúc thì --taskCount<para/>
		/// taskCount == 0 thì game sẵn sàng bắt đầu chơi
		/// </summary>
		public static int taskCount;
		private void Awake()
		{
			onAwake();
		}


		private async void Start()
		{
			await UniTask.Yield();
			while (taskCount != 0) await UniTask.Yield(); // Đợi tất cả task chạy xong

			// Đăng ký input cho Bobby, game bắt đầu
		}
	}
}