using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStat stat;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI Name;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = stat.HP.ToString();
        Name.text = stat.Name;
    }
}
