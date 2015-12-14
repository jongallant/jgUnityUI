using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    ControlManager ControlManager;    
   
    void Awake () {
        Initialize();
    }

    void Initialize()
    {
        ControlManager = new ControlManager();        
    }
	
	void Update () {
        ControlManager.Update();
    }
}
