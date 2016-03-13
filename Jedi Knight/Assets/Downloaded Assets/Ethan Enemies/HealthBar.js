#pragma strict


var healthBarSlider : UnityEngine.UI.Slider;

function Start () {

}

function Update () {

	
}


function OnTriggerEnter (other : Collider) {
		//Destroy(other.gameObject);
		healthBarSlider.value -=.02f;
}
