using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganWindowController : MonoBehaviour
{
    //references to all organs
    public GameObject organOne;
    public GameObject organTwo;
    public GameObject organThree;
 
    //a list of all organs
    private List<GameObject> organList;
    private List<Color> organColorList;

    private static OrganWindowController instance;
    public static OrganWindowController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<OrganWindowController>();
                if(instance == null)
                {
                    instance = new GameObject().AddComponent<OrganWindowController>();  
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        //set up list of organs.
        SetUpOrganList();

        //set up list of organColors
        SetUpOrganColorList();

    }

    private void Start()
    {
      
        //start out with all organs visible.
        SetOrgansInactive(true);

        //initialize scene
        InitScene();
    }


    #region Private Utility Methods
    private void InitScene()
    {
        SetOrganOnOff(0, true);
        SetOrganAlpha(0, .1f);
    }
    private void SetUpOrganColorList()
    {
        organColorList = new List<Color>();
        organColorList.Add(organOne.GetComponent<MeshRenderer>().material.color);
        organColorList.Add(organTwo.GetComponent<MeshRenderer>().material.color);
        organColorList.Add(organThree.GetComponent<MeshRenderer>().material.color);
    }

    private void SetUpOrganList()
    {
        organList = new List<GameObject>();
        organList.Add(organOne);
        organList.Add(organTwo);
        organList.Add(organThree);
    }

    private void SetOrgansInactive(bool onOFf)
    {
        foreach (var organ in organList)
        {
            organ.SetActive(onOFf);
        }
    }
    #endregion

    #region Public Functionality Methods
    public void SetOrganOnOff(int organIndex, bool onOff)
    {
        SetOrgansInactive(false);
        organList[organIndex].SetActive(onOff);
    }

    public void SetOrganAlpha(int organIndex, float alphaValue)
    {
        var activeMaterialColor = organColorList[organIndex];
        activeMaterialColor.a = alphaValue;
        organList[organIndex].GetComponent<MeshRenderer>().sharedMaterial.color = activeMaterialColor;
        Debug.Log(activeMaterialColor.ToString());
    } 

    public int GetOrganCount()
    {
        return organList.Count;
    }

    public string GetOrganName(int organIndex)
    {
        return organList[organIndex].name;
    }
    #endregion
}
