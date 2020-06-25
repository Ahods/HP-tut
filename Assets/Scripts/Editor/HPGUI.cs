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
        hpHud._NumHPIcons = EditorGUILayout.IntField(new GUIContent("Hp Icons", "How many icons to draw"), hpHud._NumHPIcons);

        EditorGUILayout.Space();
        
        EditorGUILayout.LabelField(new GUIContent("HP Segements", "States of hp icons minus the empty state."));

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("1"))
            hpHud._HeartSegments = 1;

        if (GUILayout.Button("2"))
            hpHud._HeartSegments = 2;
        
        if (GUILayout.Button("4"))
            hpHud._HeartSegments = 4;

        GUILayout.EndHorizontal();

        hpHud._MaxHP = hpHud._NumHPIcons * hpHud._HeartSegments;
        EditorGUILayout.LabelField(new GUIContent( "Max Hp", "Max HP Icons * Heart Segments"),
           new GUIContent(hpHud._MaxHP.ToString()));

        hpHud._Icon = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Custom GameObject", "Optional: If you want a custom GameObject that'll do more. Must have a SpriteRenderer attached."),
            hpHud._Icon, typeof(GameObject), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        EditorGUILayout.Space();

        spriteFoldout = EditorGUILayout.Foldout(spriteFoldout, "HP Sprites");
        if (spriteFoldout)
            DisplaySpriteFields(ref hpHud);
        
    }
    private void DisplaySpriteFields(ref PlayerHpHud hpHud)
    {
        hpHud._EmptyHeart = (Sprite)EditorGUILayout.ObjectField("Empty HP",hpHud._EmptyHeart, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

        if (hpHud._HeartSegments > 2)
            hpHud._QuarterHeart = (Sprite)EditorGUILayout.ObjectField("Quarter HP", hpHud._QuarterHeart, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

        if(hpHud._HeartSegments > 1)
            hpHud._HalfHeart = (Sprite)EditorGUILayout.ObjectField("Half HP", hpHud._HalfHeart, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

        if (hpHud._HeartSegments > 2)
            hpHud._ThreequarterHeart = (Sprite)EditorGUILayout.ObjectField("Three Quarter HP", hpHud._ThreequarterHeart, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        
        hpHud._FullHeart = (Sprite)EditorGUILayout.ObjectField("Full HP", hpHud._FullHeart, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
    }

    private void IconPostion(ref PlayerHpHud hpHud)
    {
        EditorGUILayout.LabelField("HP Position Info");
        hpHud._IconStartPos = EditorGUILayout.Vector2Field("Start Position", hpHud._IconStartPos);

        hpHud._IconPosOffset = EditorGUILayout.FloatField(new GUIContent("HP Position Offset"," Space in between icons' x and y position."), hpHud._IconPosOffset);

        hpHud._IconsPerRow = EditorGUILayout.IntField(new GUIContent( "Icons Per Row", "How many Health Icons do you want in a row."), hpHud._IconsPerRow);

        hpHud._CanvasPanel = (Transform)EditorGUILayout.ObjectField(new GUIContent("Canvas Parent", "Canvas GameObject you want to parent the health too."),
            hpHud._CanvasPanel, typeof(Transform), true);

    }
}
