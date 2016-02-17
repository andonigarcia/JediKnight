using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public GameObject playerPrefab;

	private const string typeName = "JediKnight";
	private const string gameName = "Lightsaber";
	private HostData[] hostList;

	void Start()
	{
		Cardboard.SDK.VRModeEnabled = false;
	}

	public void serverButtonClick ()
	{
		if (!Network.isClient && !Network.isServer)
		{
			GameObject menu = GameObject.FindGameObjectWithTag ("Main Menu");
			Destroy (menu);
			Cardboard.SDK.VRModeEnabled = true;
			StartServer ();
		}
	}

	private void StartServer()
	{
		Network.InitializeServer(4, 23400, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	// If StartServer is successful, this will be called
	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
		SpawnPlayer();
	}

//	void OnGUI()
//	{
//		if (!Network.isClient && !Network.isServer)
//		{
//			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
//				StartServer();
//
//			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
//				RefreshHostList();
//
//			if (hostList != null)
//			{
//				for (int i = 0; i < hostList.Length; i++)
//				{
//					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
//						JoinServer(hostList[i]);
//				}
//			}
//		}
//	}
//
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	/* TODO: Either make the GUI display a real host list or
	 * (more likely) have the client input the IP/Host to connect
	 * to.
	 */
	public void clientButtonClick ()
	{
		if (!Network.isClient && !Network.isServer)
		{
			GameObject menu = GameObject.FindGameObjectWithTag ("Main Menu");
			Destroy (menu);
			Cardboard.SDK.VRModeEnabled = true;
			JoinServer (hostList [0]);
		}
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}

	/* Probably don't want to spawn a player but do something with
	 * registering the new client's gyro info and stuff
	 */
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		SpawnPlayer();
	}

	private void SpawnPlayer()
	{
		Network.Instantiate(playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
	}

}
