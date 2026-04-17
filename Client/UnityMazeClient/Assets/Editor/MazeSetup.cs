using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using MazeGame.Maze;
using MazeGame.Network;
using MazeGame.Game;

/// <summary>
/// Run via menu: Maze → Setup Scene & Prefabs
/// Creates the MazeCell prefab and wires up all scene objects automatically.
/// </summary>
public static class MazeSetup
{
    [MenuItem("Maze/Setup Scene && Prefabs")]
    public static void Run()
    {
        EnsureFolders();
        var cellPrefab = CreateMazeCellPrefab();
        SetupScene(cellPrefab);
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        AssetDatabase.SaveAssets();
        Debug.Log("[MazeSetup] Done! Press Ctrl+S to save the scene.");
    }

    // ── Prefab ───────────────────────────────────────────────────────────

    static MazeCell CreateMazeCellPrefab()
    {
        var root = new GameObject("MazeCell");

        root.AddComponent<MazeCell>();

        // Dark floor tile
        var floor = root.AddComponent<SpriteRenderer>();
        floor.sprite = BuiltinSprite();
        floor.color  = new Color(0.15f, 0.15f, 0.15f);

        // 4 directional wall slots
        //                    name           localPos               localScale
        AddSlot(root, "Slot_Up",    new Vector3( 0,      0.45f, 0), new Vector3(0.9f, 0.1f, 1));
        AddSlot(root, "Slot_Down",  new Vector3( 0,     -0.45f, 0), new Vector3(0.9f, 0.1f, 1));
        AddSlot(root, "Slot_Left",  new Vector3(-0.45f,  0,    0), new Vector3(0.1f, 0.9f, 1));
        AddSlot(root, "Slot_Right", new Vector3( 0.45f,  0,    0), new Vector3(0.1f, 0.9f, 1));

        var prefabAsset = PrefabUtility.SaveAsPrefabAsset(root, "Assets/Prefabs/MazeCell.prefab");
        Object.DestroyImmediate(root);

        Debug.Log("[MazeSetup] MazeCell prefab saved to Assets/Prefabs/MazeCell.prefab");
        return prefabAsset.GetComponent<MazeCell>();
    }

    static void AddSlot(GameObject parent, string name, Vector3 localPos, Vector3 localScale)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent.transform);
        go.transform.localPosition = localPos;
        go.transform.localScale    = localScale;

        go.AddComponent<WallSlot>();

        // Visual children — one per connection type
        var wall     = MakeVisual(go, "Wall",     new Color(0.80f, 0.80f, 0.80f)); // light grey
        var passage  = MakeVisual(go, "Passage",  new Color(0f,    0f,    0f, 0f)); // transparent
        var boundary = MakeVisual(go, "Boundary", new Color(0.40f, 0.40f, 0.40f)); // dark grey
        var exit     = MakeVisual(go, "Exit",     new Color(0.20f, 0.90f, 0.20f)); // green

        // Wire private [SerializeField] fields via SerializedObject
        var so = new SerializedObject(go.GetComponent<WallSlot>());
        so.FindProperty("wallVisual").objectReferenceValue     = wall;
        so.FindProperty("passageVisual").objectReferenceValue  = passage;
        so.FindProperty("boundaryVisual").objectReferenceValue = boundary;
        so.FindProperty("exitVisual").objectReferenceValue     = exit;
        so.ApplyModifiedPropertiesWithoutUndo();

        // Default state: wall visible, others hidden
        passage.SetActive(false);
        boundary.SetActive(false);
        exit.SetActive(false);
    }

    static GameObject MakeVisual(GameObject parent, string name, Color color)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale    = Vector3.one;
        var sr    = go.AddComponent<SpriteRenderer>();
        sr.sprite = BuiltinSprite();
        sr.color  = color;
        return go;
    }

    // ── Scene ─────────────────────────────────────────────────────────────

    static void SetupScene(MazeCell cellPrefab)
    {
        // MazeGridRoot — moves itself for centering
        var mazeGridGO = new GameObject("MazeGridRoot");
        var mazeGrid   = mazeGridGO.AddComponent<MazeGrid>();
        {
            var so = new SerializedObject(mazeGrid);
            so.FindProperty("cellPrefab").objectReferenceValue    = cellPrefab;
            so.FindProperty("gridContainer").objectReferenceValue = mazeGridGO.transform;
            so.FindProperty("cellSize").floatValue                = 1f;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        // Player
        var playerGO = new GameObject("Player");
        playerGO.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        var playerSR = playerGO.AddComponent<SpriteRenderer>();
        playerSR.sprite       = BuiltinSprite();
        playerSR.color        = Color.yellow;
        playerSR.sortingOrder = 10; // renders on top of cells
        var navigator = playerGO.AddComponent<CellNavigator>();

        // GameManager (also hosts the SignalR client)
        var gmGO          = new GameObject("GameManager");
        var signalRClient = gmGO.AddComponent<MazeSignalRClient>();
        var gameManager   = gmGO.AddComponent<GameManager>();
        {
            var so = new SerializedObject(gameManager);
            so.FindProperty("mazeClient").objectReferenceValue = signalRClient;
            so.FindProperty("mazeGrid").objectReferenceValue   = mazeGrid;
            so.FindProperty("player").objectReferenceValue     = navigator;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        // Camera — fixed at origin, grid moves beneath it
        var cam = Camera.main;
        if (cam != null)
        {
            cam.orthographic     = true;
            cam.orthographicSize = 5f;
            cam.transform.position  = new Vector3(0, 0, -10);
            cam.backgroundColor  = new Color(0.08f, 0.08f, 0.08f);
        }

        Debug.Log("[MazeSetup] Scene objects created: MazeGridRoot, Player, GameManager.");
    }

    // ── Helpers ───────────────────────────────────────────────────────────

    static void EnsureFolders()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        if (!AssetDatabase.IsValidFolder("Assets/Editor"))
            AssetDatabase.CreateFolder("Assets", "Editor");
    }

    static Sprite BuiltinSprite() =>
        AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
}
