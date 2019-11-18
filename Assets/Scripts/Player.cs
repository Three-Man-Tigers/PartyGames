using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public GameObject pointOne;
    //public GameObject pointTwo;
    //public GameObject pointThree;

    //Move
    float _moveSpeed = 7.5f;
    float _flySpeed = 5f;
    bool _flyingFlag = false;
    bool _isRidingFlag = false;

    //ATTACK
    LineRenderer line;


    public static Player instance; 
    private void Awake()
    {
        if (instance == null) {
            instance = this ;
        }
    }

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
            line.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.localPosition, pointTwo.transform.localPosition, Color.green);

        MoveControlByTranslate();
        LineAttack();
        
    }

    void MoveControlByTranslate()
    {
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow)) //前
        {
            this.transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow)) //
        {
            this.transform.Translate(Vector3.forward * -_moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) //左
        {
            this.transform.Translate(Vector3.right * -_moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) //右
        {
            this.transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
        }
    }



   

    void LineAttack() {
        if (Input.GetKeyDown("z"))
        {
            if (_isRidingFlag)
            {
                _isRidingFlag = false;
                gameObject.transform.GetChild(0).parent=null;
            }
            else
            {
                StopCoroutine("FireLaser");
                StartCoroutine("FireLaser");
            }
        }
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;

        while (Input.GetKey("z"))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin); //設定 Line Render 的起始點位置
            if (Physics.Raycast(ray, out hit, 100))
            {
                line.SetPosition(1, hit.point); //設定第二個 Line Render 第二個點位置，即可連成一條線
                if (hit.rigidbody && hit.collider.tag.Equals("Monster"))
                {
                    //hit.rigidbody.AddForceAtPosition(transform.forward * 50, hit.point); //如果 ray 打到的物體是剛體，就讓物體作功
                    _flyingFlag = true;
                    Debug.Log(hit.rigidbody.transform.position);
                    transform.Translate(hit.rigidbody.transform.position * _flySpeed * Time.deltaTime);
                }
            }
            else
            {
                line.SetPosition(1, ray.GetPoint(15)); //如果都沒打到物體，就發射 15 這麼長的射線
            }


            yield return null;
        }

        line.enabled = false;
    }


    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster")) {
            if (_flyingFlag)
            {
                Debug.Log("AAA");
                _isRidingFlag = true;
                _flyingFlag = false;
                transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 3, collision.transform.position.z);
                collision.gameObject.transform.parent = gameObject.transform;
            }
            else
            {
                if (!_isRidingFlag) {
                    //TakenDamage
                }
            }
        }
    }

}
