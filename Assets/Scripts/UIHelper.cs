using UnityEngine;
using UnityEngine.EventSystems;

public class UIHelper : MonoBehaviour
{
    private bool isPointerOverUi;

    private void Update()
    {
        PointerOverUi();
    }

    private void PointerOverUi()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            isPointerOverUi = true;
            return;
        }
        isPointerOverUi = false;
    }

    public bool PointerStatus()
    {
        return isPointerOverUi;
    }
}