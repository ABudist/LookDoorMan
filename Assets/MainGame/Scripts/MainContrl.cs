using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainContrl : MonoBehaviour
{

    public float Gravity = 20;
    public float speed;
    public float CheckGroundDis = 3;
    private CharacterController mController;
    Vector3 vector = new Vector3();
    bool isLook = true;
    bool goR = false;
    bool goL = false;
    int goRi = 0;
    int goLi = 0;
    bool LeftHit = false;
    bool RithgHit = false;
    bool isFalling = false;

    RaycastHit RayHitLetf;
    RaycastHit RayHitRight;
    int layerMask = 1 << 10;


    // Start is called before the first frame update
    void Start()
    {
        mController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveManager();
    }


    void MoveManager()
    {       
        if(MangerGame.inst.IsDead)
        {
            return;
        }
        //Left check Line
        Debug.DrawRay(transform.position - new Vector3(0.5f, 0, 0), Vector3.down, Color.green, 5f);
        //Right check Line
        Debug.DrawRay(transform.position + new Vector3(0.5f, 0, 0), Vector3.down, Color.red, 5f);       
        RotateMe();
    }

    void RotateMe()
    {
        if (Input.GetKey(KeyCode.D) && isLook)
        {
            isLook = false;
            goR = true;
            goRi = 0;
            vector = transform.position;
            transform.RotateAround(new Vector3(vector.x + 0.5f, vector.y - 0.5f, vector.z), new Vector3(0, 0, -1), 6);
        }
        if (goR)
        {
            goRi += 6;
            if (goRi < 90)
            {
                //Debug.Log(transform.eulerAngles.x);
                transform.RotateAround(new Vector3(vector.x + 0.5f, vector.y - 0.5f, vector.z), new Vector3(0, 0, -1), 6);
            }
            else
            {

                if (Physics.Raycast(transform.position - new Vector3(0.5f, 0, 0), Vector3.down, out RayHitLetf, CheckGroundDis, layerMask)
                    || Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector3.down, out RayHitLetf, CheckGroundDis, layerMask)
                    )
                {
                    isLook = true;
                    goR = false;
                    goRi = 0;
                    isFalling = false;
                    MangerGame.inst.CreatSound(1);
                }
                else
                {
                    Vector3 mDir = Vector3.zero;
                    mDir = transform.TransformDirection(mDir);
                    float y = mDir.y - Gravity * Time.deltaTime;
                    mDir = new Vector3(mDir.x, y, mDir.z);
                    mController.Move(mDir);
                    isFalling = true;
                }
            }
        }
        if (Input.GetKey(KeyCode.A) && isLook)
        {
            isLook = false;
            goL = true;
            vector = transform.position;
            transform.RotateAround(new Vector3(vector.x - 0.5f, vector.y - 0.5f, vector.z), new Vector3(0, 0, 1), 6);
        }
        if (goL)
        {
            goLi += 6;
            //if rotate not finish continue rotate ,we just need 90 degree
            if (goLi < 90)
            {
                transform.RotateAround(new Vector3(vector.x - 0.5f, vector.y - 0.5f, vector.z), new Vector3(0, 0, 1), 6);
            }
            else
            {
                if (Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector3.down, out RayHitLetf, CheckGroundDis, layerMask)
                    || Physics.Raycast(transform.position - new Vector3(0.5f, 0, 0), Vector3.down, out RayHitLetf, CheckGroundDis, layerMask)
                    )
                {
                    isLook = true;
                    goL = false;
                    goLi = 0;
                    isFalling = false;
                    MangerGame.inst.CreatSound(1);
                }
                //if the gree ray hit nothing , just go down
                else
                {
                    Vector3 mDir = Vector3.zero;
                    mDir = transform.TransformDirection(mDir);
                    float y = mDir.y - Gravity * Time.deltaTime;
                    mDir = new Vector3(mDir.x, y, mDir.z);
                    mController.Move(mDir);
                    isFalling = true;

                }
            }
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("coin"))
        {
            MangerGame.inst.GetCoin(1);
            MangerGame.inst.CreatFx(0, other.transform);
            MangerGame.inst.CreatSound(0);
            Destroy(other.gameObject);
        }
        if (other.tag.Equals("win"))
        {
            MangerGame.inst.GameWin();
        }
        if (other.tag.Equals("dead"))
        {
            this.gameObject.SetActive(false);
            MangerGame.inst.CreatSound(2);
            MangerGame.inst.CreatFx(1, other.transform);
            MangerGame.inst.GameLost();
        }

    }

}
