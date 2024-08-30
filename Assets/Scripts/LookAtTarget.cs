using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour, ILookAtTarget
{
    public Transform TargetPoint { get => targetPoint; }

    [SerializeField]
    private Transform targetPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
