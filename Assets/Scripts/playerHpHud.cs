using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHpHud : MonoBehaviour {

    /// <summary>
    /// Using a list so we can easily add to in the future
    /// if you want the player to gain additional health
    /// </summary>
    [SerializeField]
    private List<GameObject> totalHP;

    /// <summary>
    /// Object to represent the player health
    /// </summary>
    [SerializeField]
    private GameObject heart;

    [Header("Heart Sprites")]
    /// <summary>
    /// Sprites to show health.
    /// Can be expanded with quarter hearts
    /// </summary>
    public Sprite fullHeart; 
    public Sprite emptyHeart; 
    public Sprite halfHeart; 
    public Sprite quarterHeart; 
    public Sprite threequarterHeart;

    /// <summary>
    /// int are easier to deal with than doubles 
    /// when trying to deal with quarter hearts.
    /// </summary>
    public int MaxHP;
    public int Hp;

    public int MaxHPIcons;
    public int HeartSegments;

    public Vector2 IconStartPos;
    public float IconPosOffset;
    public int IconRows;

    /// <summary>
    /// Set up for the health system.
    /// </summary>
    void Start () {
        Hp = MaxHP;

        float offset;

        for (int i = 0; i < Hp; i++)
        {
            offset = (1.0f * i);
            Instantiate(heart, new Vector3(-10 + offset, 5, 0), Quaternion.identity);
        }
        
        totalHP = new List<GameObject>(GameObject.FindGameObjectsWithTag("hudHeart"));
    }
    
    /// <summary>
    /// Updates current health for player.
    /// </summary>
    void LateUpdate () {
        MaxHP = GetComponent<PlayerInfo>().maxHP;
        Hp = GetComponent<PlayerInfo>().hp;

        for(int i = 0; i < MaxHP; i++)
        {
            if(i < Hp)
            {
                // full hearts
                totalHP[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
            }
            else
            {
                // empty hearts
                totalHP[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
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
        float offset = (1.0f * totalHP.Count);
        totalHP.Add(Instantiate(heart, new Vector3(-10 + offset, 5, 0), Quaternion.identity) as GameObject);

        MaxHP++;
    }

    /*
     If you want to use quarter hearts I would suggest having the Max HP be a multiple of 4.
     That way, you just have to check how many times 4 goes into the current health for 
     full hearts. Then take the modulus for how much of a quarter heart to display
     */

}
