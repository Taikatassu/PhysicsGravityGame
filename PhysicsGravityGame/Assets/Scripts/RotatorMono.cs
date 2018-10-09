using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorMono : MonoBehaviour {

    public float rotationSpeed;
    private Transform t;

    private void Start() {
        t = transform;
    }

    void Update() {
        t.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
