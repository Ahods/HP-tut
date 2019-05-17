using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class playerHpHud : MonoBehaviour {

    /// <summary>
    /// Using a list so we can easily add to in the future
    /// if you want the player to gain additional health
    /// </summary>
    [SerializeField]
    private List<GameObject> totalHP;

    /// <summary>
    /// Object to represent teh player health
    /// </summary>
    [SerializeField]
    private GameObject heart;

    /// <summary>
    /// Sprites to show full or no health.
    /// Can be expanded with quarter hearts
    /// </summary>
    private Sprite fullHeart, emptyHeart;

    /// <summary>
    /// int are easier to deal with than doubles 
    /// when trying to deal with quarter hearts.
    /// </summary>
    private int maxHP;
    private int hp;

    /// <summary>
    /// Set up for the health system.
    /// </summary>
    void Start () {
        maxHP = GetComponent<PlayerInfo>().maxHP;
        hp = GetComponent<PlayerInfo>().hp;

        fullHeart = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/hudHeartFull.png", typeof(Sprite));
        emptyHeart = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/hudHeartEmpty.png", typeof(Sprite));

        float offset;

        for (int i = 0; i < hp; i++)
        {
            offset = (1.0f * i);
            Instantiate(heart, new Vector3(-10 + offset, 5, 0), Quaternion.identity);
        }
        
        totalHP = new List<GameObject>(GameObject.FindGameObjectsWithTag("hudHeart"));
    }
    
    /// <summary>
    /// Updates current health for player.
    /// </summary>
    void Update () {
        maxHP = GetComponent<PlayerInfo>().maxHP;
        hp = GetComponent<PlayerInfo>().hp;

        for(int i = 0; i < maxHP; i++)
        {
            if(i < hp)
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

        maxHP++;
    }

    /*
     If you want to use quarter hearts I would suggest having the Max HP be a multiple of 4.
     That way, you just have to check how many times 4 goes into the current health for 
     full hearts. Then take the modulus for how much of a quarter heart to display
     */

}
