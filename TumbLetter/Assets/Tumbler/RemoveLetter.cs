using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLetter : MonoBehaviour {

    

    void OnTriggerExit(Collider collision)
    {
        Debug.Log("Began Destruction");
        collision.transform.GetComponent<TumblerCenter>().selfDestruct();
    }
}
