using UnityEngine;

public class Button : BaseControl
{
    public delegate void SubmitAction();
    public event SubmitAction OnSubmit;

    public Sprite IconSprite;
    public float IconSize;
    public Color IconColor;

    public Color PressedColor;
    public Color Color;
    public int Width;
    public int Height;
    public float BorderSize;
    public string Text;
    public Font Font;
    public float TextSize;
    public Color TextColor;

    protected GameObject Icon;
    protected GameObject Background;
    protected GameObject Border;
    protected GameObject TextGO;
    protected BoxCollider2D BoxCollider;

    public void Start()
    {
        FindGameObjects();
        Refresh();
    }

    private void FindGameObjects()
    {
        Icon = transform.Find("Icon").gameObject;
        Border = transform.Find("Border").gameObject;
        Background = transform.Find("Background").gameObject;
        TextGO = transform.Find("Text").gameObject;
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    public override void Refresh()
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

        Offset = transform.position;

        if (IconSprite != null)
        {
            Icon.SetActive(true);
            Icon.GetComponent<SpriteRenderer>().sprite = IconSprite;
            Icon.GetComponent<SpriteRenderer>().color = IconColor;
            Icon.transform.localScale = new Vector2(IconSize, IconSize);
        }
        else
        {
            Icon.SetActive(false);
        }



    }

    public override void Submit()
    {
        if (OnSubmit != null)
            OnSubmit();
    }

}


