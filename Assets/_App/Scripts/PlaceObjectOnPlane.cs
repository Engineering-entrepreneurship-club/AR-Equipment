using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectOnPlane : MonoBehaviour
{
    ARRaycastManager raycaster;
    [SerializeField] GameObject placedPrefab;
    GameObject spawnedObject;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycaster = GetComponent<ARRaycastManager>();

    }
    void OnPlaceObject(InputValue value)
    {
        // get the screen touch position
        Vector2 touchPosition = value.Get<Vector2>();

        // raycast from the touch position into the 3D scene lookinf for a plane or a object
        // check the raycast hit a plane 
        // if (raycaster.Raycast())
        if (raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;


            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);

            }
        }

    }

}
