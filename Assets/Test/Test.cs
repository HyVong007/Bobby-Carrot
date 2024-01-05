using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour
{
	private void Awake()
	{
		var stack = new Stack<int>();
		stack.Push(2);
		stack.Push(1);
		print(stack.Peek());
	}
}
