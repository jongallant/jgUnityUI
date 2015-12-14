using UnityEngine;

public class Button : BaseControl
{
    public Color PressedColor;
    public Color Color;
    public int Width;
    public int Height;
    public float BorderSize;
    public string Text;
    public Font Font;
    public float TextSize;
    public Color TextColor;

    protected GameObject Background;
    protected GameObject Border;
    protected GameObject TextGO;
    protected BoxCollider2D BoxCollider;

    bool Pressed;
        
#if UNITY_EDITOR
    Editor Editor;
#endif

    public void Start()
    {
        FindGameObjects();

#if UNITY_EDITOR
        Editor = GameObject.Find("Editor").GetComponent<Editor>();
#endif

        Refresh();
    }

    private void FindGameObjects()
    {
        Border = transform.Find("Border").gameObject;
        Background = transform.Find("Background").gameObject;
        TextGO = transform.Find("Text").gameObject;
        BoxCollider = GetComponent<BoxCollider2D>();
    }
    
    public virtual bool Refresh()
    {

#if UNITY_EDITOR
        if (Border == null)
        {
            FindGameObjects();
        }
#endif

        Border.transform.localScale = new Vector3(Width, Height, 1);
        Background.transform.localScale = new Vector3(Width - BorderSize, Height - BorderSize, 1);

        if (Pressed)
        {
            Background.GetComponent<SpriteRenderer>().color = PressedColor;
            Border.GetComponent<SpriteRenderer>().color = Color.Lerp(PressedColor, Color.black, 0.3f);
        }
        else
        {
            Background.GetComponent<SpriteRenderer>().color = Color;
            Border.GetComponent<SpriteRenderer>().color = Color.Lerp(Color, Color.black, 0.3f);
        }

        TextGO.GetComponent<MeshRenderer>().sortingOrder = 10;
        TextGO.GetComponent<TextMesh>().text = Text;
        TextGO.GetComponent<TextMesh>().characterSize = TextSize;
        TextGO.GetComponent<TextMesh>().color = TextColor;
        TextGO.GetComponent<TextMesh>().font = Font;

       BoxCollider.size = new Vector2(Width, Height);

        return true;
    }

    public override void Reset()
    {
        Pressed = false;
    }

    public override void Select()
    {
        Pressed = true;
    }

    public override void Submit()
    {

    }

}


