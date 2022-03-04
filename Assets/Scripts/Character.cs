using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Hit()
    {
        anim.SetTrigger("Swing");
        // anim.SetBool("IsDrive", true);
        // StartCoroutine(DelayFunc(() =>
        // {
        //     // anim.SetBool("IsDrive", false);
        // }, 4f));
    }

    public void Veldle() {
        anim.SetTrigger("Veldle");
    }

    public void Idle()
    {
         anim.SetTrigger("Idle");
        // anim.SetBool("IsDrive", false);
    }

    public void SetTimeAnim(float time)
    {
        anim.speed = time;
    }
    IEnumerator DelayFunc(Action call, float time)
    {
        yield return new WaitForSeconds(time);
        call();
    }

}
