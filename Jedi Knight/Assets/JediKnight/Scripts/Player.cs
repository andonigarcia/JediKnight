using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : MonoBehaviour {
	const short LIGHTSABER_MSG = 1234;

	private NetworkView nView;
	private bool inputsEnabled = false;

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		nView = GetComponent<NetworkView>();
	}

	void Update()
	{
		/* Keeps Ethan still */
		gameObject.transform.rotation =  Quaternion.identity;
		gameObject.transform.position =  Vector3.zero;

		if (!nView.isMine)
		{
			if (!inputsEnabled)
			{
				Input.gyro.enabled = true;
				inputsEnabled = true;
			}

			nView.RPC ("ReadMessage", RPCMode.All, Input.gyro.attitude);
		}
	}

	public void InputMovement(Quaternion position)
	{
		/* Converts iOS data to Unity orientation */
		Quaternion conversion = new Quaternion (0, 0, 0.7071f, 0.7071f);

		foreach (Transform child in gameObject.GetComponentsInChildren<Transform>() )
		{
			if (child.CompareTag ("Saber"))
			{
				/* Math to mirror and rotate the position */
				Quaternion res = conversion * position;
				res *= Quaternion.Euler (0, 0, 90);
				res = new Quaternion (-res.x, -res.y, -res.z, res.w);
				res *= Quaternion.Euler (0, 180, 0);
				child.rotation = new Quaternion (res.x, -res.y, -res.z, res.w);
			}
		}
	}

	/******************
	 * Messaging Code *
	 ******************/
	[RPC]
	public void ReadMessage(Quaternion aMsg)
	{
		InputMovement (aMsg);
	}
}