using UnityEngine;

public class InterpolationVector : MonoBehaviour
{
    public Color color;
    public Vector3 direction;

    void Awake()
    {
        direction = transform.forward;
        var meshRenderer = GetComponent<MeshRenderer>();
        var mat = meshRenderer.material;
        mat = new Material(mat) {color = color};
        meshRenderer.material = mat;
    }
}