using System;
using UnityEngine;

public class IlluminationController : MonoBehaviour
{
    public Material[] affectMaterials;

    public Color SpecularColor;
    public Color DiffuseColor;
    public Color AmbientColor;
    public float Shininess;
    private static readonly int Ks = Shader.PropertyToID("ks");
    private static readonly int Kd = Shader.PropertyToID("kd");
    private static readonly int Ka = Shader.PropertyToID("ka");

    private static readonly int a = Shader.PropertyToID("a");

    public Color AmbientLightColor;
    private static readonly int Ia = Shader.PropertyToID("ia");

    public LightSource lightSource0;
    private static readonly int Id0 = Shader.PropertyToID("id0");
    private static readonly int Is0 = Shader.PropertyToID("is0");
    private static readonly int L0 = Shader.PropertyToID("l0");

    public LightSource lightSource1;
    private static readonly int Id1 = Shader.PropertyToID("id1");
    private static readonly int Is1 = Shader.PropertyToID("is1");
    private static readonly int L1 = Shader.PropertyToID("l1");

    public bool updateValues;

    // Update is called once per frame
    private void Update()
    {
        if (!updateValues) return;

        foreach (var mat in affectMaterials)
        {
            mat.SetColor(Ks, SpecularColor);
            mat.SetColor(Kd, DiffuseColor);
            mat.SetColor(Ka, AmbientLightColor);
            mat.SetFloat(a, Shininess);

            mat.SetColor(Ia, AmbientColor);

            mat.SetColor(Is0, lightSource0.Specular);
            mat.SetColor(Id0, lightSource0.Diffuse);
            mat.SetVector(L0, lightSource0.transform.position);

            mat.SetColor(Is1, lightSource1.Specular);
            mat.SetColor(Id1, lightSource1.Diffuse);
            mat.SetVector(L1, lightSource1.transform.position);
        }
    }
}