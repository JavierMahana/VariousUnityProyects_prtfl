using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {

    public float speed;
	void Update ()
    {
        Move();
	}

    private void Move()
    {
        transform.Translate(new Vector3 (-1,0,0) * Time.deltaTime * speed);
    }
}
