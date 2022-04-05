using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ScanMode : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] ARTrackedImageManager imageManager;
    // [SerializeField] ARPlaneManager planeManager;
    void OnEnable()
    {
        UIController.ShowUI("Scan");
    }

    // Update is called once per frame
    void Update()
    {
        if(imageManager.trackables.count > 0)
        {
            InteractionController.EnableMode("Main");
        }
    }
}
