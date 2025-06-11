using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CategoryButton : MonoBehaviour
{
    public string categoryID;

    public void OnClick()
    {
        InventoryInfoManager.Instance.ShowInfoByCategory(categoryID);
    }
}
