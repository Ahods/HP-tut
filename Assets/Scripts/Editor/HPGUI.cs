using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerHpHud))]
public class HPGUI : Editor
{
    bool spriteFoldout;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        // 
        PlayerHpHud hpHud = (PlayerHpHud)target;

        IconSetUp(ref hpHud);

        EditorGUILayout.Space();

        IconPostion(ref hpHud);

        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();

    }

    private void IconSetUp(ref PlayerHpHud hpHud)
    {
        hpHud.MaxHPIcons = EditorGUILayout.IntField("Max Hp Icons", hpHud.MaxHPIcons);

        EditorGUILayout.Space();

        //EditorGUILayout.Popup(new GUIContent("movementStepToMoveTo", "YOUR TOOLTIP HERE"), hpHud.HeartSegments, GUILayout.MinHeight(1.0f));
        
        EditorGUILayout.LabelField("HP Segements");

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("1"))
            hpHud.HeartSegments = 1;

        if (GUILayout.Button("2"))
            hpHud.HeartSegments = 2;
        
        if (GUILayout.Button("4"))
            hpHud.HeartSegments = 4;

        GUILayout.EndHorizontal();

        hpHud.MaxHP = hpHud.MaxHPIcons * hpHud.HeartSegments;
        EditorGUILayout.LabelField("Max Hp", hpHud.MaxHP.ToString());

        EditorGUILayout.Space();

        spriteFoldout = EditorGUILayout.Foldout(spriteFoldout, "HP Sprites");
        if (spriteFoldout)
            DisplaySpriteFields(ref hpHud);
        
    }
    private void DisplaySpriteFields(ref PlayerHpHud hpHud)
    {
        hpHud.emptyHeart = (Sprite)EditorGUILayout.ObjectField("Empty HP",hpHud.emptyHeart, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

        if (hpHud.HeartSegments > 2)
            hpHud.quarterHeart = (Sprite)EditorGUILayout.ObjectField("Quarter HP", hpHud.quarterHeart, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

        if(hpHud.HeartSegments > 1)
            hpHud.halfHeart = (Sprite)EditorGUILayout.ObjectField("Half HP", hpHud.halfHeart, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

        if (hpHud.HeartSegments > 2)
            hpHud.threequarterHeart = (Sprite)EditorGUILayout.ObjectField("Three Quarter HP", hpHud.threequarterHeart, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        
        hpHud.fullHeart = (Sprite)EditorGUILayout.ObjectField("Full HP", hpHud.fullHeart, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
    }

    private void IconPostion(ref PlayerHpHud hpHud)
    {
        EditorGUILayout.LabelField("HP Position Info");
        hpHud.IconStartPos = EditorGUILayout.Vector2Field("Start Position", hpHud.IconStartPos);

        hpHud.IconPosOffset = EditorGUILayout.FloatField("HP Position Offset", hpHud.IconPosOffset);

        hpHud.IconRows = EditorGUILayout.IntField("Number of Rows", hpHud.IconRows);        

    }
}
