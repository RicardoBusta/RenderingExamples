using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interpolation : MonoBehaviour
{
    public Camera camera;
    public InterpolationVector[] vertices;

    public InterpolatedVector interpolated;

    private Vector3[] vPos;
    private float bigArea;
    private Vector3 pos;
    public TextMeshPro[] DebugText;

    private void Start()
    {
        interpolated.SetValues(vertices[0].direction, vertices[1].color);
        vPos = new[] {vertices[0].transform.position, vertices[1].transform.position, vertices[2].transform.position};
        bigArea = OrientedTriangleArea(vPos);
    }

    private void Update()
    {
        Debug.DrawLine(pos, vPos[0], vertices[0].color);
        Debug.DrawLine(pos, vPos[1], vertices[1].color);
        Debug.DrawLine(pos, vPos[2], vertices[2].color);

        if (Input.GetMouseButton(0))
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var HitInfo))
            {
                InterpolateAt(HitInfo.point);
            }
        }
    }

    private void InterpolateAt(Vector3 position)
    {
        var a0 = OrientedTriangleArea(new[] {position, vPos[1], vPos[2]}) / bigArea;
        var a1 = OrientedTriangleArea(new[] {position, vPos[2], vPos[0]}) / bigArea;
        var a2 = OrientedTriangleArea(new[] {position, vPos[0], vPos[1]}) / bigArea;

        DebugText[0].text = a0.ToString();
        DebugText[1].text = a1.ToString();
        DebugText[2].text = a2.ToString();
        
        var interpolatedNormal = ((a0 * vertices[0].direction) +
                                  (a1 * vertices[1].direction) +
                                  (a2 * vertices[2].direction)).normalized;
        var interpolatedColor = a0 * vertices[0].color +
                                a1 * vertices[1].color +
                                a2 * vertices[2].color;
        var interpolatedPosition = a0 * vPos[0] +
                                   a1 * vPos[1] + 
                                   a2 * vPos[2];
        pos = interpolatedPosition;
        interpolated.transform.position = interpolatedPosition;

        interpolated.SetValues(interpolatedNormal, interpolatedColor);
    }

    /// <summary>
    /// Calculates the oriented area of a triangle
    /// </summary>
    /// <param name="p">Array containing 3 vertices</param>
    /// <returns></returns>
    private float OrientedTriangleArea(IReadOnlyList<Vector3> p)
    {
        var v1 = p[0] - p[1];
        var v2 = p[0] - p[2];
        var signedArea = (
                             p[0].x * (p[1].y - p[2].y) +
                             p[1].x * (p[2].y - p[0].y) +
                             p[2].x * (p[0].y - p[1].y)
                         ) / 2;
        return signedArea;
    }
}