using BobbyCarrot.LevelEditors;
using BobbyCarrot.Movers;
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


		public LevelEditor editor; // Test


		private void Awake()
		{
			Main.level = new(editor.CreateLevelFile());
			Destroy(editor.gameObject);
			Camera.main.aspect = Main.level.width / (float)Main.level.height;
			Camera.main.transform.position = new(Main.level.width / 2f - 0.5f, Main.level.height / 2f - 0.5f, -10);
			Camera.main.orthographicSize = Main.level.height / 2f - 1;

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