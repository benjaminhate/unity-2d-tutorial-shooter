using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayController : MonoBehaviour {

	public float timeRayLive;
	private float timeRay;
    private Text scoreText;

    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().scoreText;
    }

    void Update(){
		/* Cela permet de ne pas avoir trop de Clones de Ray qui restent en vie pour aucune raison */
		if (Time.time - timeRay > timeRayLive) { // Si le rayon est en vie depuis plus que le temps autorisé, on le détruit
			Destroy (this.gameObject);
		}
	}

	public void setTimeRay(float time){
		this.timeRay = time;
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.CompareTag ("Ennemy")) { // Si c'est un Ennemy, il est détruit
			coll.GetComponent<EnnemyController>().death();
			Destroy (this.gameObject);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().score += 1;
            scoreText.text = "Score : " + player.GetComponent<PlayerController>().score;
		}
	}

}
