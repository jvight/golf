using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjMap : MonoBehaviour
{
    [SerializeField]
    public Transform xMark;
    public int id;
    public bool isMove;
    public bool isX;
    public Vector3 posEnd;
    public bool isRotate;
    public Vector3 angleEnd;
    // Start is called before the first frame update
    void Start()
    {
        // if (isMove)
        // {
        //     Move();
        // }
    }

    Sequence seq;
    Sequence seq2;
    public void Move()
    {
        seq = DOTween.Sequence()
        .SetLoops(-1)
        .SetUpdate(true)
        .Append(transform.DOMove(posEnd, 1f * Time.timeScale))
        .Append(transform.DOMove(transform.position, 1f * Time.timeScale))
        .Play();
    }
    public void Rotate(){
        seq2=DOTween.Sequence()
        .SetLoops(-1)
        .SetUpdate(true)
        .Append(transform.DORotate(angleEnd,2*Time.timeScale))
        .Append(transform.DORotate(transform.eulerAngles,2*Time.timeScale))
        .Play();
    }
    public void Change()
    {
        if (isMove)
        {
            DOTween.Kill(seq);
            // Move();
        }
        if(isRotate){
            DOTween.Kill(seq2);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
