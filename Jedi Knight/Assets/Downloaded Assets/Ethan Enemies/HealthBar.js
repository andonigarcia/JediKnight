#pragma strict


var healthBarSlider : UnityEngine.UI.Slider;
var gameOverObject : GameObject[];
var timeLeft : float;

function Start () {
	gameOverObject = GameObject.FindGameObjectsWithTag("GameOver");

	for(g in gameOverObject){
			g.SetActive(false);
		}
	timeLeft = 10.0f;

}

function Update () {

	if(healthBarSlider.value <=0){

		timeLeft -= Time.deltaTime;
		for(g in gameOverObject){
			g.SetActive(true);
		}


		if (timeLeft <= 0){
			for(g in gameOverObject){
				g.SetActive(false);
			}
			healthBarSlider.value = 1;
			timeLeft = 5.0f;
		}
	}
	
}



function OnTriggerEnter (other : Collider) {

		if(other.gameObject.tag == "Bullet"){
			//Destroy(other.gameObject);
			healthBarSlider.value -=.02f;

		}
}
