using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;

public class FighterManager : MonoBehaviour
{
    public FighterDatabase fighterDB;
    public TMP_Text nameText;
    public SpriteRenderer spriteFighter;

    private int selectedOption = 0;

    void Start()
    {
        UpdateFighter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= fighterDB.FighterCount)
        {
            selectedOption = 0;
        }

        UpdateFighter(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = fighterDB.FighterCount - 1;
        }

        UpdateFighter(selectedOption);
    }

    private void UpdateFighter(int selectedOption)
    {
        Fighter fighter = fighterDB.GetFighter(selectedOption);
        spriteFighter.sprite = fighter.fighterSprite;
        nameText.text = fighter.fighterName;
    }
}
