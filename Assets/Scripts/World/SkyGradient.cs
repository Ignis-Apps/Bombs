using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
public class SkyGradient : MonoBehaviour
{

    public Gradient gradient = null;
    
    [HideInInspector][SerializeField]
    private MeshFilter meshFilter;
    [HideInInspector] [SerializeField]
    private Gradient currentGradient = null;

    private void Reset()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = CreateMesh();
        
        CreateMeshRenderer();

    }

    private void Update()
    {
        if(gradient.colorKeys.Length != currentGradient.alphaKeys.Length)
        {
            meshFilter.sharedMesh = CreateMesh();
            return;
        }

        for(int i=0; i < gradient.colorKeys.Length; i++)
        {

           if(!IsColorEqual(currentGradient.colorKeys[i], gradient.colorKeys[i])){  
                meshFilter.sharedMesh = CreateMesh();
                return; 
           }
           
        }
    }

    private bool IsColorEqual(GradientColorKey oldKey, GradientColorKey nextKey)
    {
        if (Mathf.Abs(oldKey.time - nextKey.time) > 0.01) { return false; }
        if (Mathf.Abs(oldKey.color.r - nextKey.color.r) > 0.01) { return false; }
        if (Mathf.Abs(oldKey.color.g - nextKey.color.g) > 0.01) { return false; }
        if (Mathf.Abs(oldKey.color.b - nextKey.color.b) > 0.01) { return false; }
        return true;
       
    }
    
    Mesh CreateMesh()
    {

        if(gradient == null)
        {
            CreateDefaultGradient();
        }

        float width     = 1.0f;
        float height    = 1.0f;

        Mesh mesh = new Mesh();

        int vertexCount = gradient.colorKeys.Length * 2;

        Vector3[] vertecies = new Vector3[vertexCount];
        Vector3[] normals   = new Vector3[vertexCount];
        Vector2[] uvs       = new Vector2[vertexCount];
        Color[] color       = new Color[vertexCount];

        for(int i = 0; i < vertexCount / 2; i++)
        {
            vertecies[i * 2]        = new Vector3(-width / 2, gradient.colorKeys[i].time - (height / 2), 0);
            vertecies[i * 2 + 1]    = new Vector3( width / 2, gradient.colorKeys[i].time - (height / 2), 0);
            
            normals[i * 2]      = -Vector3.forward;
            normals[i * 2 + 1]  = -Vector3.forward;

            uvs[i * 2]      = new Vector3(0, gradient.colorKeys[i].time);
            uvs[i * 2 + 1 ] = new Vector3(0, gradient.colorKeys[i].time);

            color[i * 2]        = gradient.colorKeys[i].color;
            color[i * 2+ 1 ]    = gradient.colorKeys[i].color;
        }

        mesh.vertices = vertecies;

        int[] indicies = new int[gradient.colorKeys.Length * 6];
        for(int i=0; i< gradient.colorKeys.Length - 1; i++)
        {
            indicies[i * 6]     = (i * 2);
            indicies[i * 6 + 1] = (i * 2) + 2;
            indicies[i * 6 + 2] = (i * 2) + 1;

            indicies[i * 6 + 3] = (i * 2) + 2;
            indicies[i * 6 + 4] = (i * 2) + 3;
            indicies[i * 6 + 5] = (i * 2) + 1;
        }

        mesh.triangles = indicies;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.colors = color;

        currentGradient = new Gradient();
        GradientColorKey[] gck = gradient.colorKeys;
        GradientAlphaKey[] gak = gradient.alphaKeys;
        currentGradient.SetKeys(gck, gak);


        return mesh;
    }

    void CreateDefaultGradient()
    {
        GradientColorKey[] gradientColorKeys = new GradientColorKey[2];
        GradientAlphaKey[] gradientAlphaKeys = new GradientAlphaKey[2];
        gradient = new Gradient();

        gradientColorKeys[0].color = new Color(0.0f, 0.0f, 0.0f);
        gradientColorKeys[0].time = 0f;
        gradientColorKeys[1].color = new Color(1.0f, 1.0f, 1.0f);
        gradientColorKeys[1].time = 1f;

        gradientAlphaKeys[0].alpha = 1.0f;
        gradientAlphaKeys[0].time = 0f;
        gradientAlphaKeys[1].alpha = 1.0f;
        gradientAlphaKeys[1].time = 1.0f;

        gradient.SetKeys(gradientColorKeys, gradientAlphaKeys);

    }

    void CreateMeshRenderer()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = new Material(Shader.Find("Unlit/SimpleVertexShader"));
        meshRenderer.allowOcclusionWhenDynamic = false;
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        meshRenderer.receiveShadows = false;
        meshRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        meshRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
        meshRenderer.sharedMaterial = material;
    }

}
