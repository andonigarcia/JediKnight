using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 40f;

	private NetworkView nView;

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		nView = GetComponent<NetworkView>();
		if (nView.isMine)
			Debug.Log("New object instanted by me");
		else
			Debug.Log("New object instantiated by " + info.sender);
	}

	void Update()
	{
		if (nView.isMine)
		{
			InputMovement ();
		}
	}

	void InputMovement()
	{
		Vector3 movement = Vector3.zero;

		if (Input.GetKey (KeyCode.W))
			movement = Vector3.back;
		else if (Input.GetKey (KeyCode.S))
			movement = Vector3.forward;
		else if (Input.GetKey (KeyCode.D))
			movement = Vector3.right;
		else if (Input.GetKey (KeyCode.A))
			movement = Vector3.left;

		foreach (Transform child in gameObject.GetComponentsInChildren<Transform>() )
		{
			if (child.CompareTag ("Saber"))
			{
				child.Rotate(movement * speed * Time.deltaTime);
			}
		}
	}
}