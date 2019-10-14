using System.Collections;
using UnityEngine;

public class InteractiveRayTracing : MonoBehaviour
{
    public Texture RenderTexture;

    private Coroutine rayTracingRoutine;

    public float Near;

    private Texture2D tex2d;

    private void Start()
    {
        tex2d = Texture2D.CreateExternalTexture(
            RenderTexture.width,
            RenderTexture.height,
            TextureFormat.RGB24,
            false, false,
            RenderTexture.GetNativeTexturePtr());
    }

    public void StartRayTracing()
    {
        if(rayTracingRoutine!=null)
        {
            StopCoroutine(rayTracingRoutine);
        }
        rayTracingRoutine = StartCoroutine(RayTracing());
    }

    private IEnumerator RayTracing()
    {
        var delay = new WaitForEndOfFrame();
        const int pixelsPerFrame = 1;
        var currentPixel = pixelsPerFrame;

        for (var x = 0; x < RenderTexture.width; x++)
        {
            for (var y = 0; y < RenderTexture.height; y++)
            {
                var direction = new Vector3(x, y, Near).normalized;
                var ray = new Ray(Vector3.zero, direction);

                tex2d.SetPixel(x,y, Color.white);

                currentPixel--;
                if (currentPixel == 0)
                {
                    currentPixel = pixelsPerFrame;
                    yield return delay;
                }
            }
        }

        rayTracingRoutine = null;
    }
}