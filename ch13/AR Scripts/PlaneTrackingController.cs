using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneTrackingController : MonoBehaviour {
    [SerializeField] ARSessionOrigin arOrigin = null;
    [SerializeField] GameObject planePrefab = null;

    private ARPlaneManager planeManager;
    private ARRaycastManager raycastManager;
    private GameObject prim;

    void Start() {
        prim = GameObject.CreatePrimitive(PrimitiveType.Cube);
        prim.SetActive(false);
        
        raycastManager = arOrigin.gameObject.AddComponent<ARRaycastManager>();
        
        planeManager = arOrigin.gameObject.AddComponent<ARPlaneManager>();
        planeManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;
        planeManager.planePrefab = planePrefab;
    }
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var hits = new List<ARRaycastHit>();
            if (raycastManager.Raycast(Input.mousePosition, hits, TrackableType.PlaneWithinPolygon)) {
                prim.SetActive(true);
                prim.transform.localScale = new Vector3(.1f, .1f, .1f);

                var pose = hits[0].pose;
                prim.transform.localPosition = pose.position;
                prim.transform.localRotation = pose.rotation;
            }
        }
    }
}
