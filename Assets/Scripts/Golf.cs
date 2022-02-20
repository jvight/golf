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
    Vector3 oldPos;
    Vector3 oldAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldPos = transform.position;
        // oldAngle = transform.eulerAngles;
    }

    public void ReBack()
    {
        character.SetTimeAnim(1);
        Debug.Log(oldPos);
        transform.position = oldPos;
        // transform.eulerAngles = oldAngle;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(true);
        isShoot = false;
    }

    void Update()
    {
        if (isShoot || GameController.Instance.AmountBall <= 0 || GameController.Instance.GameDone) { return; }
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
                drawTrajectory.UpdateTrajectory(forceV, rb, transform.position);
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
            GameController.Instance.PlayGolf();
            rb.AddForce(new Vector3(Force.x, Math.Abs(Force.y), Math.Abs(Force.y)) * forceMultiplier);
        }, 0.9f));
        isShoot = true;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Plank")
        {
            StartCoroutine(DelayFunc(() =>
            {
                gameObject.SetActive(false);
            }, 0.03f));
            Time.timeScale = 2;
            character.SetTimeAnim(0.5f);
        }
        if (isShoot)
        {
            GameController.Instance.PourDone();
        }
    }
    IEnumerator DelayFunc(Action call, float time)
    {
        yield return new WaitForSeconds(time);
        call();
    }
}
