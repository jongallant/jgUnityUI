using UnityEngine;

public class Spin : BaseControl
{
    public bool ShowBorder;
    public bool ShowBackground;
    
    public Color PressedColor;
    public Color Color;
    public int Width;
    public int Height;
    public float BorderSize;
    public string Text;
    public Font Font;
    public float TextSize;
    public Color TextColor;

    public string[] Items;
    private int Index;

    protected GameObject LeftButton;
    protected GameObject RightButton;
    protected GameObject Background;
    protected GameObject Border;
    protected GameObject TextGO;
        
    public void Start()
    {
        FindGameObjects();
        Refresh();
    }

    private void FindGameObjects()
    {
        LeftButton = transform.Find("LeftButton").gameObject;
        RightButton = transform.Find("RightButton").gameObject;
        Border = transform.Find("Border").gameObject;
        Background = transform.Find("Background").gameObject;
        TextGO = transform.Find("Text").gameObject;

        LeftButton.GetComponent<Button>().OnSubmit += Prev;
        RightButton.GetComponent<Button>().OnSubmit += Next;
    }

    private void Next()
    {
        Index++;
        if (Index > Items.Length - 1)
            Index = 0;

        TextGO.GetComponent<TextMesh>().text = Items[Index];
    }

    private void Prev()
    {
        Index--;
        if (Index < 0)
            Index = Items.Length - 1;

        TextGO.GetComponent<TextMesh>().text = Items[Index];
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
        TextGO.GetComponent<TextMesh>().text = Items[Index];
        TextGO.GetComponent<TextMesh>().characterSize = TextSize;
        TextGO.GetComponent<TextMesh>().color = TextColor;
        TextGO.GetComponent<TextMesh>().font = Font;
        
        Border.SetActive(ShowBorder);
        Background.SetActive(ShowBackground);

        LeftButton.transform.localPosition = new Vector3(-Width / 2f - LeftButton.GetComponent<Button>().Width/2f, 0);
        RightButton.transform.localPosition = new Vector3(Width / 2f + RightButton.GetComponent<Button>().Width/2f, 0);

        Offset = transform.position;
    }

    public override void Submit()
    {

    }
    

}


