using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{

    public RectTransform homePanel, arPanel, bricksMenuPanel;
    public GameObject menuBtn;

    private bool bricksMenuActive = false;
    private bool menuBtnActive = true;
    

    public void HideHomePanel() {
        homePanel.DOAnchorPos(new Vector2(-1000, 0), .25f).OnComplete(()=> Deactivate(homePanel));
        ShowArPanel();
    }

    public void ShowArPanel() {
        Activate(arPanel);
        arPanel.DOAnchorPos(Vector2.zero, .25f);

    }


    public void ToggleMenuBtn() {

        if (menuBtnActive)
        {
            menuBtn.SetActive(false);
        }
        else {
            menuBtn.SetActive(true);
        }

        menuBtnActive =  !menuBtnActive;

    }

    public void ToggleBrickMenu() {
        ToggleMenuBtn();

        if (bricksMenuActive)
        {
            bricksMenuPanel.DOAnchorPos(new Vector3(0, -1000f), .25f).OnComplete(() => Deactivate(bricksMenuPanel));

        }
        else {
            Activate(bricksMenuPanel);
            bricksMenuPanel.DOAnchorPos(Vector2.zero, .25f);

            

        }
        bricksMenuActive = !bricksMenuActive;

    }


    private void Deactivate(Transform panel) {
        panel.gameObject.SetActive(false);

    }

    private void Activate(Transform panel) {
        panel.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
