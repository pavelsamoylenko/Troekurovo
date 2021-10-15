using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zappar;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject _gamePrefab;
    private bool _userHasPlaced;

    // Start is called before the first frame update
    void Start()
    {
        _gamePrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserPlaced();
    }

    private void CheckUserPlaced()
    {
        if (_userHasPlaced) return;
        
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            _userHasPlaced = true;
            _gamePrefab.SetActive(true);
            enabled = false;
        }
    }
}
