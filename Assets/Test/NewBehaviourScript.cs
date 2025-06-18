using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Arrow;
    public Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(Arrow);
        Arrow arrow = obj.GetComponent<Arrow>();
        //arrow.SetTargetFly(Target, this);
    }

 
}
