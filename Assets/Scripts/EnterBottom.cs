using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBottom : MonoBehaviour {
    private StairController _stairController;
    [SerializeField] private float Treshold;

    // Use this for initialization
    void Start() {
        _stairController = GetComponentInParent<StairController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (Input.GetAxis("Vertical") > Treshold) {
            _stairController.Show();
        }
        else {
            _stairController.Reset();
        }
    }
}