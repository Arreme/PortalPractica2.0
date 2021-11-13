using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class PortalUIHandler : MonoBehaviour
{
    private Image _renderer;
    [SerializeField] private Sprite _noPortal;
    [SerializeField] private Sprite _blueOnly;
    [SerializeField] private Sprite _orangeOnly;
    [SerializeField] private Sprite _all;

    [SerializeField] GameObject _orangePortal;
    [SerializeField] GameObject _bluePortal;



    private void Start()
    {
        _renderer = GetComponent<Image>();
    }
    public void updatingUI()
    {
        if(_orangePortal.activeInHierarchy && _bluePortal.activeInHierarchy)
        {
            _renderer.sprite = _all;
        } else if (_orangePortal.activeInHierarchy)
        {
            _renderer.sprite = _orangeOnly;
        } else if (_bluePortal.activeInHierarchy)
        {
            _renderer.sprite = _blueOnly;
        } else
        {
            _renderer.sprite = _noPortal;
        }
    }
}
