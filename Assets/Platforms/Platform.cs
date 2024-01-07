using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public abstract class Platform : MonoBehaviour, IPlatform
	{
		public static ReadOnlyArray<ReadOnlyArray<Stack<IPlatform>>> array { get; private set; }
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			PlayGround.onAwake += async () =>
			{
				int count = 0;
				++PlayGround.taskCount;
				array = Util.NewReadOnlyArray(Main.level.width, Main.level.height, out _, (__, ___) => new Stack<IPlatform>());

				// Hiện animation "Loading x % ...."
				// Dùng count tính %

				for (int x = 0; x < array.Length; ++x)
					for (int y = 0; y < array[0].Length; ++y)
						foreach (var id in Main.level.platforms[x][y])
						{
							if ((++count) % 20 == 0) await UniTask.Yield();
							IPlatform p = null;

							// Sinh p dựa theo id
							if ((4 <= id && id <= 75 && id != 0 && id != 1
							&& id != 2 && id != 3 && id != 21 && id != 34
							&& id != 47 && id != 50 && id != 55 && id != 58)
							|| (id == 84) // Wind
							|| (89 <= id && id <= 95)
							|| (id == 109) // Rock
							|| (id == 119) || (id == 120) // Dragon head and body
							|| (id == 135) // Grass
							|| (id == 141) // Lock
							|| (240 <= id && id <= 244)
							|| (256 <= id && id <= 262)
							|| (269 <= id && id <= 335)) // 269 = Snow
							{
								// Obstacle
							}
							else if ((0 <= id && id <= 3)
							|| (id == 21) || (id == 34) || (id == 47) || (id == 50)
							|| (id == 55) || (id == 58) // Door
							|| (id == 79)
							|| (id == 85) // Wind Stop
							|| (id == 121) // Dragon tail
							|| (176 <= id && id <= 182) // 180=Ice, 181=Start, 182=Exit
							|| (id == 190)
							|| (192 <= id && id <= 239)
							|| (id == 245) || (id == 246) // Water
							|| (251 <= id && id <= 255) // 251,252,253=Water
							|| (263 <= id && id <= 268)
							|| (359 <= id && id <= 361)
							|| (id == 368))
							{
								// Ground
							}
							else if ((id == 76) || (id == 78) || (id == 83) || (id == 86) || (id == 88)
							|| (id == 125) || (id == 143)
							|| (183 <= id && id <= 189) || (id == 191) || (id == 362))
							{
								// Item
							}
							else if (80 <= id && id <= 82)
							{
								// Cloud Grid
							}
							else if (96 <= id && id <= 98)
							{
								// Cloud
							}
							else if (id == 99)
							{
								// Ice
							}
							else if (id == 108)
							{
								// Lotus
							}
							else if ((id == 110) || (id == 126) || (id == 127) || (id == 142))
							{
								// Bean Tree Node
							}
							else if (112 <= id && id <= 115)
							{
								// Pin Wheel
							}
							else if (id == 116)
							{
								// Wood
							}
							else if ((id == 128) || (id == 129) || (id == 130) || (id == 159))
							{
								// Block Button
							}
							else if (131 <= id && id <= 134)
							{
								// Block
							}
							else if (136 <= id && id <= 138)
							{
								// Carrot
							}
							else if ((id == 139) || (id == 140))
							{
								// Egg
							}
							else if ((id == 144) || (id == 175))
							{
								// Trap
							}
							else if (145 <= id && id <= 148)
							{
								// Mirror
							}
							else if (149 <= id && id <= 152)
							{
								// Conveyor
							}
							else if (153 <= id && id <= 158)
							{
								// Maze
							}
							else if (id == 160)
							{
								// MowerStation
							}
							else if ((id == 161) || (id == 162))
							{
								// ConveyorButton
							}
							else if ((id == 163) || (id == 164))
							{
								// Maze Button
							}
							else if ((id == 165) || (id == 166))
							{
								// WaterButton
							}
							else if (167 <= id && id <= 174)
							{
								// PinWheelButton
							}
							else if (247 <= id && id <= 250)
							{
								// WaterFlow
							}
							else throw new System.Exception($"Platform ID={id} không hợp lệ !");

							array[x][y].Push(p);
						}

				// Ẩn animation "Loading..."

				--PlayGround.taskCount;
			};
		}


		public virtual bool CanEnter(Mover mover) => true;
		public virtual async UniTask OnEnter(Mover mover) { }
		public virtual bool CanExit(Mover mover) => true;
		public virtual async UniTask OnExit(Mover mover) { }
	}



	public interface IPlatform
	{
		bool CanEnter(Mover mover);

		UniTask OnEnter(Mover mover);

		bool CanExit(Mover mover);

		UniTask OnExit(Mover mover);
	}
}