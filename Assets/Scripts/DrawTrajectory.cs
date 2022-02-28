using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    public Transform dotParent;
    // [Range(3, 30)]
    private int _lineSegmentCount = 24;
    private List<Vector3> _linePoint = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _lineSegmentCount; i++)
        {
            float scl = i;
            GameObject dot = Instantiate(ballPrefab);
            dot.transform.parent = dotParent;
            dot.transform.localScale -= new Vector3(scl / 100   , scl / 100, scl / 100);
            dot.SetActive(false);
        }
    }

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rb, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rb.mass) * Time.fixedDeltaTime;
        float flightDuration = (10 + velocity.y) / Physics.gravity.y;
        float stepTime = flightDuration / _lineSegmentCount;
        _linePoint.Clear();
        for (int i = 0; i < _lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;
            Vector3 MovementVector = new Vector3(
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed
            );
            RaycastHit hit;
            if (Physics.Raycast(startingPoint, -MovementVector, out hit, MovementVector.magnitude))
            {
                for (int j = 0; j < dotParent.childCount; j++)
                {
                    if (j >= i)
                    {
                        dotParent.GetChild(j).gameObject.SetActive(false);
                    }
                }
                break;
            }
            // _linePoint.Add(-MovementVector + startingPoint);
            // if (dotParent.childCount < _lineSegmentCount)
            // {
            //     OnCreateDot(-MovementVector + startingPoint, i);
            // }
            // else
            // {
            // }
            dotParent.GetChild(i).gameObject.SetActive(true);
            dotParent.GetChild(i).transform.position = -MovementVector + startingPoint;
            // Debug.Log(dotParent.GetChild(i));
            // if (dotParent.GetChild(i) == null) {
            //     OnCreateDot(-MovementVector + startingPoint);
            // }
        }

        // _lineRenderer.positionCount = _linePoint.Count;
        // _lineRenderer.SetPositions(_linePoint.ToArray());
    }

    public void OnCreateDot(Vector3 pos, float scl)
    {
        GameObject dot = Instantiate(ballPrefab);
        dot.transform.parent = dotParent;
        dot.transform.position = pos;
        dot.transform.localScale -= new Vector3(scl / 150, scl / 150, scl / 150);
    }

    public void HideLine()
    {
        for (int i = 0; i < dotParent.childCount; i++)
        {
            dotParent.GetChild(i).gameObject.SetActive(false);
        }
        _lineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
