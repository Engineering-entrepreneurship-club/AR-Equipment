using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using TMPro;
using RotaryHeart.Lib.SerializableDictionary;


[System.Serializable]
public class EquipmentPrefabDictionary:
    SerializableDictionaryBase<string, GameObject>
{ }

public class EquipmentsMainMode : MonoBehaviour
{
    [SerializeField] EquipmentPrefabDictionary equipmentsPrefab;
    [SerializeField] ARTrackedImageManager imageManager;
    [SerializeField] TMP_Text equipmentName;
    [SerializeField] Toggle infoButton;
    [SerializeField] GameObject detailsPanel;
    [SerializeField] TMP_Text detailsText;



    Camera m_camera;
    int layerMask;

    void Start()
    {
        m_camera = Camera.main;
        layerMask = 1 << LayerMask.NameToLayer("equipment");
    }
    void Update()
    {
        if (imageManager.trackables.count == 0)
        {
            InteractionController.EnableMode("Scan");
        }
        else
        {
            //Ray ray = new Ray(m_camera.transform.position, m_camera.transform.forward);
            //if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            if(Physics.Raycast(m_camera.transform.position, m_camera.transform.forward,  out RaycastHit hit))
                {
                /*                equipment e = hit.collider.GetComponentInParent<equipment>();
                                equipmentName.text = e.equipmentName;
                                detailsText.text = e.description;
                                infoButton.interactable = true;*/
                GameObject go = hit.collider.gameObject;
                if (go.CompareTag("equipment"))
                {
                    equipmentName.text = go.GetComponent<equipment>().equipmentName;
                    detailsText.text = go.GetComponent<equipment>().description;
                    infoButton.interactable = true;
                }
                else
                {
                    equipmentName.text = "";
                    detailsText.text = "";
                    infoButton.interactable = false;
                }

            }
        }
    }
    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImageChanged;
        UIController.ShowUI("Main");
        equipmentName.text = "";
        infoButton.interactable = false;
        detailsPanel.SetActive(false);
        foreach(ARTrackedImage image in imageManager.trackables)
        {
            InstantiateEquipment(image);
        }
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    void InstantiateEquipment(ARTrackedImage image)
    {
        string name = image.referenceImage.name.Split('-')[0];
        if (image.transform.childCount == 0) // make sure the equipment is not in the scene
        {
            ScreenLog.Log($"Instantiate {name}");
            GameObject equipment = Instantiate(equipmentsPrefab[name]);
            equipment.transform.SetParent(image.transform, false); //set the parent of the equipment to the image
        }
        else
        {
            ScreenLog.Log($"{name} already instantiated");
        }
    }

    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage newImage in eventArgs.added)
        {
            InstantiateEquipment(newImage);
        }
    }



}
