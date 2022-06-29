using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjWeapon : MonoBehaviour
{
    [SerializeField] Rigidbody2D proj;
    [SerializeField] float fireRate = 0.25f;
    [SerializeField] float projSpeed = 1500f;
    [SerializeField] Animation anim;
    [SerializeField] AnimationClip shotAnim;
    [SerializeField] AudioClip shotSound;

    float counter;

    void Update ()
    {
        LookAt(transform);

        counter += Time.deltaTime;
        if (Input.GetMouseButton(0) && counter >= fireRate) 
        {
            anim.Rewind(shotAnim.name);
            anim.Play(shotAnim.name);
            GetComponent<AudioSource>().PlayOneShot(shotSound);
            ShootProj(); // can have loop for x shot projs
            counter = 0;
        }
    }

    void LookAt (Transform obj) 
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(obj.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void ShootProj () 
    {
        Rigidbody2D p = Instantiate(proj, transform.position, Quaternion.identity);
        LookAt(p.transform);
        p.GetComponent<Rigidbody2D>().AddForce(p.transform.right * projSpeed);
    }
}
