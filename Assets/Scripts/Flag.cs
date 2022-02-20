using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Flag : MonoBehaviour
{
    // public Transform flag;
    public Material yellowFlag;
    public MeshRenderer meshFlag;
    Animation anim;
    public bool isFly = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeColor() {
        meshFlag.material = yellowFlag;
    }

    void Fly()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        isFly = true;
        anim.Play("Flag");
        GameController.Instance.UpdateFlagFly();
        // flag.DOMoveY(flag.position.y + 1f, 1f);
        // flag.GetComponent<MeshRenderer>().material.DOFade(0, 1f);
    }
    void OnTriggerEnter(Collider other)
    {
        Fly();
    }
}
