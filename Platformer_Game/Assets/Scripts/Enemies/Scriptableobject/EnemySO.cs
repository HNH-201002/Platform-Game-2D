using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data",menuName ="ScriptableObjects/New Enemy Data" ,order =1)]
public class EnemySO : ScriptableObject
{
    [SerializeField]
    private float normalSpeed;
    [SerializeField]
    private float chaseSpeed;
    [SerializeField]
    [Tooltip("Attack Range")]
    private float radius;
    [SerializeField]
    private float distancePatrol;
    public float NormalSpeed
    {
        get { return normalSpeed; }
        set { normalSpeed = value; }
    }
    public float ChaseSpeed 
    { 
        get { return chaseSpeed; }
        set { chaseSpeed = value; }
    }
    public float Radius
    {
        get { return radius; }
        set { radius = value; }
    }
    public float DistancePatrol
    {
        get { return distancePatrol; }
        set { distancePatrol = value; }
    }
}
