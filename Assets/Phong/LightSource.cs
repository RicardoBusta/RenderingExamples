using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public Color Diffuse;

    public Color Specular;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Diffuse;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
