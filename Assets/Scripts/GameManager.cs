using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] anomalies;

    [SerializeField]
    private float probability;

    // Start is called before the first frame update
    void Start()
    {

    float f = Random.value;
    Debug.Log(f);

    if (probability > f)
    {

        int i =Random.Range(0, anomalies.Length);
        Debug.Log(i);
        GameObject anomaly = anomalies[i];
        anomaly.SetActive(true);

    }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
