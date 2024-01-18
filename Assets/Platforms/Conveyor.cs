using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	[CreateAssetMenu(fileName = "Conveyor", menuName = "Platforms/Conveyor")]
	public sealed class Conveyor : Platform
	{
		private static readonly List<Conveyor> conveyors = new();
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			PlayGround.onAwake += () => conveyors.Clear();
		}


		[SerializeField] private SerializableDictionaryBase<Vector3, AnimationData> anims;
		private Vector3 direction;

		public override Platform Create()
		{
			var p = base.Create() as Conveyor;
			p.anims = anims;

			p.direction = id switch
			{
				149 => Vector3.up,
				150 => Vector3.down,
				151 => Vector3.left,
				_ => Vector3.right
			};
			p.animationData = anims[p.direction];
			conveyors.Add(p);
			return p;
		}


		public override bool CanEnter(Mover mover) =>
			mover is not LotusLeaf and not Cloud
			&& (mover is Flyer or Fireball || mover.direction == direction);


		private static UniTask task;
		public override async UniTask OnEnter(Mover mover)
		{
			if (mover is Flyer or Fireball || task.isRunning()) return;
			await (task = Task());

			async UniTask Task()
			{
				Mover.enableInput = false; // Test
				mover.direction = direction;

				while (mover.CanMove())
				{
					if (!await mover.Move()) return;
					if (Peek(mover.transform.position) is not Conveyor) break;
				}

				Mover.enableInput = true;
				Debug.Log("end");
			}
		}


		public static void ChangeState()
		{
			foreach (var conveyor in conveyors)
				conveyor.animationData = conveyor.anims[conveyor.direction = -conveyor.direction];
		}
	}
}