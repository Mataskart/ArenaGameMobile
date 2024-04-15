using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.MUIP;
using JetBrains.Annotations;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEditor;
using TMPro;
public class AchievementManager : MonoBehaviour
{
    public AchievementsDatabase achievementsDb;
    public ListView achievementsListView;
    public TextMeshProUGUI completedTxt;
    void Start()
    {
        completedTxt.text = CountCompleted();
        LoadAchievements();
    }

    private void LoadAchievements()
    {
        for (int i = 0; i < achievementsDb.AchievementCount; i++)
        {
            ListView.ListItem achievement = new ListView.ListItem();
            Achievement achievements = achievementsDb.GetAchievement(i);

            bool completed = CheckIfCompleted(achievements);

            if (completed)
            {
                achievement.row0 = new ListView.ListRow();
                achievement.row1 = new ListView.ListRow();
                achievement.row2 = new ListView.ListRow();
                achievement.row0.rowType = ListView.RowType.Icon;
                achievement.row0.iconScale = 1.3f;
                achievement.row1.rowType = ListView.RowType.Text;
                achievement.row2.rowType = ListView.RowType.Text;
                achievement.row0.rowIcon = achievements.achievementSpriteCompleted;
                achievement.row1.rowText = achievements.achievementTitle;
                achievement.row2.rowText = achievements.achievementShortDescription;
                achievementsListView.listItems.Add(achievement);
            }
            else
            {
                achievement.row0 = new ListView.ListRow();
                achievement.row1 = new ListView.ListRow();
                achievement.row2 = new ListView.ListRow();
                achievement.row0.rowType = ListView.RowType.Icon;
                achievement.row0.iconScale = 1.3f;
                achievement.row1.rowType = ListView.RowType.Text;
                achievement.row2.rowType = ListView.RowType.Text;
                achievement.row0.rowIcon = achievements.achievementSpriteBW;
                achievement.row1.rowText = achievements.achievementTitle;
                achievement.row2.rowText = achievements.achievementShortDescription;

                achievementsListView.listItems.Add(achievement);
            }
        }

        achievementsListView.InitializeItems();
    }

    private bool CheckIfCompleted(Achievement achievement)
    {
        int completed = PlayerPrefs.GetInt(achievement.achievementTitle, 0);

        if (completed == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private string CountCompleted()
    {
        int completedCount = 0;
        for (int i = 0; i < achievementsDb.AchievementCount; i++)
        {
            Achievement achievement = achievementsDb.GetAchievement(i);

            if (PlayerPrefs.GetInt(achievement.achievementTitle, 0) == 1)
            {
                completedCount++;
            }
        }
        string completed = "COMPLETED " + completedCount.ToString() + " / " + achievementsDb.AchievementCount.ToString();
        return completed;
    }
}