using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Side
{
    Left,
    Right
}

public class DialogWindow : MonoBehaviour
{
    public Image characterIcon;
    public Image characterImage;
    public TMP_Text textBox;
    
    public static DialogWindow Create()
    {
        GameObject a = Instantiate(Resources.Load<GameObject>("UI_Prefabs/DialogWindow"));
        //CanvasManager.instance.controlledCanvasList.Add(a.GetComponent<ControlledCanvas>());
        return a.GetComponentInChildren<DialogWindow>();
    }
    
    public void SetSide(Side side)
    {
        switch (side)
        {
            case Side.Left:
                characterIcon.transform.position.Set(176.25f, 0f, 0f);
                textBox.transform.position.Set(1094f, 0f, 0f);
                break;

            case Side.Right:
                characterIcon.transform.position.Set(1743.75f, 0f, 0f);
                textBox.transform.position.Set(826f, 0f, 0f);
                break;

            default: break;
        }
    }
    
    public void SetText(string text)
    {
        textBox.text = text;
    }

    public void SetCharacterIcon(string path)
    {
        characterImage.sprite = Resources.Load<Sprite>(path);
    }

}
