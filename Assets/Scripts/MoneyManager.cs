using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    [SerializeField] int money;
    [SerializeField] TextMeshProUGUI moneyText;

    void Awake () => instance = this;
    void Start () => UpdateMoney();

    public void UpdateMoney (int cnt = 0)
    {
        money += cnt;
        moneyText.text = money.ToString();    
    }
}
