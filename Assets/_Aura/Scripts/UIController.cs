using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [Header("Game Object to parent panels to.")]
    public RectTransform panelParent;

    public GameObject organSelectPanel;

    public GameObject organSelectButtonPrefab;

    [Header("Base organ control Panel prefab.")]
    public GameObject baseOrganControlPanelPrefab;

    [Header("Base organ Alpha value control slider")]
    public Slider baseOrganAlphaValueSliderPrefab;

    [Header("Base back to organ select button.")]
    public GameObject baseBackToOrganSelectBtnPrefab;

    private List<GameObject> organSelectPanels;

    int panelCount;

    [SerializeField]int currentOrgan = 0;

    private IEnumerator Start()
    {
        organSelectPanels = new List<GameObject>();
        SetUpOrganControlPanels();
        yield return new WaitForSeconds(.05f);
        SetOrganPanelsInactive();
        SetUpOrganSelectPanel();
    }

    private void SetUpOrganControlPanels()
    {
        
       panelCount  = OrganWindowController.Instance.GetOrganCount();
        for (int i = 0; i < panelCount; i++)
        {
            currentOrgan = i;
            //instantiate a panel
            var newPanel = Instantiate(baseOrganControlPanelPrefab);

            //set up panel name
            newPanel.name = OrganWindowController.Instance.GetOrganName(currentOrgan);

            //instantiate a slider
            var sliderObject = Instantiate(baseOrganAlphaValueSliderPrefab);
            var newSlider = sliderObject.GetComponent<Slider>();

            //set the method for slider to listen to
            newSlider.onValueChanged.AddListener((f) => SetOrganAlpha(f));

            //parent slider to respective panel
            sliderObject.transform.SetParent(newPanel.transform, false);

            //instantiate back to select organ button
            var btnObject = Instantiate(baseBackToOrganSelectBtnPrefab);
            var backBtn = btnObject.GetComponent<Button>();

            //set up method for button to listen to
            backBtn.onClick.AddListener(() => SetOrganPanelActiveByName(organSelectPanel));

            //parent button to respective panel
            btnObject.transform.SetParent(newPanel.transform,false);

            //parent panel to panel parent
            newPanel.transform.SetParent(panelParent, false);

            //add panel to list of panel objects
            organSelectPanels.Add(newPanel);
        }
    }

    private void SetUpOrganSelectPanel()
    {
        for(int i = 0;i < panelCount;i++)
        {
            //instantiate an organ select btn
            var btnObject = Instantiate(organSelectButtonPrefab);
            var organBtn = btnObject.GetComponentInChildren<Button>();
            var organNameTxt = organBtn.GetComponentInChildren<Text>();

            //set up button
            organBtn.onClick.AddListener(() => SetPanelActive(organSelectPanels[i]));
            organNameTxt.text = organSelectPanels[i].name;
            btnObject.transform.SetParent(organSelectPanel.transform, false);

        }
    }
    private void SetPanelActive(GameObject panelName)
    {
        //turn all panels inactive
        SetOrganPanelsInactive();

        //turn panel with name matching name of passed in panel object on
        for (int i = 0; i < organSelectPanels.Count; i++)
        {
            organSelectPanels[i].SetActive(organSelectPanels[i].name.Equals(panelName.name));
        }

    }

    private void SetOrganPanelsInactive()
    {
        foreach (var panel in organSelectPanels)
        {
            panel.SetActive(false);
        }
    }

    public void SetOrganPanelActiveByName(GameObject panelName)
    {
        SetPanelActive(panelName);
    }
    public void SetOrganAlpha(float alphaValue)
    {
        OrganWindowController.Instance.SetOrganAlpha(currentOrgan,alphaValue);
    }

    
}
