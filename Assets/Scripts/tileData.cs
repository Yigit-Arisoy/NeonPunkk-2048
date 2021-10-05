using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tileData : MonoBehaviour
{
    public int tileValue = 2;
    public TextMeshPro tileValueText;
    void Start()
    {
        tileValueText.text = tileValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
