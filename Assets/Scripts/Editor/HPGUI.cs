using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerHpHud))]
public class HPGUI : Editor
{
    bool spriteFoldout, posFoldout;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        // 
        PlayerHpHud hpHud = (PlayerHpHud)target;

        IconSetUp(ref hpHud);

        EditorGUILayout.Space();
        IconPostion(ref hpHud);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(new GUIContent("Change Number of Icons", "Generally for testing purposes."));
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add"))
            hpHud.AddHPIcon(1);

        if (GUILayout.Button("Remove"))
            hpHud.RemoveHPIcon(1);

        GUILayout.EndHorizontal();
        if (GUILayout.Button("Reset"))
            hpHud.ResetHUD();


        // TODO - Set up if check for all requirments meet to build a HUD
        //hpHud.UpdateHUD();

        UpdateHUD(ref hpHud);
    }

    private void IconSetUp(ref PlayerHpHud hpHud)
    {
        hpHud._MaxNumHPIcons = EditorGUILayout.IntField(
            new GUIContent("Hp Icons", "How many icons total"), hpHud._MaxNumHPIcons);

        hpHud._VisibleNumHPIcons = EditorGUILayout.IntSlider(
            new GUIContent("Visible Hp Icons", "How many icons to show right now"), 
            hpHud._VisibleNumHPIcons, 1, hpHud._MaxNumHPIcons);

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

        hpHud._MaxHP = hpHud._MaxNumHPIcons * hpHud._HeartSegments;
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
        posFoldout = EditorGUILayout.Foldout(posFoldout, "HP Position Info");

        if (posFoldout)
        {
            hpHud._IconStartPos = EditorGUILayout.Vector2Field("Start Position", hpHud._IconStartPos);

            hpHud._IconPosOffset = EditorGUILayout.Vector2Field(new GUIContent("HP Position Offset", " Space in between icons' x and y position."), hpHud._IconPosOffset);

            //hpHud._IconsPerRow = EditorGUILayout.IntField(new GUIContent("Icons Per Row", "How many Health Icons do you want in a row."), hpHud._IconsPerRow);

            hpHud._IconsPerRow = EditorGUILayout.IntSlider(
            new GUIContent("Icons Per Row", "How many icons in a row."),
            hpHud._IconsPerRow, 1, hpHud._MaxNumHPIcons);

            hpHud._CanvasPanel = (Transform)EditorGUILayout.ObjectField(new GUIContent("Canvas Parent", "Canvas GameObject you want to parent the health too."),
                hpHud._CanvasPanel, typeof(Transform), true);
        }

    }

    private void UpdateHUD(ref PlayerHpHud hpHud)
    {
        if (hpHud._MaxNumHPIcons > 0 && hpHud._EmptyHeart != null && hpHud._FullHeart != null &&
            hpHud._HeartSegments > 0 && hpHud._Icon != null) 
        {
            hpHud.BuildHUD();
        }

    }
}
