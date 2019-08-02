using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LoveManager : MonoBehaviour {
    
    public static int love = 0;
    public TextMeshProUGUI loveText;

    [SerializeField]
    private int loveInEditor;

    void Start()
    {
        love = loveInEditor;
    }

    void Update ()
    {
        UpdateLoveText();
	}

    void UpdateLoveText()
    {
        if (love <= 0)
        {
            loveText.text = "00";
            return;
        }
        loveText.text = love.ToString();
    }
}
