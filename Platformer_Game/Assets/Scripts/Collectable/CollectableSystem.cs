using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableSystem : MonoBehaviour
{
    [SerializeField] private int count;
    [SerializeField] private TMP_Text text;
    const string fruitEarnSound = "FruitEarn";
    public void Add(int amount)
    {
        SoundManager.Instance.PlaySound(fruitEarnSound);
        count += amount;
        text.text = count.ToString();
    }
}
