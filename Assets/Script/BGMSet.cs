using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSet : MonoBehaviour
{
    AudioSource BGM;
    public Slider slider;
    public Toggle musicOnToggle;
    public Toggle musicOffToggle;

    // Start is called before the first frame update
    void Start()
    {
        BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        slider.value = BGM.volume;

        if(BGM.isActiveAndEnabled)
        {
            musicOnToggle.isOn = true;
        }
        else
        {
            musicOffToggle.isOn = true;
        }
        
           
    }

    private void Update()
    {
        BGM.volume = slider.value;
        BGM.enabled = musicOnToggle.isOn;
    }
}
