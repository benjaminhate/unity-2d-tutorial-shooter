using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyGenerator : MonoBehaviour {

	public GameObject ennemyPrefab;

	public int maxEnnemy;
	public int numEnnemy;

	public float timeLastEnnemy;
	public float ennemyRate;

	void Start() {
		// On initialise la valeur pour faire apparaître des Ennemy directement
		timeLastEnnemy = Time.time - ennemyRate;
		numEnnemy = 0;
	}
	
	void Update () {
        numEnnemy = GameObject.FindGameObjectsWithTag("Ennemy").Length;
		if (Time.time - timeLastEnnemy > ennemyRate && numEnnemy < maxEnnemy) {
			/* Si on a attendu suffisamment de temps et que on est pas au max d'Ennemy, on crée un nouvel Ennemy */
			createEnnemy ();
		}
	}

	void createEnnemy(){
		numEnnemy += 1;
		timeLastEnnemy = Time.time;
		int randX;
		int randY;
		Vector2 pos;
		Vector2 playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		do{ // Tant que la position d'apparition de l'Ennemy est trop proche par rapport au joueur on regénère des positions aléatoires
			randX=Random.Range(-10,10);
			randY=Random.Range(-10,10);
			pos = new Vector2 (randX, randY);
		} while(Vector2.Distance(pos,playerPos) < 5);
		Instantiate(ennemyPrefab,pos,Quaternion.identity);
	}

    public int getNumEnnemy()
    {
        return numEnnemy;
    }
}
