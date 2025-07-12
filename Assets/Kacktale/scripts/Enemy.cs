using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int type;
    public Mobs Enemys;
    public TextMeshProUGUI HpTxt;
    public TextMeshProUGUI NameSpace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HpTxt.text = Enemys.mob[type].HP.ToString();
        NameSpace.text = Enemys.mob[type].Name;
    }
}
