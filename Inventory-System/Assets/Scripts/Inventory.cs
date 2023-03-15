using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Check if game object is already dont destroy on load
#region Singleton 

    public static Inventory Instance
    {
        get; private set;
    }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

#endregion

    public List<Weapons> weaponsList;
    public List<ScriptableKey> keysList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddWaepon(Weapons weapon)
    {
        weaponsList.Add(weapon);
    }


    public void AddKey(ScriptableKey key)
    {
        keysList.Add(key);
    }

    public bool HasKey(ScriptableKey key)
    {
        bool bHasKey = false;
        foreach (var item in keysList)
        {
            if (item.keyID == key.keyID)
            {
                bHasKey= true;
                break;
            }
        }
        return bHasKey;
    }
}
