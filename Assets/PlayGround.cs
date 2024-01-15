using BobbyCarrot.LevelEditors;
using BobbyCarrot.Movers;
using BobbyCarrot.Platforms;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace BobbyCarrot
{
	public sealed class PlayGround : MonoBehaviour
	{
		public static event Action onAwake, onStart;

		/// <summary>
		/// Bắt đầu chạy task thì taskList.Add(TASK_ID)<br/>
		/// Kết thúc task thì taskList.Remove(TASK_ID)
		/// </summary>
		public static readonly List<int> taskList = new();


		public LevelEditor editor; // Test

		public static readonly Dictionary<Item.Type, byte> itemCount = new();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			foreach (Item.Type type in Enum.GetValues(typeof(Item.Type)))
				itemCount[type] = 0;
		}


		private void Awake()
		{
			Main.level = new(editor.CreateLevelFile());
			Destroy(editor.gameObject);
			Camera.main.aspect = Main.level.width / (float)Main.level.height;
			Camera.main.transform.position = new(Main.level.width / 2f - 0.5f, Main.level.height / 2f - 0.5f, -10);
			Camera.main.orthographicSize = Main.level.height / 2f - 1;

			cts?.Dispose();
			cts = new();
			taskList.Clear();
			var keys = itemCount.Keys;
			//foreach (var key in keys) itemCount[key] = 0;

			onAwake();
		}


		private async void Start()
		{
			onStart();

			await UniTask.Yield();
			while (taskList.Count != 0) await UniTask.Yield(); // Đợi tất cả task chạy xong

			// Sinh Bobby tại Ground.startPoint
			// Đăng ký input cho Bobby, game bắt đầu
		}


		private static CancellationTokenSource cts;
		public static CancellationToken Token => cts.Token;
		public static void End()
		{
			cts.Cancel();

		}


		public Mover b;
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Mover.Show<Truck>(new(1, 1), Vector3.right);
			}
		}
	}
}