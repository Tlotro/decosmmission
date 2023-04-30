using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public static class SpaceGenerator
{
    public static int timeOffset;
    public static int PixelCountByWidth = 640;
    public static float faintQuantity;
    public static float diskQuantity;
    public static float diskDegree;
    public static float diskOffset;
    public static float diskWidth;

    private static Material m_MaterialBase = Resources.Load<Material>("Materials/SpaceBox");
    private static Material m_Material;

    public static void Setup()
    {
        m_Material = MonoBehaviour.Instantiate(m_MaterialBase);
    }

    public static void generateSpaceValues()
    {
        timeOffset = Mathf.RoundToInt(System.DateTime.Now.Minute*60+System.DateTime.Now.Second);
        faintQuantity = Random.Range(0.003f, 0.01f);
        diskQuantity = faintQuantity + Random.Range(0.01f, 0.02f);
        diskDegree = Random.Range(-89f, 89f);
        diskOffset = Random.Range(0 + Mathf.Min(-1/Mathf.Tan(Mathf.Deg2Rad * diskDegree), 0), 1 + Mathf.Max(-1/Mathf.Tan(Mathf.Deg2Rad * diskDegree),0));
        diskWidth = Random.Range(0f, 0.3f);
    }

    public static void getSpace(int width, int height, Image renderer)
    {
        m_Material.SetFloat("_TimeOffset", timeOffset);
        m_Material.SetFloat("_FaintQuantity", faintQuantity);
        m_Material.SetFloat("_DiscQuantity", diskQuantity);
        m_Material.SetFloat("_DiscDegree", diskDegree);
        m_Material.SetFloat("_DiscOffset", diskOffset);
        m_Material.SetFloat("_DiskWidth", diskWidth);
        
        //Get a temporary RenderTexture and draw our source texture into it using our shader
        Texture2D tmp = new Texture2D(width, height, TextureFormat.RGB24, false);
        tmp.Apply();
        RenderTexture tmp2 = RenderTexture.GetTemporary(width, height, 0);
        Graphics.Blit(tmp,tmp2, m_Material);

        //Store the last active RT and set our new one as active
        RenderTexture.active = tmp2;

        //Read the star texture into a new Texture2D
        Texture2D Tex = new Texture2D(width, height);
        Tex.filterMode = FilterMode.Point;
        Tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        Tex.Apply();
        //Restore the last active RT and release our temp tex
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(tmp2);
        byte[] data = Tex.EncodeToPNG(); 
        //System.IO.File.WriteAllBytes("Assets/Resources/Materials/Space.png", data);
        renderer.sprite = Sprite.Create(Tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f),32);
    }
}
