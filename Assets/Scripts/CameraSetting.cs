using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{

    //status
    public float baseWidth = 750;
    public float baseHeight = 1334;
    public float baseOrthographicSize = 5;

    public GameObject _player;        //Public variable to store a reference to the player game object
    public GameObject _enemy;
    private Vector3 offset;            //Private variable to store the offset distance between the player and camera

    public string _mode;

    void Awake()
    {
        float newOrthographicSize = (float)Screen.height / (float)Screen.width * this.baseWidth / this.baseHeight * this.baseOrthographicSize;
        GetComponent<Camera>().orthographicSize = Mathf.Max(newOrthographicSize, this.baseOrthographicSize);
    }

    // Start is called before the first frame update
    void Start()
    {
        _mode = "Normal";
        offset = transform.position - _player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        CameraMode(_mode);
    }

    public void CameraMode(string mode) {
        switch (mode) {
            case "Normal":
                NormalMode();
                break;
            case "Battle":
                BattleMode();
                break;
            case "Arena":
                ArenaMode();
                break;
            default:
                NormalMode();
                break;
        }
    }

    void NormalMode() {
        transform.position = _player.transform.position + offset;
    }

    void BattleMode()
    {
        if (_enemy == null)
            _enemy = GameObject.FindGameObjectWithTag("Enemy");
        else
        {
            Vector3 vector = ((_player.transform.position + _enemy.transform.position) / 2) + offset;
            transform.position = new Vector3(vector.x, vector.y - 4, vector.z);
        }
    }

    void ArenaMode() {
        Vector3 vector = ((_player.transform.position + _enemy.transform.position) / 2) + offset;
        transform.position = new Vector3(vector.x, vector.y - 4, vector.z+8);
    }

    
}
