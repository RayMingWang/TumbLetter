using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumblerManager : MonoBehaviour {
    public TumblerCenter tumbler;

    public TumblerCenter current_tumbler; 
	// Use this for initialization
	public void change_Tumbler()
    {
        current_tumbler = Instantiate(tumbler);
        current_tumbler.manager = this;
    }
}
