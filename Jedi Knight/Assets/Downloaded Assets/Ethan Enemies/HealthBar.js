#pragma strict


var healthBarSlider : UnityEngine.UI.Slider;

function Start () {

}

function Update () {

	
}


function OnTriggerEnter (other : Collider) {

		if(other.gameObject.tag == "Bullet"){
			//Destroy(other.gameObject);
			healthBarSlider.value -=.02f;

		}
}
