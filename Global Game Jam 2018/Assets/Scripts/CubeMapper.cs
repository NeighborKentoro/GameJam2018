using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMapper : MonoBehaviour {

    public Rect front;
    public Rect top;
    public Rect back;
    public Rect bottom;
    public Rect left;
    public Rect right;

    public int maxRandomMultiplier = 0;

    // Use this for initialization
    //void Update() { //for testing only!~!!!!! -_-
    void Start() {

        Mesh thisMesh = GetComponent<MeshFilter>().mesh;
        Vector2[] uvs = new Vector2[24];

        // Front
        float safeFrontX = front.x;
        if(!Mathf.Approximately(maxRandomMultiplier, 0f)) {
            safeFrontX = Random.Range(0, maxRandomMultiplier) * front.width;
        }
        uvs[0] = new Vector2(safeFrontX, front.y);
        uvs[1] = new Vector2(safeFrontX + front.width, front.y);
        uvs[2] = new Vector2(safeFrontX, front.y + front.height);
        uvs[3] = new Vector2(safeFrontX + front.width, front.y + front.height);

        // Top
        uvs[8] = new Vector2(top.x, top.y);
        uvs[9] = new Vector2(top.x + top.width, top.y);
        uvs[4] = new Vector2(top.x, top.y + top.height);
        uvs[5] = new Vector2(top.x + top.width, top.y + top.height);

        // Back
        float safeBackX = back.x;
        if(!Mathf.Approximately(maxRandomMultiplier, 0f)) {
            safeBackX = Random.Range(0, maxRandomMultiplier) * back.width;
        }
        uvs[7] = new Vector2(safeBackX, back.y);
        uvs[6] = new Vector2(safeBackX + back.width, back.y);
        uvs[11] = new Vector2(safeBackX, back.y + back.height);
        uvs[10] = new Vector2(safeBackX + back.width, back.y + back.height);

        // Bottom
        uvs[13] = new Vector2(bottom.x, bottom.y);
        uvs[14] = new Vector2(bottom.x + bottom.width, bottom.y);
        uvs[12] = new Vector2(bottom.x, bottom.y + bottom.height);
        uvs[15] = new Vector2(bottom.x + bottom.width, bottom.y + bottom.height);

        // Left
        uvs[23] = new Vector2(left.x, left.y);
        uvs[20] = new Vector2(left.x + left.width, left.y);
        uvs[22] = new Vector2(left.x, left.y + left.height);
        uvs[21] = new Vector2(left.x + left.width, left.y + left.height);

        //if the right Rect is unset, just use the left one
        Rect safeRight = (Mathf.Approximately(right.width, 0f) || Mathf.Approximately(right.height, 0f)) ? left : right;

        // Right
        uvs[19] = new Vector2(safeRight.x, safeRight.y);
        uvs[16] = new Vector2(safeRight.x + safeRight.width, safeRight.y);
        uvs[18] = new Vector2(safeRight.x, safeRight.y + safeRight.height);
        uvs[17] = new Vector2(safeRight.x + safeRight.width, safeRight.y + safeRight.height);

        thisMesh.uv = uvs;
    }
}
