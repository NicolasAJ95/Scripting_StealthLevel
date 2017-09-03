using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectReference : MonoBehaviour
{

    public Shader postEffect;
    private Material mat;



    // Use this for initialization
    void Start()
    {
        this.mat = new Material(this.postEffect);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        Graphics.Blit(source, destination, mat);
    }

}