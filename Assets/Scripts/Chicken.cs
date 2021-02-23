using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chicken : MonoBehaviour
{
	private Rigidbody2D rb2D;
	private SpriteRenderer sr;
	private Animator anim;
	private bool landed;
	private Vector2 spawn;

	private Vector3 origSprSize;

	public float speed;
	public float rotationSpeed;
	public float jumpheight;
	public Vector2 com;
	
	public AudioClip[] sounds;
	public AudioSource auds;

	void userinput() 
	{
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
			if (!sr.flipX) {
				sr.flipX = true;
			}
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
			if (sr.flipX) {
				sr.flipX = false;
			}
		}
		if (Input.GetKey(KeyCode.UpArrow) && landed) {
			if (!auds.isPlaying) {
				auds.clip = sounds[0];
				auds.Play();
			}
			anim.SetBool("isJumping", true);
			rb2D.velocity = Vector2.up * jumpheight;
			landed = false;
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			Crouch(true);
		}
		else {
			Crouch(false);
		}
		if (Input.GetKey(KeyCode.Space)) {
			if (sr.flipX) {
				transform.position += Vector3.right * speed * 1.2f * Time.deltaTime;
				transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
			}
			if (!sr.flipX) {
				transform.position += Vector3.left * speed * 1.2f * Time.deltaTime;
				transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Corn") {
			if (auds.isPlaying) {
				auds.Stop();
			}
			auds.clip = sounds[1];
			auds.Play();
			speed += 0.25f;
			Destroy(other.gameObject);
		}
		if (other.gameObject.tag == "Portal1") {
			SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
		}
		if (other.gameObject.tag == "Portal2") {
			SceneManager.LoadScene("Level 2", LoadSceneMode.Single);
		}
		if (other.gameObject.tag == "Portal3") {
			SceneManager.LoadScene("Level 3", LoadSceneMode.Single);
		}
		if (other.gameObject.tag == "Portal4") {
			SceneManager.LoadScene("Level 4", LoadSceneMode.Single);
		}
		if (other.gameObject.tag == "Portal5") {
			SceneManager.LoadScene("Level 5", LoadSceneMode.Single);
		}
		if (other.gameObject.tag == "Portal6") {
			SceneManager.LoadScene("Level 6", LoadSceneMode.Single);
		}
		if (other.gameObject.tag == "PortalRest") {
			SceneManager.LoadScene("Rest Room", LoadSceneMode.Single);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Platform") {
			anim.SetBool("isJumping", false);
			landed = true;
		}
		if (col.gameObject.tag == "CloudFish") {
			if (auds.isPlaying) {
				auds.Stop();
			}
			auds.clip = sounds[2];
			auds.Play();
		}
	}

	public void Crouch(bool pressed) {
		if(pressed) {
			this.gameObject.transform.localScale = new Vector3(origSprSize.x, 0.10f, origSprSize.z);
			rb2D.gravityScale = 1.5f;
		}
		else {
			this.gameObject.transform.localScale = origSprSize;
			rb2D.gravityScale = 0.75f;
		}
	}

	public void fail() {
		if (transform.position.y < -12 || transform.position.x > 14 || transform.position.x < -14) {
			if (auds.isPlaying) {
				auds.Stop();
			}
			auds.clip = sounds[2];
			auds.Play();
			transform.position = spawn;
			rb2D.velocity = Vector2.zero;
			rb2D.angularVelocity = 0;
		}
	}

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
		rb2D.centerOfMass = com;
		sr = gameObject.GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		auds = GetComponent<AudioSource>();
		origSprSize = this.gameObject.transform.localScale;
		spawn = new Vector2 (transform.position.x, transform.position.y + .2f);
    }

    void Update()
    {
        userinput();
		fail();
    }
}
