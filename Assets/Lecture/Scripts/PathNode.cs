using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 forwardVec = transform.forward;

        Vector3 fromVec = transform.position + forwardVec * 3f;
        Vector3 toVec = transform.position + forwardVec * -3f;
        Gizmos.DrawLine(fromVec,toVec);
    }
}
