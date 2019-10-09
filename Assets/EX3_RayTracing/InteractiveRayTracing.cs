using System.Collections;
using UnityEngine;

public class InteractiveRayTracing : MonoBehaviour
{
    public Texture RenderTexture;

    private Coroutine rayTracingRoutine;

    public float Near;
    
    public void StartRayTracing()
    {
        StopCoroutine(rayTracingRoutine);
        rayTracingRoutine = StartCoroutine(RayTracing());
    }

    private IEnumerator RayTracing()
    {
        for (var x = 0; x < RenderTexture.width; x++)
        {
            for (var y = 0; y < RenderTexture.height; y++)
            {
                var direction = new Vector3(x, y, Near).normalized;
                var ray = new Ray(Vector3.zero, direction);
            }
        }
    }
}
