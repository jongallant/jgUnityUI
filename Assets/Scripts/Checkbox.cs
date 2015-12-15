using UnityEngine;

public class Checkbox : BaseControl
{
    public Color PressedColor;
    public Color Color;
    public Color BorderColor;
    public int Size;
    public float BorderSize;
    public bool Checked;

    protected GameObject Box;
    protected GameObject Border;
    protected GameObject Check;
    protected BoxCollider2D BoxCollider;
        

    public void Start()
    {
        FindGameObjects();
        Refresh();
    }

    private void FindGameObjects()
    {
        Border = transform.Find("Border").gameObject;
        Box = transform.Find("Box").gameObject;
        Check = transform.Find("Check").gameObject;
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

        Border.transform.localScale = new Vector3(Size, Size, 1);
        Box.transform.localScale = new Vector3(Size - BorderSize, Size - BorderSize, 1);

        if (Pressed)
        {
            Box.GetComponent<SpriteRenderer>().color = PressedColor;
            Border.GetComponent<SpriteRenderer>().color = Color.Lerp(PressedColor, Color.black, 0.3f);
        }
        else
        {
            Box.GetComponent<SpriteRenderer>().color = Color;
            Border.GetComponent<SpriteRenderer>().color = BorderColor;
        }

        BoxCollider.size = new Vector2(Size, Size);
        Offset = transform.position;

        if (Checked)
            Check.SetActive(true);
        else
            Check.SetActive(false);
    }

    public override void Submit()
    {
        Checked = !Checked;
    }

}


