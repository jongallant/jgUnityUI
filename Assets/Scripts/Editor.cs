// Class is used to provide a live edit of control properties, in Edit mode
#if UNITY_EDITOR

using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Editor : MonoBehaviour
{
    public List<BaseControl> Controls = new List<BaseControl>();

    void Update()
    {
        RefreshControls();

        for (int i = 0; i < Controls.Count; i++)
        {
            if (Controls[i] == null)
            {
                Controls.RemoveAt(i);
                i--;
            }
            else
            {
                Controls[i].Refresh();
            }
        }        
    }
    
    private void RefreshControls()
    {
        Controls.Clear();

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

}


#endif

