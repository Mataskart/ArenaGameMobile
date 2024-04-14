using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu]
public class AchievementsDatabase : ScriptableObject
{
    public Achievement[] achievement;

    public int AchievementCount
    {
        get
        {
            return achievement.Length;
        }
    }
    public Achievement GetAchievement(int index)
    {
        return achievement[index];
    }
}