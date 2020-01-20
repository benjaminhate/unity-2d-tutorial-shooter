using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// variables de vitesse
	public float m_speed;
	public float r_speed;

	// variables de reset
	public float base_x;
	public float base_y;
	public float base_rot;
	public int base_health;
	public int base_score;

	// variables de tir
	public GameObject rayPrefab;
	public float raySpeed;
	public float fireRate;
	private float fireTime;

	// variables de score
	public int score;

	// variables de mort
	public int health;
	public float time_dead;
	public bool dead;
	private float time_of_death;

	// variables de Text
	public Text lifeText;
	public Text scoreText;

	void Start(){
		playerLive ();
		resetPlayer ();
	}

	void Update(){
		if (dead && Time.time - time_of_death > time_dead) {
			playerLive ();
		}
	}

	void FixedUpdate()
	{
		if (!dead) { // On peut se déplacer et tirer seulement si on est pas mort
			deplacement ();
			interactions ();
		}
	}

	private void deplacement(){
		// Récupération des touches haut
		if (Input.GetKey(KeyCode.UpArrow))
			transform.Translate(Vector2.up * m_speed);
		if (Input.GetKey(KeyCode.DownArrow))
			transform.Translate(Vector2.down * m_speed);

		// Récupération des touches gauche et droite
		if (Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(Vector3.forward * r_speed);
		if (Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.forward * -r_speed);
	}

	private void interactions(){
		if (Time.time - fireTime > fireRate && Input.GetKey (KeyCode.Space)) {
			/* Si on a attendu suffisamment de temps et que Espace est appuyée, on tire */
			shoot ();
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.CompareTag ("Hole")) { // Si c'est un trou, on meurt
			playerDie ();
		}
		if (coll.CompareTag ("Ennemy")) { // Si c'est un Ennemy, on perd une vie et l'Ennemy est détruit
			coll.GetComponent<EnnemyController>().death();
			health -= 1;
			changeText();
			if (health == 0)
			{
				playerDie();
			}
		}
	}

	void shoot(){
		fireTime = Time.time;
		GameObject rayInstance = Instantiate (rayPrefab, transform.position, transform.rotation);
		/* On ajoute une vitesse au rayon et un temps d'apparition qui sert dans le script RayController */
		rayInstance.GetComponent<Rigidbody2D> ().velocity = raySpeed * transform.up;
		rayInstance.GetComponent<RayController> ().setTimeRay (Time.time);
	}

	void playerDie(){
		dead = true;
		time_of_death = Time.time;
		/* On désactive le visuel du joueur -> il devient invisible */
		GetComponent<Renderer> ().enabled = false;
		resetPlayer ();
	}

	void playerLive(){
		dead = false;
		GetComponent<Renderer> ().enabled = true;
	}

	void changeText()
	{
		lifeText.text = "Vie : " + health;
		scoreText.text = "Score : " + score;
	}

	void resetPlayer(){
		this.transform.position = new Vector2 (base_x, base_y);
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, base_rot));
		fireTime = Time.time - fireRate;
		health = base_health;
		score = base_score;
		changeText();
	}
}
