using UnityEngine;

public class DimensionSwitch : MonoBehaviour
{
    [SerializeField] private GameObject normalRealm;
    [SerializeField] private GameObject shadowRealm;
    private bool _realmSwitch;
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            _realmSwitch = !_realmSwitch;
        }

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
