using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDecoration : MonoBehaviour
{
	private Vector2 spawn;

	public float limit;
	public float speed;

	
    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= limit) {
			transform.position = spawn;
		}
		else {
			transform.position = new Vector2(transform.position.x - speed, transform.position.y);
		}
    }
}
