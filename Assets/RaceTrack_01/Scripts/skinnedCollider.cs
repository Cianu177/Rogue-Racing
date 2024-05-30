using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//use this for collider meshes that are skinned:
//a mesh collider will be added to the snapshot of your skinned mesh

public class skinnedCollider : MonoBehaviour {

    public bool invisibleCollider = true;

    private Mesh bakedMesh;
    private SkinnedMeshRenderer skinnedMeshRenderer;

	// Use this for initialization
	void Start () {

        bakedMesh = new Mesh();
        skinnedMeshRenderer = this.gameObject.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.BakeMesh(bakedMesh);
        this.gameObject.AddComponent<MeshCollider>();
        this.gameObject.GetComponent<MeshCollider>().sharedMesh = bakedMesh;
        if (invisibleCollider) Destroy(this.gameObject.GetComponent<SkinnedMeshRenderer>());
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
