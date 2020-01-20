using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour {

	public float speed;

	private Vector3 direction;

	void Update(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (!player.GetComponent<PlayerController> ().dead) {
			/* La direction à prendre dépend de notre position et de la position du joueur */
			direction = player.transform.position - transform.position;
			/* On normalize la direction pour avoir une vitesse constante quelque soit la distance au joueur */
			transform.position += speed * direction.normalized * Time.deltaTime;
			if (direction != Vector3.zero) {
				/* On calcule l'angle avec le joueur et on se tourne pour faire face au joueur */
				float angle = Mathf.Atan2 (direction.x, direction.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis (angle-90, Vector3.back);
			}
		}
	}

	public void death(){
		EnnemyGenerator gen = GameObject.Find ("EnnemyGenerator").GetComponent<EnnemyGenerator> ();
		if(gen.maxEnnemy==gen.getNumEnnemy())
			gen.timeLastEnnemy = Time.time;
		Destroy (this.gameObject);
	}

}
