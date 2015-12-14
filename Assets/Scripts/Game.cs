using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public List<Button> Buttons = new List<Button>();
    Button PressedButton;

    void Start () {
        GetButtons();
    }

    void GetButtons()
    {
        Buttons.Clear();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Button");
        for (int i = 0; i < temp.Length; i++)
        {
            Button button = temp[i].GetComponent<Button>();
            if (!Buttons.Contains(button))
                Buttons.Add(button);
        }
    }
	
	void Update () {
              
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, new Vector2(0, 0), 0.01f);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.tag == "Button")
                {
                    PressedButton = hits[i].collider.gameObject.GetComponent<Button>();
                    PressedButton.Press();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, new Vector2(0, 0), 0.01f);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.tag == "Button")
                {
                    if (PressedButton == hits[i].collider.gameObject.GetComponent<Button>())
                    {
                        //Execute Button Action
                        //PressedButton.Action()
                    }
                }
            }

            if (PressedButton != null)
            {
                PressedButton.Reset();
                PressedButton = null;
            }

        }
    }
}
