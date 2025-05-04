using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SentinelRayCast : MonoBehaviour
{
    [SerializeField] private Vector2 dir = Vector2.right;
    private int numCasts = 20;
    private float angleSpread = 45f;
    private float rayLength = 30f;

    private bool oracleInCast = false;

    private Mesh mesh;
    private MeshFilter meshFilter;


    private void Start()
    {
        dir.Normalize();
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        mesh.name = "VisionCone";
        meshFilter.mesh = mesh;
    }

    private void Update()
    {
        CastCone();
        if (!oracleInCast)
        {
            UIManager.Instance.CancelLightFadeIn();
        }
    }

    private void CastCone()
    {
        oracleInCast = false;

        float halfSpread = angleSpread / 2f;

        Vector3[] vertices = new Vector3[numCasts + 1];
        int[] triangles = new int[(numCasts - 1) * 3];

        vertices[0] = Vector3.zero; // origin of cone

        LayerMask hitMask = LayerMask.GetMask("Player");
        hitMask += LayerMask.GetMask("Objects");
            
        for (int i = 0; i < numCasts; i++)
        {
            float t = (float)i / (numCasts - 1);
            float angleOffset = Mathf.Lerp(-halfSpread, halfSpread, t);
            Vector2 rayDirection = Quaternion.Euler(0, 0, angleOffset) * dir;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayLength, hitMask);
            Debug.DrawRay(transform.position, rayDirection * rayLength, hit.collider ? Color.red : Color.green);

            if (hit.collider && hit.collider.tag == "PlayerHitBox" && !oracleInCast)
            {
                oracleInCast = true;
                UIManager.Instance.FadeInLightOverlay();
            }

            // Use either the hit point or max length
            Vector3 endPoint = (hit.collider ? (Vector3)hit.point : (Vector3)(rayDirection * rayLength));
            vertices[i + 1] = transform.InverseTransformPoint(transform.position + endPoint);
        }

        for (int i = 0; i < numCasts - 1; i++)
        {
            int start = i + 1;
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = start;
            triangles[i * 3 + 2] = start + 1;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    public void SetDirection(Vector2 newDir)
    {
        dir = newDir.normalized;
    }
}
