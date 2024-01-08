using UnityEngine;


public class Test : MonoBehaviour
{
	public Rigidbody2D body;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow)) body.MovePosition(transform.position + new Vector3(0, 0.5f));
		else if (Input.GetKeyDown(KeyCode.RightArrow)) body.MovePosition(transform.position + new Vector3(0.5f, 0));
		else if (Input.GetKeyDown(KeyCode.DownArrow)) body.MovePosition(transform.position + new Vector3(0, -0.5f));
		else if (Input.GetKeyDown(KeyCode.LeftArrow)) body.MovePosition(transform.position + new Vector3(-0.5f, 0));
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		print($"Trigger: {collision.name}");
	}
}