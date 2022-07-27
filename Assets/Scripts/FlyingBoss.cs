using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBoss : MonoBehaviour
{
    [SerializeField] State bossState;
    [SerializeField] Transform target;
    [SerializeField] float stateUpdateInterval = 5f;
    [SerializeField] Transform[] projSpawnPoints;
    [SerializeField] Transform[] crystalBulletSpawns;
    [SerializeField] GameObject laserProj;
    [SerializeField] GameObject verticalSpawn;
    [SerializeField] GameObject crystalBulletProj;

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

    void LookAt (Transform t, Transform target) 
    {
        Vector3 dir = target.position - t.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        t.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void MovePosition (Vector3 target, float speed = 2f) 
    {
        Vector3 dir = target - transform.position;
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
        float tickTime = stateUpdateInterval / 5f;
        float nextTick = stateUpdateInterval;
        while (t >= 0f) 
        {
            t -= Time.deltaTime;
            MovePosition(-target.position, 0.2f);

            if (t <= nextTick) 
            {
                foreach (Transform point in projSpawnPoints) 
                {
                    GameObject proj = Instantiate(laserProj, point.position, Quaternion.identity);
                    LookAt(proj.transform, target);
                    Destroy(proj, 5f);
                }

                nextTick -= tickTime;
            }

            yield return null;
        }
        
        SwitchState();
        yield return null;
    }

    IEnumerator Charging () 
    {
        print("Charging");

        float t = stateUpdateInterval;
        float tickTime = stateUpdateInterval / 10f;
        float nextTick = stateUpdateInterval;
        while (t >= 0f) 
        {
            t -= Time.deltaTime;
            MovePosition(target.position, 1f);

            if (t <= nextTick) 
            {
                foreach (Transform point in crystalBulletSpawns) 
                {
                    GameObject proj = Instantiate(crystalBulletProj, point.position, Quaternion.identity);
                    proj.GetComponent<Rigidbody2D>().AddForce((target.position - point.position) * 175f);
                    Destroy(proj, 5f);
                }
                
                nextTick -= tickTime;
            }

            yield return null;
        }
        
        SwitchState();
        yield return null;
    }

    IEnumerator VerticalShooting () 
    {
        print("VerticalShooting");

        float t = stateUpdateInterval;
        float tickTime = stateUpdateInterval / 5f;
        float nextTick = stateUpdateInterval;
        while (t >= 0f) 
        {
            t -= Time.deltaTime;
            MovePosition(Vector3.zero, 0.2f);

            if (t <= nextTick) 
            {
                //Vector3 pos = new Vector3(target.position.x + 15*i, target.position.y, 0f);
                GameObject proj = Instantiate(verticalSpawn, target.position, Quaternion.identity);
                Destroy(proj, 5f);
                
                nextTick -= tickTime;
            }

            yield return null;
        }
        
        SwitchState();
        yield return null;
    }
}
