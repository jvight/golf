using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Plank : MonoBehaviour
{
    public GameObject TextAddScore;
    public Color colorChange;
    Rigidbody rb;
    bool coled = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // rb.centerOfMass = new Vector3(0, 0.25f, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnCreateAddScore()
    {
        GameObject text = Instantiate(TextAddScore);
        text.transform.parent = transform;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (!coled && collisionInfo.gameObject.tag == "Plank")
        {
            GetComponent<MeshRenderer>().material.DOColor(colorChange, 1f);
            coled = true;
        }
        // rb.angularVelocity *= 50;
    }
}

