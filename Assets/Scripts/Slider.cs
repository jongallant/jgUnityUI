using UnityEngine;

public class Slider : BaseControl
{
    public float Size;
    public float BorderSize;

    public float Value;
  

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

        MaxExtent = Size / 2f;
        MaxExtent -= Knob.transform.localScale.x / 2f;
        MaxExtent -= BorderSize;

        Offset = transform.position;


        Value = (Knob.transform.localPosition.x + MaxExtent) / MaxExtent / 2f;

    }
}
