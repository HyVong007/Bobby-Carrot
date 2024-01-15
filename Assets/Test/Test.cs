using BobbyCarrot.Movers;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour
{
	private void Awake()
	{
		var t = typeof(LotusLeaf);
		print(t.GetInterface("IPlatforms"));
	}
}