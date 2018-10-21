using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustableSoftShadow : MonoBehaviour {

    public enum Type
    {
        Cube = 0,
        Circle = 1
    }

    public Type type = Type.Cube;
    public Color colorShadow = Color.black;
    public float magnificationSizeShadow = 65.0f;
    

    [Range(0,5)]
    public int shadowSize;

    //Private
    private bool workingCapacity = true;

    public Sprite shadow1;
    public Sprite shadow2;
    public Sprite shadow3;
    public Sprite shadow4;
    public Sprite shadow5;

    public Rect rectShadow;
    public RectTransform rectTransformShadow;
    public RectTransform rectTransformThisObject;

    [HideInInspector]
    public Image shadow;

    // Use this for initialization
    private void Awake() {
        /*
        try
        {
            //Load shadow "Resources/Shadow/"
            shadow1 = Resources.Load("roundedsquare_shadow_1") as Sprite;
            shadow2 = (Sprite)Resources.Load("Shadow/roundedsquare_shadow_2");
            shadow3 = (Sprite)Resources.Load("Shadow/roundedsquare_shadow_3");
            shadow4 = (Sprite)Resources.Load("Shadow/roundedsquare_shadow_4");

        }
        catch (UnityException ex)
        {
            Debug.Log("Error : " + ex);
            workingCapacity = false;
        } */       
    }

    // Use this for initialization
    private void Start()
    {

    }

    public GameObject CreateShadow()
    {
        if (shadow != null)
            return null;


        shadow = Image();
        shadow.sprite = shadow3;

        shadow.gameObject.transform.SetParent(this.gameObject.transform.root);
        SetShadowSize(shadowSize);
        shadow.color = colorShadow;


        shadow.type = UnityEngine.UI.Image.Type.Sliced;
        shadow.fillCenter = true;



        rectTransformThisObject = this.GetComponent<RectTransform>();
        rectTransformShadow = shadow.GetComponent<RectTransform>();
        rectShadow = new Rect(rectTransformThisObject.rect.x, rectTransformThisObject.rect.y, (rectTransformThisObject.rect.width + magnificationSizeShadow), rectTransformThisObject.rect.height + magnificationSizeShadow);
        shadow.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        shadow.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
        rectTransformShadow.sizeDelta = new Vector2(rectShadow.width, rectShadow.height);
        rectTransformShadow.position = new Vector3(rectTransformThisObject.position.x, rectTransformThisObject.position.y, rectTransformThisObject.position.z);

        this.transform.SetParent(shadow.gameObject.transform);
        return shadow.gameObject;
    }

    public void DeleteShadow()
    {
        if (shadow == null)
            return;

        this.transform.SetParent(shadow.gameObject.transform.root);
        DestroyImmediate(shadow.gameObject);
        shadow = null;
    }

    public GameObject ReCreateShadow()
    {
        if (shadow == null)
            return null;

        this.transform.SetParent(shadow.gameObject.transform.root);


        SetShadowSize(shadowSize);
        shadow.color = colorShadow;
        if (type == Type.Cube)
        {
            shadow.type = UnityEngine.UI.Image.Type.Sliced;
            shadow.fillCenter = true;
        }
        else
        {
            shadow.type = UnityEngine.UI.Image.Type.Simple;
        }


        rectTransformThisObject = this.GetComponent<RectTransform>();
        rectTransformShadow = shadow.GetComponent<RectTransform>();
        rectShadow = new Rect(rectTransformThisObject.rect.x, rectTransformThisObject.rect.y, (rectTransformThisObject.rect.width + magnificationSizeShadow), rectTransformThisObject.rect.height + magnificationSizeShadow);
        shadow.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        shadow.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
        rectTransformShadow.sizeDelta = new Vector2(rectShadow.width, rectShadow.height);
        rectTransformShadow.position = new Vector3(rectTransformThisObject.position.x, rectTransformThisObject.position.y, rectTransformThisObject.position.z);

        this.transform.SetParent(shadow.gameObject.transform);

        return shadow.gameObject;

    }

    public void SetShadowSize(int size)
    {
        switch (size)
        {
            case 1:
                shadow.sprite = shadow1;
                break;
            case 2:
                shadow.sprite = shadow2;
                break;
            case 3:
                shadow.sprite = shadow3;
                break;
            case 4:
                shadow.sprite = shadow4;
                break;
            case 5:
                shadow.sprite = shadow5;
                break;
        }
    }

    //Created "GameObject" and add component "Image"
    private Image Image()
    {
        GameObject obj = new GameObject();
        obj.AddComponent<Image>();
        obj.name = "Shadow (" + this.name + ")";
        obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        return obj.GetComponent<Image>();
    }

    
}
