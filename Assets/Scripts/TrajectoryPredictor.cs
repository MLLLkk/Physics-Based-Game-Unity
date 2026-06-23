using UnityEngine;

public class TrajectoryPredictor : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public int resolution = 30; // عدد النقاط في الخط (كلما زاد زادت النعومة)

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(Vector3 startPos, Vector3 velocity)
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = resolution;
        Vector3[] points = new Vector3[resolution];

        for (int i = 0; i < resolution; i++)
        {
            float time = i * 0.1f; // تباعد النقاط الزمني
            // معادلة المقذوفات الفيزيائية
            points[i] = startPos + velocity * time + 0.5f * Physics.gravity * time * time;
        }

        lineRenderer.SetPositions(points);
    }

    public void HideTrajectory()
    {
        lineRenderer.enabled = false;
    }
}