#pragma strict

function OnTriggerEnter (other : Collider) {
	if (other.gameObject.name == "Beam") {
		var dsscript : DisplayScore = GameObject.Find("DisplayScoreText").GetComponent(DisplayScore);
		dsscript.enemyKilled();
	}
}