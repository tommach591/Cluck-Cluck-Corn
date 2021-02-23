using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFish : MonoBehaviour
{
	private Vector3 startingPos;
	private Rigidbody2D rb2d;
	private SpriteRenderer sr;
	public Transform rotationCenter;

	public float rotationRadius;
	public float angularSpeed;
	public float stretchx;
	public float stretchy;

	public float posx;
	public float posy;
	public float angle;

	public bool horizontal;
	public bool vertical;
	public float firstlimit; // Unity Axis, not CloudFish Axis
	public float secondlimit; // Unity Axis, not CloudFish Axis
	private bool rotated;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        posx = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius * stretchx;
        posy = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius * stretchy;
		transform.position = new Vector2(posx, posy);
		angle = angle + Time.deltaTime * angularSpeed;
		if (angle >= 360f) {
			angle = 0f;
		}

		if (horizontal) {
			if (transform.position.x > firstlimit && sr.flipX == true) {
				sr.flipX = false;
			}
			if (transform.position.x < secondlimit && sr.flipX == false) {
				sr.flipX = true;
			}
			if (sr.flipX == false) {
				transform.rotation = Quaternion.AngleAxis(1f, Vector3.forward);
			}
			if (sr.flipX == true) {
				transform.rotation = Quaternion.AngleAxis(1f, Vector3.back);
			}
		}
		if (vertical) {
			if (transform.localEulerAngles.z == 0) {
				transform.Rotate(Vector3.forward * 90f);
				rotated = false;
			}
			if (transform.position.y > firstlimit && rotated == true) {
				transform.Rotate(Vector3.forward * 180f);
				rotated = false;
			}
			if (transform.position.y < secondlimit && rotated == false) {
				transform.Rotate(Vector3.forward * -180f);
				rotated = true;
			}
		}
	}
}
