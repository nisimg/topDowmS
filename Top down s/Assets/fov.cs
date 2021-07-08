using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class fov : MonoBehaviour
{
    Mesh mesh;
    [SerializeField] LayerMask mask;
    [SerializeField] float Fov = 90f;
    [SerializeField] float viewDis = 50f;
    Vector3 origin = Vector3.zero;
    private float startingAngle;
    private void Start()
    {

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
     
        origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        
        int rayCount = 50; 
        float angle = startingAngle;
        float angleIncrese = Fov/ rayCount;
        
        Vector3[] vertices = new Vector3[rayCount +1+1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount *3];

        vertices[0] = origin;
        int vertxIndex = 1;
        int TriIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;    
            RaycastHit2D hit = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDis, mask);

            if (hit.collider  == null)
            {
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDis;
            }
            else
            {
                vertex = hit.point;
            }
            
            vertices[vertxIndex] = vertex;

            if (i > 0)
            {
                triangles[TriIndex + 0] = 0;
                triangles[TriIndex + 1] = vertxIndex - 1;
                triangles[TriIndex + 2] = vertxIndex;

                TriIndex += 3;
            }
            vertxIndex++;
            angle -= angleIncrese;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);

    }
    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + Fov / 2f;
    }

    public void SetFoV(float fov)
    {
        this.Fov = fov;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDis = viewDistance;
    }


}
