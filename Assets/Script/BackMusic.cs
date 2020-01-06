using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackMusic : MonoBehaviour
{
    public AudioClip music;
    private AudioSource back;
    static BackMusic backMusic;

    private void Awake()
    {
        if(backMusic == null)
        {
            backMusic = this;
            back = this.GetComponent<AudioSource>();
            back.loop = true; //設置循環播放  
            back.volume = 0.5f;//設置音量
            back.clip = music;
            back.Play(); //播放背景音樂
            DontDestroyOnLoad(this);
        }
    }
    
}
