using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DrawLine))]
public class DrawLineEditor : Editor {

    DrawLine t;
    public Material material;
    public Mesh mesh;
    public bool[,] previewMatrix;
    public bool loaded = false;
    public bool creationIsOn = false;
    void OnSceneGUI()
    {
        // get the chosen game object
        t = target as DrawLine;
        GameObject obj = GameObject.Find("Yatah");

        Event e = Event.current;
        switch (e.type)
        {
            case EventType.keyDown:
                {
                    if (Event.current.keyCode == (KeyCode.KeypadMinus))
                    {
                        creationIsOn = false;
                    }
                    else if (Event.current.keyCode == (KeyCode.KeypadPlus))
                    {
                        creationIsOn = true;
                    }
                    break;
                }
        }

        if (!loaded)
        {
            previewMatrix = new bool[Mathf.FloorToInt(68), Mathf.FloorToInt(68)];
            for (int k = 0; k < previewMatrix.GetLength(0); k++)
            {
                for (int l = 0; l < previewMatrix.GetLength(1); l++)
                {
                    previewMatrix[k, l] = false;
                }
            }
            loaded = true;
        }

        if (t == null || t.GameObjects == null)
            return;

        // grab the center of the parent
        Vector3 center = t.transform.position;

        // iterate over game objects added to the array...
        for (int i = 0; i < t.GameObjects.Length; i++)
        {
            // ... and draw a line between them
            if (t.GameObjects[i] != null)
                Handles.DrawLine(center, t.GameObjects[i].transform.position);
        }

        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Handles.DrawLine(Event.current.mousePosition, hit.point);
            //68 IS THE CURRENT GRID SIZE
            int x = Mathf.FloorToInt(hit.point.x + 68 / 2);
            int z = Mathf.FloorToInt(hit.point.z + 68 / 2);



            if (previewMatrix[x, z] == false)
            {
                Vector3 position = CoordToPosition(x, z);
                if (creationIsOn)
                    DoCreateSimplePrefab(position);
            }

            Debug.Log("INSTANTIATED AT " + x + "," + z);
            previewMatrix[x, z] = true;
        }
    }
    public void LoadResources()
    {
        //Load material "MaterialTest" and extract mesh from a prefab named "MeshTest"
        material = Resources.Load("WhiteMat", typeof(Material)) as Material;
        mesh = ((GameObject)Resources.Load("Meshes/Cube")).GetComponent<MeshFilter>().sharedMesh;

    }
    public void DoCreateSimplePrefab(Vector3 pos)
    {
        LoadResources();
        //PrefabUtility.InstantiatePrefab(obj);
        GameObject go = new GameObject("Edge");
        go.transform.position = new Vector3(pos.x, pos.y, pos.z);
        go.transform.localScale = new Vector3(1f, 0.01f, 1f);
        go.transform.parent = t.transform;
        MeshFilter meshFilter = go.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
        meshRenderer.material = material;

        //PrefabUtility.CreateEmptyPrefab("Assets/Temporary/" + t.gameObject.name + ".prefab");

    }


    public Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-68 / 2 + 0.5f + x, 0f + -1, -68 / 2 + 0.5f + y);
    }
}
