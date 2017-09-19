using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurPostEffect : MonoBehaviour {

    public Shader postEffect;
    private Material mat;
    [Range(0.0f, 200.0f)]
    public float BlurOffset = 1.0f;
    [Range(0, 20)]
    public int Iterations;
    [Range(0, 4)]
    public int DownRes;

    public Texture2D mask;

    // Use this for initialization
    void Start()
    {
        this.mat = new Material(this.postEffect);
    }


    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mat.SetFloat ("_BlurOffset", this.BlurOffset );
        mat.SetTexture ("_Mask", this.mask);

        int width = source.width >> DownRes;
        int height = source.height >> DownRes;

        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(source, rt);

        for (int i = 0; i < Iterations; i++)
        {
            RenderTexture rt2 = RenderTexture.GetTemporary(width, height);
            Graphics.Blit(rt, rt2, mat);
            RenderTexture.ReleaseTemporary(rt);
            rt = rt2;
        }


        Graphics.Blit(rt, destination);
        RenderTexture.ReleaseTemporary(rt);
}}
