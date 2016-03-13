#pragma strict


var healthBarSlider : UnityEngine.UI.Slider;
var gameOverObject : GameObject[];

function Start () {

}

function Update () {

	if(healthBarSlider.value <=0){
		gameOverObject = GameObject.FindGameObjectsWithTag("GameOver");
		for(g in gameOverObject){
			g.SetActive(true);
		}
	}
	
}


function OnTriggerEnter (other : Collider) {

		if(other.gameObject.tag == "Bullet"){
			//Destroy(other.gameObject);
			healthBarSlider.value -=.02f;

		}
}
