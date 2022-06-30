using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralCallFuncFromKeyframe : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] string methodName;

    public void Call () 
    {
        target.SendMessage(methodName, SendMessageOptions.DontRequireReceiver);
    }
}
