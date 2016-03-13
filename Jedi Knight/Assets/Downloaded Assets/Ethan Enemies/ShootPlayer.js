#pragma strict

var bullet : Rigidbody;
var speed = 20;

function Start () {

}

function FixedUpdate () {

if(Time.time % 2 < 0.0005){
	 //Vector3 shoot = bullet.transform.TransformDirection(ethan.position);
	//object should always be spawned facing ethan
 	var blt = Instantiate(bullet, transform.position, transform.rotation);
 	blt.velocity = transform.TransformDirection(Vector3(0,0,speed));
 	Destroy(blt.gameObject,3);

   }
}