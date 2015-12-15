using System.Collections.Generic;
using UnityEngine;

public class ControlManager {

    BaseControl SelectedControl;
    List<BaseControl> Controls;

    public ControlManager()
    {
        GetControls();
    }

    private void GetControls()
    {
        Controls = new List<BaseControl>();

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Button");
        for (int i = 0; i < temp.Length; i++)
        {
            Button button = temp[i].GetComponent<Button>();
            if (!Controls.Contains(button))
                Controls.Add(button);
        }

        temp = GameObject.FindGameObjectsWithTag("Slider");
        for (int i = 0; i < temp.Length; i++)
        {
            Slider slider = temp[i].GetComponent<Slider>();
            if (!Controls.Contains(slider))
                Controls.Add(slider);
        }

        temp = GameObject.FindGameObjectsWithTag("Checkbox");
        for (int i = 0; i < temp.Length; i++)
        {
            Checkbox checkbox = temp[i].GetComponent<Checkbox>();
            if (!Controls.Contains(checkbox))
                Controls.Add(checkbox);
        }

        temp = GameObject.FindGameObjectsWithTag("Spin");
        for (int i = 0; i < temp.Length; i++)
        {
            Spin spin = temp[i].GetComponent<Spin>();
            if (!Controls.Contains(spin))
                Controls.Add(spin);
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, new Vector2(0, 0), 0.01f);

            for (int i = 0; i < hits.Length; i++)
            {
                BaseControl control = CheckCollider(hits[i].collider);
                if (control != null)
                {
                    SelectedControl = hits[i].collider.gameObject.GetComponent<BaseControl>();
                    SelectedControl.Select(pos);
                }                
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, new Vector2(0, 0), 0.01f);

            for (int i = 0; i < hits.Length; i++)
            {
                BaseControl control = CheckCollider(hits[i].collider);
                if (control != null)
                {
                    control.Submit();
                }
            }

            if (SelectedControl != null)
            {
                SelectedControl.Reset();
                SelectedControl = null;
            }

        }


    }


    private BaseControl CheckCollider(Collider2D collider)
    {
        if (collider.gameObject.tag == "Button" || collider.gameObject.tag == "Slider" || collider.gameObject.tag == "Checkbox" || collider.gameObject.tag == "Spin")
        {
            return collider.gameObject.GetComponent<BaseControl>();
        }
        return null;
    }
}
