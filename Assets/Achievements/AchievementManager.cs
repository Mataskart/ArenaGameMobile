using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.MUIP;
using JetBrains.Annotations;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEditor;

public class AchievementManager : MonoBehaviour
{
    public AchievementsDatabase achievementsDb;
    public ListView achievementsListView;
    void Start()
    {
        LoadAchievements();
    }

    private void LoadAchievements()
    {
        for (int i = 0; i < achievementsDb.AchievementCount; i++)
        {
            ListView.ListItem achievement = new ListView.ListItem();
            Achievement achievments = achievementsDb.GetAchievement(i);

            bool completed = CheckIfCompleted(achievments);

            if (completed)
            {
                achievement.row0 = new ListView.ListRow();
                achievement.row1 = new ListView.ListRow();
                achievement.row2 = new ListView.ListRow();
                achievement.row0.rowType = ListView.RowType.Icon;
                achievement.row0.iconScale = 1.3f;
                achievement.row1.rowType = ListView.RowType.Text;
                achievement.row2.rowType = ListView.RowType.Text;
                achievement.row0.rowIcon = achievments.achievementSpriteCompleted;
                achievement.row1.rowText = achievments.achievementTitle;
                achievement.row2.rowText = achievments.achievementShortDescription;

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
                achievement.row0.rowIcon = achievments.achievementSpriteBW;
                achievement.row1.rowText = achievments.achievementTitle;
                achievement.row2.rowText = achievments.achievementShortDescription;

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
}