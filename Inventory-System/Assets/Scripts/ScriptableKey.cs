using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableKey : ScriptableObject
{
    public int keyID;
    public string keyName;
    public string keyDescription;
    public string keyMessage;
    public Sprite keySprite;
}
