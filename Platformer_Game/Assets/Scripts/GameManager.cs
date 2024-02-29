using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private List<FoodGenerator> foodGenerators;
    [SerializeField] private int maximumFood;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            foodGenerators = new List<FoodGenerator>();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    public void ReloadRegisterFoodGenerator()
    {
        maximumFood = 0;
        foodGenerators = new List<FoodGenerator>();
    }
    public void RegisterFoodGenerator(FoodGenerator generator)
    {
        if (!foodGenerators.Contains(generator))
        {
            foodGenerators.Add(generator);
            maximumFood += generator.GetAmountOfFood();
        }
    }
    public int ScoreCalculate(int foodPlayerEarn)
    {
        int cup = Mathf.FloorToInt(foodPlayerEarn / maximumFood * 3);
        return cup == 0 ? 1 : cup;
    }
    // use for button event
    private void OnEnable()
    {
        Health.OnPlayerDied += ReloadRegisterFoodGenerator;
        SceneManager.sceneLoaded += ReloadRegisterFoodGenerator;
    }

    private void ReloadRegisterFoodGenerator(Scene arg0, LoadSceneMode arg1)
    {
        ReloadRegisterFoodGenerator();
    }

    private void OnDisable()
    {
        Health.OnPlayerDied -= ReloadRegisterFoodGenerator;
        SceneManager.sceneLoaded -= ReloadRegisterFoodGenerator;
    }
}
