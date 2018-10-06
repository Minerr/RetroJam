using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHole : MonoBehaviour {
    [SerializeField] private BoxCollider2D _collider;
    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("Vertical") <= -0.1) {
            _collider.enabled = false;
        }
        else {
            _collider.enabled = true;
        }
    }
}