using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu]
public class FighterDatabase : ScriptableObject
{
    public Fighter[] fighter;

    public int FighterCount
    {
        get
        {
            return fighter.Length;
        }
    }

    public Fighter GetFighter(int index)
    {
        return fighter[index];
    }
}
