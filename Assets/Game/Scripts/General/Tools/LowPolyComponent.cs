using UnityEngine;

public class LowPolyComponent : MonoBehaviour
{
    public void MakeLowPoly()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        if (filter == null) return;

        MeshData meshData = new MeshData(filter, true);

        filter.sharedMesh = meshData.CreateMesh();
    }

    public void MakeSmoothPoly()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        if (filter == null) return;

        MeshData meshData = new MeshData(filter, false);

        filter.sharedMesh = meshData.CreateMesh();
    }
}
