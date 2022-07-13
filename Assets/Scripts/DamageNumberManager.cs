using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumberManager : MonoBehaviour
{
    public static DamageNumberManager instance;

    [SerializeField] TextMeshPro numberObj;

    void Awake () => instance = this;

    public void SpawnNumbers (int num, Color color, Vector3 pos, float despawnTime = 3f, Sprite symbol = null)
    {
        GameObject t = Instantiate(numberObj.gameObject, pos + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f), Quaternion.identity);
        t.GetComponent<TextMeshPro>().text = num.ToString();
        t.GetComponent<TextMeshPro>().color = color;

        if (symbol) t.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = symbol;

        Destroy(t, despawnTime);
    }
}
