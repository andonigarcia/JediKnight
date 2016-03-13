using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : MonoBehaviour {

	public float speed = 80f;
	private NetworkView nView;
	const short LIGHTSABER_MSG = 1234;
	private Vector3 prev_pos = Vector3.zero;
	public float Threshold = 0.05f;

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		nView = GetComponent<NetworkView>();
	}

	void Update()
	{
		gameObject.transform.position =  Vector3.zero;

		Debug.Log ("nview isMine: " + nView.isMine.ToString());
		if (nView.isMine)
		{
			//InputMovement (Vector3.zero);
		}
		else
		{
			//Vector3 ourPos = new Vector3 (Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
			Quaternion ourPos = Input.gyro.attitude;
			Debug.Log ("Message: " + ourPos.ToString ());
			nView.RPC ("ReadMessage", RPCMode.All, ourPos);
		}
	}

	public void InputMovement(Quaternion position)
	{
		/*
		Vector3 movement = position;

		if (SystemInfo.deviceType == DeviceType.Desktop) {
			if (Input.GetKey (KeyCode.W))
				movement += Vector3.back;
			else if (Input.GetKey (KeyCode.S))
				movement += Vector3.forward;
			else if (Input.GetKey (KeyCode.D))
				movement += Vector3.right;
			else if (Input.GetKey (KeyCode.A))
				movement += Vector3.left;
		}*/

		foreach (Transform child in gameObject.GetComponentsInChildren<Transform>() )
		{
			if (child.CompareTag ("Saber"))
			{
				/*if (Vector3.Magnitude (movement - prev_pos) > Threshold)
				{
					//child.Rotate ((movement - prev_pos) * speed * Time.deltaTime);
					child.Rotate(movement - prev_pos);
					prev_pos = movement;
				}*/

				child.localRotation = new Quaternion(-position.x, position.z, position.y, -position.w);
				gameObject.transform.rotation =  Quaternion.identity;
			}
		}
	}

	/******************
	 * Messaging Code *
	 ******************/
	[RPC]
	public void ReadMessage(Quaternion aMsg)
	{
		Debug.Log ("The message is: " + aMsg.ToString());
		InputMovement (aMsg);
	}
}