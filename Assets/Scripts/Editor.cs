#if UNITY_EDITOR

using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Editor : MonoBehaviour
{
    public List<Button> Buttons = new List<Button>();

    void Update()
    {
        RefreshButtons();

        for (int i = 0; i < Buttons.Count; i++)
        {
            if (Buttons[i] == null)
            {
                Buttons.RemoveAt(i);
                i--;
            }
            else
            {
                Buttons[i].Refresh();
            }
        }        
    }
    
    private void RefreshButtons()
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

}


#endif

