using System;
using UnityEngine;

public class InterpolatedVector : MonoBehaviour
{
    private Material material;

    public Vector3 direction;

    private void Awake()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        material = new Material(material);
        meshRenderer.material = material;
    }

    // Update is called once per frame
    public void SetValues(Vector3 direction, Color color)
    {
        this.direction = direction;
        material.color = color;
        transform.forward = direction;
    }
}