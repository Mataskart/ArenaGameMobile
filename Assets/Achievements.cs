using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    public ListView achievementsList;

    void fillList()
    {
        ListView.ListItem item = new ListView.ListItem();
        item.row0 = new ListView.ListRow();
        item.row0.rowText = "Row text";
        achievementsList.listItems.Add(item);
        achievementsList.InitializeItems();
    }
}
