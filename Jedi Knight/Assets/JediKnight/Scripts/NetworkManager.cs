using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject serverScreenPrefab;
	public GameObject clientScreenPrefab;
	public GameObject clientGUIDScreenPrefab;
	public GameObject clientIPScreenPrefab;

	private const int numConnections = 4;
	private bool useNat;
	private const int portNum = 23400;
	private const string typeName = "JediKnight";
	private const string gameName = "Lightsaber";
	private GameObject[] enemyobj;

	void Start()
	{
		Cardboard.SDK.VRModeEnabled = false;

		enemyobj = GameObject.FindGameObjectsWithTag("EthanEnemies");

		foreach(GameObject g in enemyobj){
			g.SetActive(false);
		}
	}

	private void SpawnPlayer()
	{
		Network.Instantiate(playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);


		foreach(GameObject g in enemyobj){
			g.SetActive(true);
		}
	}

	/********************
	 * Server Side Code *
	 ********************/
	public void serverButtonClick ()
	{
		if (!Network.isClient && !Network.isServer)
		{
			GameObject menu = GameObject.FindGameObjectWithTag ("Main Menu");
			Destroy (menu);
			StartServer ();
		}
	}

	private void StartServer()
	{
		useNat = !Network.HavePublicAddress ();
		Network.InitializeServer(numConnections, portNum, useNat);
		MasterServer.RegisterHost(typeName, gameName);
	}

	// If StartServer is successful, this will be called
	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");

		GameObject serverScreen = Object.Instantiate (serverScreenPrefab);
		foreach (Text child in serverScreen.GetComponentsInChildren<Text>())
		{
			if (child.CompareTag ("ServerInfo"))
			{
				if (useNat)
				{
					child.text = "GUID: " + Network.player.guid;
				}
				else
				{
					child.text = "IP: " + Network.player.externalIP + "\n" + "Host: " + Network.player.externalPort;
				}
			}
		}
	}

	// Not 100% that this is the right function
	void OnPlayerConnected(NetworkPlayer player)
	{
		Debug.Log ("Player joined the network!");
		GameObject serverScreen = GameObject.FindGameObjectWithTag ("Server Screen");
		Destroy (serverScreen);
		Cardboard.SDK.VRModeEnabled = true;
		SpawnPlayer ();
	}


	/********************
	 * Client Side Code *
	 ********************/
	public void clientButtonClick ()
	{
		if (!Network.isClient && !Network.isServer)
		{
			GameObject menu = GameObject.FindGameObjectWithTag ("Main Menu");
			Destroy (menu);

			GameObject clientScreen = Object.Instantiate (clientScreenPrefab);

			foreach (Button child in clientScreen.GetComponentsInChildren<Button>())
			{
				if (child.CompareTag ("GUIDButton"))
				{
					child.onClick.AddListener(() => ClientButtonSelector (true));
				}
				else if (child.CompareTag ("IPButton"))
				{
					child.onClick.AddListener(() => ClientButtonSelector (false));
				}
			}
		}
	}

	public void ClientButtonSelector (bool GUID)
	{
		GameObject menu = GameObject.FindGameObjectWithTag ("Client Screen");
		Destroy (menu);

		GameObject clientScreen;
		if (GUID)
		{
			clientScreen = Object.Instantiate (clientGUIDScreenPrefab);
		}
		else
		{
			clientScreen = Object.Instantiate (clientIPScreenPrefab);
		}

		foreach (Button child in clientScreen.GetComponentsInChildren<Button>())
		{
			if (child.CompareTag ("GUIDButton"))
			{
				child.onClick.AddListener (() => ClientInputSubmit (true));
			}
			else if (child.CompareTag ("IPButton"))
			{
				child.onClick.AddListener (() => ClientInputSubmit (false));
			}
		}
	}

	public void ClientInputSubmit (bool GUID)
	{
		if (GUID)
		{
			GameObject guidScreen = GameObject.FindGameObjectWithTag ("GUID Input");

			InputField guidInput = GameObject.FindGameObjectWithTag ("GUIDInfo").GetComponent<InputField> ();
			Network.Connect (guidInput.text);
		}
		else
		{
			GameObject ipScreen = GameObject.FindGameObjectWithTag ("IP Input");

			InputField ipInput = GameObject.FindGameObjectWithTag ("IPInfo").GetComponent<InputField> ();
			InputField hostInput = GameObject.FindGameObjectWithTag ("HostInfo").GetComponent<InputField> ();
			int hostVal = 0;
			if (int.TryParse (hostInput.text, out hostVal))
				Network.Connect (ipInput.text, hostVal);
			else
				OnFailedToConnect (NetworkConnectionError.IncorrectParameters);
		}
	}

	void OnConnectedToServer()
	{
		GameObject guidScreen = GameObject.FindGameObjectWithTag ("GUID Input");
		GameObject ipScreen = GameObject.FindGameObjectWithTag ("IP Input");
		if (guidScreen != null)
			Destroy (guidScreen);
		else
			Destroy (ipScreen);

		Debug.Log ("We fucking logged into a server!");
	}

	void OnFailedToConnect(NetworkConnectionError error)
	{
		GameObject guidScreen = GameObject.FindGameObjectWithTag ("GUID Input");
		GameObject ipScreen = GameObject.FindGameObjectWithTag ("IP Input");
		if (guidScreen != null)
			Destroy (guidScreen);
		else
			Destroy (ipScreen);

		GameObject clientScreen = Object.Instantiate (clientScreenPrefab);
		foreach (Text child in clientScreen.GetComponentsInChildren<Text>())
		{
			if (child.CompareTag ("ClientInfo"))
			{
				child.text = "Uh oh. We couldn't connect to that server. Let's try this again, does the server phone display a GUID or an IP&Host?";
			}
		}
		foreach (Button child in clientScreen.GetComponentsInChildren<Button>())
		{
			if (child.CompareTag ("GUIDButton"))
			{
				child.onClick.AddListener(() => ClientButtonSelector (true));
			}
			else if (child.CompareTag ("IPButton"))
			{
				child.onClick.AddListener(() => ClientButtonSelector (false));
			}
		}
	}
}