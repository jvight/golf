using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    [Range(3, 30)]
    private int _lineSegmentCount = 20;
    private List<Vector3> _linePoint = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {

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
                break;
            }
            _linePoint.Add(-MovementVector + startingPoint);
        }
        _lineRenderer.positionCount = _linePoint.Count;
        _lineRenderer.SetPositions(_linePoint.ToArray());
    }

    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
