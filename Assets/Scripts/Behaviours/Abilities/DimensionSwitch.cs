using System;
using UnityEngine;

public class DimensionSwitch : MonoBehaviour
{
    private GameObject normalRealm;
    private GameObject shadowRealm;
    private bool _realmSwitch;

    private void Awake()
    {
        normalRealm = GameObject.Find("NormalRealm");
        shadowRealm = GameObject.Find("ShadowRealm");
    }

    public void UpdateDimentionSwitching()
    {
        _realmSwitch = !_realmSwitch;
    }
    void Update()
    {
        if (_realmSwitch)
        {
            normalRealm.SetActive(false);
            shadowRealm.SetActive(true);
        }
        else
        {
            shadowRealm.SetActive(false);
            normalRealm.SetActive(true);
            
        }
    }
}
