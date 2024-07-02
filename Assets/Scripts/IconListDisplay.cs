using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconListDisplay : MonoBehaviour
{
    [SerializeField] private bool updateFlag = false;
    [SerializeField] private List<IHasIconContainer> icons = new();
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private GameObject placeholderTextPrefab;

    public List<IHasIconContainer> Icons 
    { 
        set
        {
            icons = value;
            updateFlag = true;
        } 
        get { return icons; }
    }

    [SerializeField]private List<GameObject> iconObjs = new List<GameObject>();

    private void Update()
    {
        if (updateFlag)
        {
            updateFlag = false;
            UpdateList();
        }
    }

    private void UpdateList()
    {
        while(iconObjs.Count > 0) 
        {
            Destroy(iconObjs[0]);
            iconObjs.RemoveAt(0);
        }

        Vector3 nextPos = transform.position + offset * transform.lossyScale.x;
        foreach(var icon in icons) 
        {
            Sprite image = icon.Result.GetIcon();
            if(image != null)
            {
                GameObject newObject = Instantiate(imagePrefab, nextPos, Quaternion.identity, this.transform.parent);
                newObject.GetComponent<Image>().sprite = image;
                iconObjs.Add(newObject);
            }
            else
            {
                string placeholderText = icon.Result.GetPlaceholderText();
                GameObject newObject = Instantiate(placeholderTextPrefab, nextPos, Quaternion.identity, this.transform.parent);
                newObject.GetComponent<TMPro.TMP_Text>().text = placeholderText;
                iconObjs.Add(newObject);
            }

            nextPos += offset * transform.lossyScale.x;
        }
    }
}
