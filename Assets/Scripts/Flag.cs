using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Flag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Fly()
    {
        transform.DOMoveY(transform.position.y + 1f, 1f);
        GetComponent<MeshRenderer>().material.DOFade(0, 1f);
    }
    void OnTriggerEnter(Collider other)
    {
        Fly();
        print("Another object has entered the trigger");
    }
}
