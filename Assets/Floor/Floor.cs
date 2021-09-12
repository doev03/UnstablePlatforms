using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject floorItemPrefab;
    public Vector3 numItems;

    void Start()
    {
        for (int x = 0; x < numItems.x; x++)
        {
            for (int y = 0; y < numItems.y; y++)
            {
                for (int z = 0; z < numItems.z; z++)
                {
                    GameObject floorItem = Instantiate<GameObject>(floorItemPrefab);
                    Vector3 pos = new Vector3();
                    pos.x = x * floorItem.transform.localScale.x;
                    pos.y = y * floorItem.transform.localScale.y;
                    pos.z = z * floorItem.transform.localScale.z;

                    floorItem.transform.position = pos;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
