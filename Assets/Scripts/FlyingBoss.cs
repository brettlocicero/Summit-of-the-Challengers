using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBoss : MonoBehaviour
{
    [SerializeField] State bossState;
    [SerializeField] Transform target;
    [SerializeField] float stateUpdateInterval = 5f;

    Rigidbody2D rb;
    Vector2 targetPos;
    int stateIndex;

    enum State 
    {
        Spray,
        Charge,
        VerticalShoot
    }

    void Start ()
    {
        target = PlayerInstance.instance.transform;
        rb = GetComponent<Rigidbody2D>();
        SwitchState();
    }

    void MovePosition (Vector3 dir, float speed = 2f) 
    {
        rb.MovePosition(transform.position + Time.deltaTime * dir * speed);
    }

    void SwitchState () 
    {
        State state = (State)Random.Range(0, System.Enum.GetValues(typeof(State)).Length);
        switch (state) 
        {
            case State.Spray:
                StartCoroutine(Spraying());
                break;
            case State.Charge:
                StartCoroutine(Charging());
                break;
            case State.VerticalShoot:
                StartCoroutine(VerticalShooting());
                break;
        }
    }

    IEnumerator Spraying () 
    {
        print("Spraying");

        float t = stateUpdateInterval;
        while (t >= 0f) 
        {
            t -= Time.deltaTime;
            Vector3 dir = target.position - transform.position;
            MovePosition(-dir, 0.2f);
            yield return null;
        }
        
        SwitchState();
        yield return null;
    }

    IEnumerator Charging () 
    {
        print("Charging");

        float t = stateUpdateInterval;
        while (t >= 0f) 
        {
            t -= Time.deltaTime;
            Vector3 dir = target.position - transform.position;
            MovePosition(dir, 3f);
            yield return null;
        }
        
        SwitchState();
        yield return null;
    }

    IEnumerator VerticalShooting () 
    {
        print("VerticalShooting");

        float t = stateUpdateInterval;
        while (t >= 0f) 
        {
            t -= Time.deltaTime;
            MovePosition(Vector3.zero, 3f);
            yield return null;
        }
        
        SwitchState();
        yield return null;
    }
}
