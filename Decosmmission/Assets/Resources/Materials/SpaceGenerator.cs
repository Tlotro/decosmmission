using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SpaceGenerator
{
    public static int timeOffset;
    public static int PixelCountByWidth = 240;
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
        Debug.Log(timeOffset);
        faintQuantity = Random.Range(0.003f, 0.01f);
        Debug.Log(faintQuantity);
        diskQuantity = faintQuantity + Random.Range(0.01f, 0.02f);
        Debug.Log(diskQuantity);
        diskDegree = Random.Range(-89.99f, 89.99f);
        Debug.Log(diskDegree);
        diskOffset = Random.Range(0 + Mathf.Min(-1/Mathf.Tan(Mathf.Deg2Rad * diskDegree), 0), 1 + Mathf.Max(-1/Mathf.Tan(Mathf.Deg2Rad * diskDegree),0));
        Debug.Log(diskOffset);
        diskWidth = Random.Range(0f, 0.3f);
        Debug.Log(diskWidth);
    }

    public static void getSpace(float width, float height, Image renderer)
    {
        m_Material.SetFloat("_TimeOffset", timeOffset);
        m_Material.SetFloat("_FaintQuantity", faintQuantity);
        m_Material.SetFloat("_DiscQuantity", diskQuantity);
        m_Material.SetFloat("_DiscDegree", diskDegree);
        m_Material.SetFloat("_DiscOffset", diskOffset);
        m_Material.SetFloat("_DiskWidth", diskWidth);

        //Get a temporary RenderTexture and draw our source texture into it using our shader
        RenderTexture tmp = RenderTexture.GetTemporary(Mathf.RoundToInt(width), Mathf.RoundToInt(height), 0);
        RenderTexture tmp2 = RenderTexture.GetTemporary(Mathf.RoundToInt(width), Mathf.RoundToInt(height), 0);
        Graphics.Blit(tmp,tmp2, m_Material);

        //Store the last active RT and set our new one as active
        RenderTexture lastActive = RenderTexture.active;
        RenderTexture.active = tmp2;

        //Read the blurred texture into a new Texture2D
        Texture2D Tex = new Texture2D(tmp.width, tmp.height);
        Tex.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        Tex.Apply();
        Debug.Log(Tex);
        //Restore the last active RT and release our temp tex
        RenderTexture.active = lastActive;
        RenderTexture.ReleaseTemporary(tmp);
        byte[] data = Tex.EncodeToPNG();
        Tex.filterMode = FilterMode.Point;
        renderer.sprite = Sprite.Create(Tex, new Rect(0, 0, Tex.width, Tex.height), new Vector2(0.5f, 0.5f));
    }
}
