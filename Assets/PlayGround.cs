using BobbyCarrot.LevelEditors;
using BobbyCarrot.Movers;
using BobbyCarrot.Platforms;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;


namespace BobbyCarrot
{
	public sealed class PlayGround : MonoBehaviour
	{
		public static event Action onAwake, onStart;
		/// <summary>
		/// Sử dụng cho quá trình Game đang khởi tạo <br/> Và cần đợi 1 task nào đó chạy xong mới bắt đầu chơi:<para/>
		/// Bắt đầu task thì ++taskCount<br/>
		/// task kết thúc thì --taskCount<para/>
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

			cts?.Dispose();
			cts = new();
			taskCount = 0;
			onAwake();
		}


		private async void Start()
		{
			onStart();

			await UniTask.Yield();
			while (taskCount != 0) await UniTask.Yield(); // Đợi tất cả task chạy xong

			// Sinh Bobby tại Ground.startPoint
			// Đăng ký input cho Bobby, game bắt đầu

		}


		private static CancellationTokenSource cts;
		public static CancellationToken Token => cts.Token;
		public static void End()
		{
			cts.Cancel();

		}


		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Bobby b = null;
				Platform.Peek(new(3, 1)).OnEnter(b);
			}
		}
	}
}