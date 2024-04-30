using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public AchievementsDatabase achievementsDatabase;
    [SerializeField] private NotificationManager notificationManager;

    void Update()
    {
        CheckLast();
    }

    public void CompleteAchievement(string name)
    {
        bool alreadyCompleted = CheckAchievement(name);

        if (!alreadyCompleted)
        {
            PlayerPrefs.SetInt(name, 1);
            PlayerPrefs.Save();
            ShowNotification(name);
        }
    }
    private bool CheckAchievement(string name)
    {
        int achievement = PlayerPrefs.GetInt(name, 0);

        if (achievement == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void ShowNotification(string name)
    {
        for (int i = 0; i < achievementsDatabase.AchievementCount; i++)
        {
            Achievement achievement = achievementsDatabase.GetAchievement(i);

            if (achievement.achievementTitle == name)
            {
                notificationManager.description = achievement.achievementShortDescription;
                notificationManager.UpdateUI();
                notificationManager.Open();
            }
        }
    }

    public void CheckLast()
    {
        int count = 0;
        for (int i = 0; i < achievementsDatabase.AchievementCount; i++)
        {
            Achievement achievement = achievementsDatabase.GetAchievement(i);

            if (!achievement.achievementTitle.Equals("CHAMPION OF ARENA"))
            {
                if (CheckAchievement(achievement.achievementTitle))
                {
                    count++;
                }
            }
        }

        if (count == achievementsDatabase.AchievementCount - 1)
        {
            CompleteAchievement("CHAMPION OF ARENA");
        }
    }
}
