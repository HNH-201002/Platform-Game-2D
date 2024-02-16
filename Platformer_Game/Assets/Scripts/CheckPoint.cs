using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject menuEnd;
    [SerializeField] private TMP_Text amountOfFruits;
    [SerializeField] private TMP_Text textFruits;
    [SerializeField] private GameObject cupPrefab;
    [SerializeField] private Transform cupPlace;
    private bool check = false;

    private void Start()
    {
        if (menuEnd != null)
        {
            menuEnd.SetActive(false);
        }
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !check)
        {
            textFruits.text = "Earn Fruits : " + amountOfFruits.text;
            check = true;
            if (menuEnd != null)
            {
                menuEnd.SetActive(true);
            }
            if (animator != null)
            {
                animator.SetTrigger("Win");
            }
            int cupCount = GameManager.Instance.ScoreCalculate(int.Parse(amountOfFruits.text));
            for (int i = 0; i < cupCount; i++)
            {
                InstantiateCup();
            }
        }
    }

    private void InstantiateCup()
    {
        if (cupPrefab != null && cupPlace != null)
        {
            GameObject cup = Instantiate(cupPrefab, cupPlace.position, Quaternion.identity, cupPlace);
            cup.transform.SetParent(cupPlace);
        }
    }
}