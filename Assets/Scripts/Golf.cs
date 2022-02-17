using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Golf : MonoBehaviour
{
    public Character character;
    public DrawTrajectory drawTrajectory;
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;
    private Rigidbody rb;

    private bool isShoot = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                mousePressDownPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                mouseReleasePos = touch.position;
                Vector3 force = mousePressDownPos - mouseReleasePos;
                Vector3 forceV = new Vector3(force.x, Math.Abs(force.y), Math.Abs(force.y)) * forceMultiplier;
                if (!isShoot)
                {
                    drawTrajectory.UpdateTrajectory(forceV, rb, transform.position);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                drawTrajectory.HideLine();
                mouseReleasePos = touch.position;
                Shoot(mousePressDownPos - mouseReleasePos);
            }
        }
    }

    private float forceMultiplier = 5f;
    void Shoot(Vector3 Force)
    {
        if (isShoot)
            return;
        character.Hit();
        StartCoroutine(DelayFunc(() =>
        {
            rb.AddForce(new Vector3(Force.x, Math.Abs(Force.y), Math.Abs(Force.y)) * forceMultiplier);
        }, 0.9f));
        isShoot = true;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Plank")
        {
            gameObject.SetActive(false);
            Time.timeScale = 2;
            character.SetTimeAnim(0.5f);
        }
    }
    IEnumerator DelayFunc(Action call, float time)
    {
        yield return new WaitForSeconds(time);
        call();
    }
}
