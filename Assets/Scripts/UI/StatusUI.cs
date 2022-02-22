using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : Singleton<StatusUI>
{
    [SerializeField] Image coinImage;
    [SerializeField] Text coinText;

    public void UpdateCoinUI(int amount)
    {
        coinText.text = string.Format("{0:#,##0}", amount);
    }
}

