using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjWeapon : MonoBehaviour
{

    void Update ()
    {
        LookAt(transform);
    }

    void LookAt (Transform target) 
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(target.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        target.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
