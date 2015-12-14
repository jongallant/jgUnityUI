using UnityEngine;

public enum SliderType
{
    Horizontal, 
    Vertical
}

public class Slider : BaseControl
{
    public SliderType Type;
    public float Size;
    public float BorderSize;
    public Sprite KnobSprite;
    public Sprite BackgroundSprite;
    public Sprite EdgeSprite;
    public float Value;
    public float FillerSize;
    public Color FillerColor;
    public Color BackgroundColor;
    public Color KnobColor;

    public bool ShowFiller;
    public bool ShowKnob;
    public bool ShowBackground;

    private GameObject Filler;
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
        Filler = transform.Find("Filler").gameObject;
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update () {
                        
        if (Pressed)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos -= Offset;

            if (Type == SliderType.Horizontal)
                SetValue(pos.x); 
            else if (Type == SliderType.Vertical)
                SetValue(pos.y);
        }

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
        
        float min = 0, max = 0;
        Background.transform.localScale = new Vector3(Size, 4, 1);

        if (Type == SliderType.Horizontal)
        {
            Background.transform.localRotation = Quaternion.Euler(0, 0, 0);
            EdgeLeft.transform.localRotation = Quaternion.Euler(0, 0, 0);
            EdgeRight.transform.localRotation = Quaternion.Euler(0, 0, 0);

            float extent = Background.GetComponent<SpriteRenderer>().bounds.max.x - Background.GetComponent<SpriteRenderer>().bounds.min.x;
            MaxExtent = extent / 2f;
                        
            float val = EdgeSprite.texture.width / 2f / CalculatePixelUnits(EdgeSprite);
            min = Background.GetComponent<SpriteRenderer>().bounds.min.x - val / 2f;
            max = Background.GetComponent<SpriteRenderer>().bounds.max.x + val / 2f;

            EdgeLeft.transform.position = new Vector2(min, transform.position.y);
            EdgeRight.transform.position = new Vector2(max, transform.position.y);

            float fillValue = Value * MaxExtent * 2;
            Filler.transform.position = new Vector2(min + fillValue/2f, transform.position.y); 
            Filler.transform.localScale = new Vector2(+fillValue, FillerSize);

            Value = (Knob.transform.localPosition.x + MaxExtent) / MaxExtent / 2f;
        }
        else if (Type == SliderType.Vertical)
        {
            Background.transform.localRotation = Quaternion.Euler(0, 0, 90f);
            EdgeLeft.transform.localRotation = Quaternion.Euler(0, 0, 90);
            EdgeRight.transform.localRotation = Quaternion.Euler(0, 0, 90);

            float extent = Background.GetComponent<SpriteRenderer>().bounds.max.y - Background.GetComponent<SpriteRenderer>().bounds.min.y;
            MaxExtent = extent / 2f;
            
            float val = EdgeSprite.texture.width / 2f / CalculatePixelUnits(EdgeSprite);
            min = Background.GetComponent<SpriteRenderer>().bounds.min.y - val / 2f;
            max = Background.GetComponent<SpriteRenderer>().bounds.max.y + val / 2f;

            EdgeLeft.transform.position = new Vector2(transform.position.x, min);
            EdgeRight.transform.position = new Vector2(transform.position.x, max);

            float fillValue = Value * MaxExtent * 2;
            Filler.transform.position = new Vector2(transform.position.x, min + fillValue/2f);
            Filler.transform.localScale = new Vector2(FillerSize, fillValue);
            
            Value = (Knob.transform.localPosition.y + MaxExtent) / MaxExtent / 2f;
        }

        Knob.transform.localScale = new Vector3(3.9f, 4 - BorderSize, 1);     
        Offset = transform.position;

        Background.GetComponent<SpriteRenderer>().sprite = BackgroundSprite;
        EdgeLeft.GetComponent<SpriteRenderer>().sprite = EdgeSprite;
        EdgeRight.GetComponent<SpriteRenderer>().sprite = EdgeSprite;
        Knob.GetComponent<SpriteRenderer>().sprite = KnobSprite;

        Filler.GetComponent<SpriteRenderer>().color = FillerColor;
        Background.GetComponent<SpriteRenderer>().color = BackgroundColor;
        EdgeLeft.GetComponent<SpriteRenderer>().color = BackgroundColor;
        EdgeRight.GetComponent<SpriteRenderer>().color = BackgroundColor;
        Knob.GetComponent<SpriteRenderer>().color = KnobColor;



        if (!ShowBackground)
        {
            EdgeLeft.SetActive(false);
            EdgeRight.SetActive(false);
            Background.SetActive(false);
        }
        else
        {
            EdgeLeft.SetActive(true);
            EdgeRight.SetActive(true);
            Background.SetActive(true);
        }

        if (!ShowKnob)
            Knob.SetActive(false);
        else
            Knob.SetActive(true);

        if (!ShowFiller)
            Filler.SetActive(false);
        else
            Filler.SetActive(true);
    }

    public void SetValue(float value)
    {
        if (Type == SliderType.Horizontal)
            Knob.transform.localPosition = new Vector2(Mathf.Clamp(value, -MaxExtent, MaxExtent), 0);
        else if (Type == SliderType.Vertical)
            Knob.transform.localPosition = new Vector2(0, Mathf.Clamp(value, -MaxExtent, MaxExtent));

        BoxCollider.offset = Knob.transform.localPosition;        
        BoxCollider.size = Knob.transform.localScale;
    }

    protected float CalculatePixelUnits(Sprite sprite)
    {
        return sprite.rect.width / sprite.bounds.size.x;
    }
}
