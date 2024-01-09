using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;


namespace BobbyCarrot.Platforms
{
	[RequireComponent(typeof(SpriteRenderer))]
	public abstract class Platform : MonoBehaviour, IPlatform
	{
		public static ReadOnlyArray<ReadOnlyArray<Stack<IPlatform>>> array { get; private set; }
		public static ushort id { get; private set; } = ushort.MaxValue;
		protected static SpriteAtlas atlas;
		[SerializeField]
		[HideInInspector]
		protected SpriteRenderer spriteRenderer;
		protected static Transform anchor { get; private set; }

		private void Reset()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}


		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			atlas = Addressables.LoadAssetAsync<SpriteAtlas>("Assets/Platforms/Texture/Atlas.spriteatlasv2")
				.WaitForCompletion();
			PlayGround.onAwake += () =>
			{
				anchor = new GameObject { name = "Platforms" }.transform;
				array = Util.NewReadOnlyArray(Main.level.width, Main.level.height, out _, (__, ___) => new Stack<IPlatform>());
			};

			PlayGround.onStart += async () =>
			{
				int count = 0;
				++PlayGround.taskCount;

				// Hiện animation "Loading x % ...."
				// Dùng count tính %

				for (int x = 0; x < Main.level.width; ++x)
					for (int y = 0; y < Main.level.height; ++y)
					{
						int sortingOrder = 0;
						foreach (var id in Main.level.platforms[x][y])
						{
							Platform.id = id;
							if ((++count) % 20 == 0) await UniTask.Yield();

							// Sinh p dựa theo id
							string prefab = "";
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
							|| (269 <= id && id <= 335) // 269 = Snow
							|| (id == 374)) // Border
							{
								prefab = "Assets/Platforms/Prefab/Obstacle.prefab";
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
								prefab = "Assets/Platforms/Prefab/Ground.prefab";
							}
							else if ((id == 76) || (id == 78) || (id == 83) || (id == 86) || (id == 88)
							|| (id == 125) || (id == 143)
							|| (183 <= id && id <= 189) || (id == 191) || (id == 362))
							{
								prefab = "Assets/Platforms/Prefab/Item.prefab";
							}
							else if (80 <= id && id <= 82)
							{
								prefab = "Assets/Platforms/Prefab/CloudGrid.prefab";
							}
							else if (96 <= id && id <= 98)
							{
								prefab = "Assets/Movers/Prefab/Cloud.prefab";
							}
							else if (id == 99)
							{
								prefab = "Assets/Platforms/Prefab/Ice.prefab";
							}
							else if (id == 108)
							{
								prefab = "Assets/Movers/Prefab/LotusLeaf.prefab";
							}
							else if ((id == 110) || (id == 126) || (id == 127) || (id == 142))
							{
								prefab = "Assets/Platforms/Prefab/BeanTreeNode.prefab";
							}
							else if (112 <= id && id <= 115)
							{
								prefab = "Assets/Platforms/Prefab/PinWheel.prefab";
							}
							else if (id == 116)
							{
								prefab = "Assets/Platforms/Prefab/Wood.prefab";
							}
							else if ((id == 128) || (id == 129) || (id == 130) || (id == 159))
							{
								prefab = "Assets/Platforms/Prefab/BlockButton.prefab";
							}
							else if (131 <= id && id <= 134)
							{
								prefab = "Assets/Platforms/Prefab/Block.prefab";
							}
							else if (136 <= id && id <= 138)
							{
								prefab = "Assets/Platforms/Prefab/Carrot.prefab";
							}
							else if ((id == 139) || (id == 140))
							{
								prefab = "Assets/Platforms/Prefab/Egg.prefab";
							}
							else if ((id == 144) || (id == 175))
							{
								prefab = "Assets/Platforms/Prefab/Trap.prefab";
							}
							else if (145 <= id && id <= 148)
							{
								prefab = "Assets/Platforms/Prefab/Mirror.prefab";
							}
							else if (149 <= id && id <= 152)
							{
								prefab = "Assets/Platforms/Prefab/Conveyor.prefab";
							}
							else if (153 <= id && id <= 158)
							{
								prefab = "Assets/Platforms/Prefab/Maze.prefab";
							}
							else if (id == 160)
							{
								prefab = "Assets/Platforms/Prefab/MowerStation.prefab";
							}
							else if ((id == 161) || (id == 162))
							{
								prefab = "Assets/Platforms/Prefab/ConveyorButton.prefab";
							}
							else if ((id == 163) || (id == 164))
							{
								prefab = "Assets/Platforms/Prefab/MazeButton.prefab";
							}
							else if ((id == 165) || (id == 166))
							{
								prefab = "Assets/Platforms/Prefab/WaterButton.prefab";
							}
							else if (167 <= id && id <= 174)
							{
								prefab = "Assets/Platforms/Prefab/PinWheelButton.prefab";
							}
							else if (247 <= id && id <= 250)
							{
								prefab = "Assets/Platforms/Prefab/WaterFlow.prefab";
							}
							else throw new System.Exception($"Platform ID={id} không hợp lệ !");

							var p = Addressables.InstantiateAsync(prefab, new(x, y, 0), Quaternion.identity)
								.WaitForCompletion().GetComponent<IPlatform>();

							(p as MonoBehaviour).transform.parent = anchor;
							if (p is Platform)
							{
								(p as Platform).spriteRenderer.sprite = atlas.GetSprite(id.ToString());
								(p as Platform).spriteRenderer.sortingOrder = sortingOrder++;
							}
							array[x][y].Push(p);
						}
					}

				// Ẩn animation "Loading..."

				--PlayGround.taskCount;
			};
		}


		protected void OnDisable()
		{
			var pos = transform.position;
			array[(int)pos.x][(int)pos.y].Pop();
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