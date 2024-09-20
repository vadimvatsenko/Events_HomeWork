using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ResetBtn : MonoBehaviour, IPointerClickHandler
{
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        /*_rectTransform.anchorMin = new Vector2(0, 0.9f);
        _rectTransform.anchorMax = new Vector2(1, 1);*/
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Reset");
    }

}
