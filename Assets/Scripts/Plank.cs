using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Plank : MonoBehaviour
{
    public Color colorBase;
    [SerializeField]
    public List<Color> listColorChange = new List<Color>();
    Rigidbody rb;
    public bool isRed = false;
    bool coled = false;
    public bool poured = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // rb.centerOfMass = new Vector3(0, 0.25f, 0);
        // GetComponent<MeshRenderer>().material.color = colorBase;
    }

    public void SetColor(Color color, List<Color> listColor)
    {
        colorBase = color;
        listColorChange = listColor;
        GetComponent<MeshRenderer>().material.color = colorBase;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (!coled && collisionInfo.gameObject.tag == "Plank")
        {
            if (listColorChange.Count > 0)
            {
                GetComponent<MeshRenderer>().material.DOColor(listColorChange[0], 0.6f).OnComplete(() =>
                {
                    GetComponent<MeshRenderer>().material.DOColor(listColorChange[1], 0.6f);
                });
            }
            coled = true;
            poured = true;
            GameController.Instance.PourDone();
        }
        else if (collisionInfo.gameObject.tag == "Ball")
        {
            poured = true;
        }
        // rb.angularVelocity *= 50;
    }
}

