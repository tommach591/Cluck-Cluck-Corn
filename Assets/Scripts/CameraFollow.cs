using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject chicken;
	public float smoothSpeed;
	public Vector3 offset;

	public AudioSource music;

	void Start() {
		music = GetComponent<AudioSource>();
		music.volume = 0.05f;
	}
    
    void LateUpdate()
    {
		if (!music.isPlaying) {
			music.Play();
		}
		Vector3 desiredPosition = chicken.transform.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
    }
}
