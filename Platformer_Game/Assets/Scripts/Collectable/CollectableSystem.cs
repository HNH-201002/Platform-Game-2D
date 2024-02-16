using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableSystem : MonoBehaviour
{
    [SerializeField] private int count;
    [SerializeField] private TMP_Text text;

    public void Add(int amount)
    {
        count += amount;
        text.text = count.ToString();
    }
}
