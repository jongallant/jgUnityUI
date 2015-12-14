using UnityEngine;

public class Slider : BaseControl
{
    public float Size;
    public float BorderSize;
    public Sprite KnobSprite;
    public Sprite BackgroundSprite;
    public Sprite EdgeSprite;
    public float Value;

    private GameObject EdgeLeft;
    private GameObject EdgeRight;
    private GameObject Background;
    private GameObject Knob;
    private float MaxExtent;
    private BoxCollider2D BoxCollider;

    void Start () {
        FindGameObjects();
        Refresh();
    }

    private void FindGameObjects()
    {
        EdgeLeft = transform.Find("EdgeLeft").gameObject;
        EdgeRight = transform.Find("EdgeRight").gameObject;
        Background = transform.Find("Background").gameObject;
        Knob = transform.Find("Knob").gameObject;
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update () {
                        
        if (Pressed)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos -= Offset;
            Knob.transform.localPosition = new Vector2(Mathf.Clamp(pos.x, -MaxExtent, MaxExtent), 0);
        }

        BoxCollider.offset = Knob.transform.localPosition;        
        BoxCollider.size = Knob.transform.localScale;
	}

    public override void Submit()
    {

    }

    public override void Refresh()
    {
#if UNITY_EDITOR
        if (Knob == null)
        {
            FindGameObjects();
        }
#endif

        Background.transform.localScale = new Vector3(Size, 4, 1);
        Knob.transform.localScale = new Vector3(3.9f, 4 - BorderSize, 1);     

        Offset = transform.position;

        Background.GetComponent<SpriteRenderer>().sprite = BackgroundSprite;
        EdgeLeft.GetComponent<SpriteRenderer>().sprite = EdgeSprite;
        EdgeRight.GetComponent<SpriteRenderer>().sprite = EdgeSprite;
        Knob.GetComponent<SpriteRenderer>().sprite = KnobSprite;

        float extent = Background.GetComponent<SpriteRenderer>().bounds.max.x - Background.GetComponent<SpriteRenderer>().bounds.min.x;
        MaxExtent = extent / 2f;

        float val = EdgeSprite.texture.width / 2f / CalculatePixelUnits(EdgeSprite);
        float xleft = Background.GetComponent<SpriteRenderer>().bounds.min.x - val/2f;
        float xright= Background.GetComponent<SpriteRenderer>().bounds.max.x + val / 2f;

        EdgeLeft.transform.localPosition = new Vector2(xleft, 0);
        EdgeRight.transform.localPosition = new Vector2(xright, 0);
        
        Value = (Knob.transform.localPosition.x + MaxExtent) / MaxExtent / 2f;
    }


    protected float CalculatePixelUnits(Sprite sprite)
    {
        return sprite.rect.width / sprite.bounds.size.x;
    }
}
