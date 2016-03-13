// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(Collider))]
public class TeleportEnemies : MonoBehaviour {
	public Transform ethan;
	public Transform prefab;
	private int lastPos = 0;

  private Vector3 startingPosition;

	Vector3[] positionArray = new Vector3[7];


  void Start() {
    startingPosition = transform.localPosition;
	transform.LookAt(ethan);
    SetGazedAt(false);

	positionArray[0] = new Vector3(0.46f,2.17f,1.23f);            
	positionArray[1] = new Vector3(-0.51f,1.99f,1.19f);
	positionArray[2] = new Vector3(-0.79f,2.13f,0.47f);
	positionArray[3] = new Vector3(1.19f,2.46f,0.38f);
	positionArray[4] = new Vector3(0.44f,1.03f,0.78f);
	positionArray[5] = new Vector3(0.23f,3.47f,0.62f);
	positionArray[6] = new Vector3(1.11f,3.3f,0.73f);


  }

  void LateUpdate() {
    Cardboard.SDK.UpdateState();
    if (Cardboard.SDK.BackButtonPressed) {
      Application.Quit();
    }
  }

  public void SetGazedAt(bool gazedAt) {
    GetComponent<Renderer>().material.color = gazedAt ? Color.red : Color.green;
	AudioSource sound = gameObject.GetComponent<AudioSource>();
	//sound.Play();
  }

  public void Reset() {
    transform.localPosition = startingPosition;
  }

  public void ToggleVRMode() {
    Cardboard.SDK.VRModeEnabled = !Cardboard.SDK.VRModeEnabled;
  }


	public void ReSpawn(){

		Instantiate (prefab);
		Vector3 direction = Random.onUnitSphere;
		//direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
		//float distance = 2 * Random.value + 1.5f;
		float howFarAwayIsEthan = Vector3.Distance(ethan.position, this.transform.position);
		transform.localPosition = direction * howFarAwayIsEthan;
		transform.LookAt(ethan);

	}

  public void TeleportRandomly() {

	for (int j = 0; j < 50000000; j++) {
		}
			
	Vector3 direction = Random.onUnitSphere;
    //direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
    //float distance = 2 * Random.value + 1.5f;
	//float howFarAwayIsEthan = Vector3.Distance(ethan.position, this.transform.position);
	//transform.localPosition = direction * howFarAwayIsEthan;
	int i =  (int)Mathf.Floor(Random.Range(0.0f,6.0f));

		while (i == lastPos) {
			i = (int)Mathf.Floor(Random.Range(0.0f,6.0f));
		}
	
	transform.localPosition =positionArray[i];
	lastPos = i;
	

	transform.LookAt(ethan);
	
  }


	public void OnTriggerEnter (Collider other){
		if ((other.gameObject.name == "Beam")) {
			TeleportRandomly ();		

		}


	}


}
