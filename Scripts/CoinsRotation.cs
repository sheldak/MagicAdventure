using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsRotation : MonoBehaviour
{
    public float RotationSpeed;


	void Update ()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * RotationSpeed, 0));
	}
}
