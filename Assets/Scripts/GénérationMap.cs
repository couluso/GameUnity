using System.Collections.Generic;
using UnityEngine;

public class GénérationMap : MonoBehaviour
{
    public GameObject sapin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sapin = GameObject.Find("Sapin");
        PlacerSapins();
    }

    private void PlacerSapins()
    {
        for (int x = 1; x < 100; x += 2)
        {
            for (int z = 1; z < 100; z += 5)
            {
                if (Random.Range(0, 20) == 0)
                {
                    Instantiate(sapin, new Vector3(x, -0.5f, z), Quaternion.identity);
                }
            }
        }
    }
}
