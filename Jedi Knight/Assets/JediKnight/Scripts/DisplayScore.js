#pragma strict

var score : UnityEngine.UI.Text;
var count : int;

public class DisplayScore extends MonoBehaviour{

function Start () {
	count = 0;
}

function Update () {
	score.text = "Score " + count;
}

public function enemyKilled(){
	count++;
}

}