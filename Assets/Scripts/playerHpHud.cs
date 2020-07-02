using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHpHud : MonoBehaviour {

    /// <summary>
    /// Using a list so we can easily add to in the future
    /// if you want the player to gain additional health
    /// </summary>
    private List<GameObject> totalIcons = new List<GameObject>();

    /// <summary>
    /// Object to represent the player health
    /// </summary>
    public GameObject _Icon;
    public Transform _CanvasPanel;

    /// <summary>
    /// Sprites to show health.
    /// Can be expanded with quarter hearts
    /// </summary>
    public Sprite _FullHeart; 
    public Sprite _EmptyHeart; 
    public Sprite _HalfHeart; 
    public Sprite _QuarterHeart; 
    public Sprite _ThreequarterHeart;

    /// <summary>
    /// int are easier to deal with than doubles 
    /// when trying to deal with quarter hearts.
    /// </summary>    
    public int _MaxHP;
    public int Hp;

    public int _MaxNumHPIcons;
    public int _VisibleNumHPIcons;
    public int _HeartSegments;

    public Vector2 _IconStartPos;
    public Vector2 _IconPosOffset;
    public int _IconsPerRow;

    /// <summary>
    /// Set up for the health system.
    /// </summary>
    void Start () {
        _MaxHP = _HeartSegments * _MaxNumHPIcons;
        Hp = _MaxHP;
    }

    /// <summary>
    /// Increase max health by one and adds
    /// another gui heart. 
    /// 
    /// Called from other scripts.
    /// </summary>
    public void IncreaseMaxHealth()
    {
        float offset = (1.0f * totalIcons.Count);
        totalIcons.Add(Instantiate(_Icon, new Vector3(-10 + offset, 5, 0), Quaternion.identity));

        _MaxNumHPIcons++;
        _MaxHP = _MaxNumHPIcons * _HeartSegments;

        UpdateHUD();
    }

    /// <summary>
    /// Set what you want the current health to be. 
    /// Keep in mind you are overriding whatever the
    /// current HP is set at.
    /// 
    /// Use DeltaCurrentHealth to add or subtract from current HP instead.
    /// </summary>
    /// <param name="inc_newCurrentHP"> What the new current health will be</param>
    public void SetCurrentHealth(int inc_newCurrentHP)
    {
        if (Hp + inc_newCurrentHP > _MaxHP)
            Hp = _MaxHP;
        else if (Hp - inc_newCurrentHP < 0)
            Hp = 0;
        else 
            Hp = inc_newCurrentHP;

        UpdateHUD();
    }

    /// <summary>
    /// Add or subtract from current HP. 
    /// Don't have to worry about what Current HP is at.
    /// </summary>
    /// <param name="inc_deltaCurrentHP">How much to change the Current HP by.</param>
    public void DeltaCurrentHealth(int inc_deltaCurrentHP)
    {
        if (Hp + inc_deltaCurrentHP > _MaxHP)
            Hp = _MaxHP;
        else if (Hp - inc_deltaCurrentHP < 0)
            Hp = 0;
        else
            Hp += inc_deltaCurrentHP;

        UpdateHUD();
    }

    public void ResetHUD()
    {
        for (int i = 0; i < totalIcons.Count; i++)
            DestroyImmediate(totalIcons[i]);

        totalIcons.Clear();
    }

    /// <summary>
    /// Creates the max number of Icons desired.
    /// </summary>
    public void BuildHUD()
    {
        for (int i = totalIcons.Count; i < _MaxNumHPIcons; i++)
            totalIcons.Add(Instantiate(_Icon, _IconStartPos, Quaternion.identity, _CanvasPanel));
        
        UpdateHUD();
    }

    /// <summary>
    /// Draws the HP HUD based on instance variables
    /// </summary>
    private void UpdateHUD()
    {
        double wholePoints = Hp / _HeartSegments;
        int row = 0;
        int col;

        for (int i = 0; i < _MaxNumHPIcons; i++)
        {
            col = i % _IconsPerRow;

            if (col == 0 && i != 0)
                row++;

            if (i >= _VisibleNumHPIcons)
            {
                totalIcons[i].SetActive(false);
                continue;
            }
            totalIcons[i].SetActive(true);

            Vector3 pos = _IconStartPos;
            pos += (_IconPosOffset.x * Vector3.right * col) - (Vector3.up * _IconPosOffset.y * row);

            totalIcons[i].transform.position = pos;

            if (wholePoints >= 1)
            {
                // full hearts
                totalIcons[i].GetComponent<SpriteRenderer>().sprite = _FullHeart;
                wholePoints--;
            }
            else
            {
                // Change sprite based on health left
                switch (wholePoints)
                {
                    case 0.75:
                        wholePoints -= 0.75;
                        totalIcons[i].GetComponent<SpriteRenderer>().sprite = _ThreequarterHeart;
                        break;

                    case 0.5:
                        wholePoints -= 0.5;
                        totalIcons[i].GetComponent<SpriteRenderer>().sprite = _HalfHeart;
                        break;

                    case 0.25:
                        wholePoints -= 0.25;
                        totalIcons[i].GetComponent<SpriteRenderer>().sprite = _QuarterHeart;
                        break;

                    default:
                        totalIcons[i].GetComponent<SpriteRenderer>().sprite = _EmptyHeart;
                        break;
                }
            }

        }
    }

    /// <summary>
    /// Add icons to the health bar.
    /// </summary>
    /// <param name="inc_number">Number greater than 0</param>
    public void AddHPIcon(int inc_number)
    {
        if (inc_number > 0 && 
            _VisibleNumHPIcons + inc_number < _MaxNumHPIcons)
        {
            _VisibleNumHPIcons += inc_number;

            UpdateHUD();
        }
    }

    /// <summary>
    /// Removes icons from the health bar
    /// </summary>
    /// <param name="inc_number">Number greater than 0</param>
    public void RemoveHPIcon(int inc_number)
    {
        if (inc_number != 0)
        {
            inc_number = Math.Abs(inc_number);
            _VisibleNumHPIcons -= inc_number;

            for(int i = 0; i < inc_number; i++)
            {
                totalIcons[i].SetActive(false);
            }
            
            UpdateHUD();
        }
    }

}
