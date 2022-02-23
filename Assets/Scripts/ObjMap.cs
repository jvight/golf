using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjMap : MonoBehaviour
{
    [SerializeField]
    public int id;
    public bool isMove;
    public Vector3 posEnd;
    // Start is called before the first frame update
    void Start()
    {
        // if (isMove)
        // {
        //     Move();
        // }
    }

    Sequence seq;
    public void Move()
    {
        seq = DOTween.Sequence()
        .SetLoops(-1)
        .SetUpdate(true)
        .Append(transform.DOMove(posEnd, 1f * Time.timeScale))
        .Append(transform.DOMove(transform.position, 1f * Time.timeScale))
        .Play();
    }

    public void Change()
    {
        if (isMove)
        {
            DOTween.Kill(seq);
            // Move();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
