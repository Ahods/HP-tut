using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHpHud : MonoBehaviour {

    /// <summary>
    /// Using a list so we can easily add to in the future
    /// if you want the player to gain additional health
    /// </summary>
    private List<GameObject> totalIcons;

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
    public int _Hp;

    public int _NumHPIcons;
    public int _HeartSegments;

    public Vector2 _IconStartPos;
    public float _IconPosOffset;
    public int _IconsPerRow;

    /// <summary>
    /// Set up for the health system.
    /// </summary>
    void Start () {
        _Hp = _MaxHP;

        float offset;

        for (int i = 0; i < _Hp; i++)
        {
            offset = (1.0f * i);
            Instantiate(_Icon, new Vector3(-10 + offset, 5, 0), Quaternion.identity);
        }

        totalIcons = new List<GameObject>(GameObject.FindGameObjectsWithTag("hudHeart"));
    }
    
    /// <summary>
    /// Updates current health for player.
    /// </summary>
    void LateUpdate () {
        _MaxHP = GetComponent<PlayerInfo>().maxHP;
        _Hp = GetComponent<PlayerInfo>().hp;

        for(int i = 0; i < _MaxHP; i++)
        {
            if(i < _Hp)
            {
                // full hearts
                totalIcons[i].GetComponent<SpriteRenderer>().sprite = _FullHeart;
            }
            else
            {
                // empty hearts
                totalIcons[i].GetComponent<SpriteRenderer>().sprite = _EmptyHeart;
            }

        }

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

        _NumHPIcons++;
        _MaxHP = _NumHPIcons * _HeartSegments;
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
        if (_Hp + inc_newCurrentHP > _MaxHP)
            _Hp = _MaxHP;
        else if (_Hp - inc_newCurrentHP < 0)
            _Hp = 0;
        else 
            _Hp = inc_newCurrentHP;
    }

    /// <summary>
    /// Add or subtract from current HP. 
    /// Don't have to worry about what Current HP is at.
    /// </summary>
    /// <param name="inc_deltaCurrentHP">How much to change the Current HP by.</param>
    public void DeltaCurrentHealth(int inc_deltaCurrentHP)
    {
        if (_Hp + inc_deltaCurrentHP > _MaxHP)
            _Hp = _MaxHP;
        else if (_Hp - inc_deltaCurrentHP < 0)
            _Hp = 0;
        else
            _Hp += inc_deltaCurrentHP;
    }

}
