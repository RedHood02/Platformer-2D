using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    [SerializeField] bool hasKey;


    public void SetHasKey(bool newBool)
    {
        hasKey = newBool;
    }

    public bool GetHasKey()
    {
        return hasKey;
    }
}
