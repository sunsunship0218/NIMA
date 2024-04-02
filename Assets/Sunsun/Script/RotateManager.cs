using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class RotateManager : MonoBehaviour
{
    [Header("Player Settings")]
    //玩家物件與現在面對的方向
    public myDirection _myDirection; 
    public GameObject Player;

    [Header("Map Settings")]
    //地圖平台座標(可以走的)
    public Transform Platform;
    //中間建築座標(不可行走)
    public Transform Building;
    //填補旋轉後的3D平台落差
    public GameObject UnseeCube;
    //方塊大小皆須一樣,可調整
    public float WorldUnits = 5.0f;

    //玩家移動控制腳本
   [SerializeField] PlayerMove playerMove;
    //旋轉角度
    float degree =0;
    //放隱形方塊的List
    List<Transform> UnseeList = new List<Transform>();

    //朝向改變時才生成unseeCube,保存最後一frame的位置             
    myDirection LastDirection;
    float LastDepth;


    void Start()
    {
        //定義玩家初始朝向的地圖方向
        _myDirection = myDirection.Back;
        playerMove = Player.GetComponent<PlayerMove>();
        //強制更新隱形方塊的位置
        Upd_UnseeCube_Data(true);

    }

    void Update()
    {
        //fezMove.IsJump==false
        //沒有跳躍才進行玩家相機與玩家位置的判定
        //--------------------------------------------------------我還需要寫沒有攻擊的時候-----------------------------------------------------------
        if (!playerMove.IsJump)
        {
           
            bool updateLocation = false;
            if (IsOnInvisiCube())
            {
                Debug.Log("IsOninvisiCube");
                //如果移動玩家到附近的平台了
                if (MovePlayerToNearPlatform())
                {
                    Debug.Log("Move");
                    updateLocation = true;
                }

                //如果相機移動方向了
                if (MoveCamToNearPlatfoem())
                {
                    Debug.Log("CamMove");
                    updateLocation = true;
                }

                if (updateLocation)
                {
                    //更新隱形方塊的位置,但不強制rebuild
                    Upd_UnseeCube_Data(false);
                    Debug.Log("UPD");
                }
                
            }
        }
        
        
        Controll();
    }

    private void Controll()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
      
            if (IsOnInvisiCube())
            {
                MovePlayerToNearPlatform();
            }
           
            // BUG HERE
            LastDirection = _myDirection;
            _myDirection = RotateDirectionLeft();//有執行
            //左轉90度
            degree -= 90f;//有執行
            Upd_UnseeCube_Data(false);
            playerMove.Upd_myFacingDirection(_myDirection, degree);//有執行
        }

        //往右走
        else if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (IsOnInvisiCube())
            {
                MovePlayerToNearPlatform();
            }
            LastDirection = _myDirection;
            _myDirection = RotateDirectionRight();
            //右轉90度
            degree += 90f;
            Upd_UnseeCube_Data(false);
            playerMove.Upd_myFacingDirection(_myDirection, degree);
        }
    }
    void Upd_UnseeCube_Data( bool ForceRebuild)
    {
        //沒有遇到需要強制更新隱形方塊位置的狀況
        if (!ForceRebuild)
        {
            //沒有轉動( 最後朝向方向==玩家起始朝向方向  && 沒有深度差'
            if( LastDirection==_myDirection && LastDepth == GetPlayerDepth())
            {
                Debug.Log("Didnt Move");
                return; 
            }
        }
        //刪除舊位置隱形方塊
        foreach(var item in UnseeList)
        {
            //直接Destroy會影響到原有的遊戲場景,歸到(0,0,0,)再刪
            item.position = Vector3.zero;
            Destroy(item.gameObject);
        }
        //建置新位置方塊與存放的List
        UnseeList.Clear();
        float New_Depth = 0f;
        New_Depth = GetPlayerDepth();//與玩家的新位置相同
 
    
    }

    //創造新隱形方塊
    
    //False不在隱形方塊,True在隱形方塊上
    bool IsOnInvisiCube()
    {
        //檢查List中所有cube跟玩家的位置
        foreach (var item in UnseeList)
        {
            //返回隱形方塊位置  - 玩家水平位置的絕對值。水平位置差<方塊邊長, 就代表在隱形方塊上
            if (Mathf.Abs(item.position.x - playerMove.transform.position.x) < WorldUnits && (item.position.z - playerMove.transform.position.z) < WorldUnits)
            {
                //返回隱形方塊位置  - 玩家垂直位置的絕對值。垂直位置差<方塊邊長, 就代表在隱形方塊上,確保玩家在平台上
                if (playerMove.transform.position.y - item.position.y <= WorldUnits + 0.2f && playerMove.transform.position.y - item.position.y > 0) ;
                return true;
            }                         
        }       
        return false;
    }

    //獲取玩家的(x,0,0)跟(0,0,z)
     float GetPlayerDepth()
    {
        float ClosestPoint = 0f;
        // 前後
        if(_myDirection ==myDirection.Front || _myDirection == myDirection.Back)
        {
            ClosestPoint = playerMove.transform.position.z;
        }
        //左右
       else if (_myDirection == myDirection.Left || _myDirection == myDirection.Right)
        {
            ClosestPoint = playerMove.transform.position.x;
        }
        //Debug.Log("Player" +Mathf.Round(ClosestPoint));
        return Mathf.Round(ClosestPoint);
       
    }

    //移動玩家到附近平台(Update判斷過跳躍了,所以以下程式不用判斷了)
    bool  MovePlayerToNearPlatform()
    {
        //遍歷可行走的平台
        foreach (Transform item in Platform)
        {
            //檢查朝向前後
            if(_myDirection == myDirection.Front ||_myDirection == myDirection.Back)
            {
                //水平位置差 < 方塊邊長, 就代表在隱形方塊上,同IsOnInvisiCube()
                if (Mathf.Abs(item.position.x - playerMove.transform.position.x) < WorldUnits + 0.1f)
                {
                    //玩家比平台高,且高約世界單位+0.2f,才會被判定需要調整位置
                    if ( (playerMove.transform.position.y - item.position.y <= WorldUnits + 0.2f && playerMove.transform.position.y - item.position.y > 0))
                    {
                        //保持與平台相同的x,y座標不變,只平移不同的Z(移動到平台位置)
                        playerMove.transform.position = new Vector3(playerMove.transform.position.x, playerMove.transform.position.y, item.position.z);
                        //有移動
                        return true;
                    }
                }
                //朝向左右
                else
                {
                    if (Mathf.Abs(item.position.z - playerMove.transform.position.z) < WorldUnits + 0.1f)
                    {
                        //玩家比平台高,且高約世界單位+0.2f,才會被判定需要調整位置
                        if ((playerMove.transform.position.y - item.position.y <= WorldUnits + 0.2f && playerMove.transform.position.y - item.position.y > 0))
                        {
                            //保持與平台相同的Y,Z座標不變,只平移不同的X(移動到平台位置)
                            playerMove.transform.position = new Vector3(item.position.x, playerMove.transform.position.y, playerMove.transform.position.z);
                            //有移動
                            return true;
                        }
                    }
                }
            }
        }
        //沒動
        return false;
    }
   //移動相機到附近平台
   bool MoveCamToNearPlatfoem()
    {
        bool IsCamMove = false;
        //遍歷可行走的平台
        foreach (Transform item in Platform)
        {
            //檢查朝向前後
            if (_myDirection == myDirection.Front || _myDirection == myDirection.Back)
            {
                //水平位置差 < 方塊邊長, 就代表在隱形方塊上,同IsOnInvisiCube()
                if (Mathf.Abs(item.position.x - playerMove.transform.position.x) < WorldUnits + 0.1f)
                {
                    //玩家比平台高,且高約世界單位+0.2f,才會被判定需要調整位置
                    if ((playerMove.transform.position.y - item.position.y <= WorldUnits + 0.2f && playerMove.transform.position.y - item.position.y > 0 && !playerMove.IsJump))
                    {
                        //移動到最接近相機的平台位置,面相前方則移動到面前的,面相後方則移動到後面的
                        if (_myDirection == myDirection.Front && item.position.z < playerMove.transform.position.z)
                            IsCamMove = true; 

                        if (_myDirection == myDirection.Back && item.position.z > playerMove.transform.position.z)
                            IsCamMove= true;

                        //開始移動相機位置
                        if (IsCamMove)
                        {
                            //保持與平台相同的x,y座標不變,只平移不同的Z(移動到平台位置)
                            playerMove.transform.position = new Vector3(playerMove.transform.position.x, playerMove.transform.position.y, item.position.z);
                            return true;
                        }
                    }
                }
                //朝向左右
                else
                {
                    if (Mathf.Abs(item.position.z - playerMove.transform.position.z) < WorldUnits + 0.1f)
                    {
                        //玩家比平台高,且高約世界單位+0.2f,才會被判定需要調整位置
                        if ((playerMove.transform.position.y - item.position.y <= WorldUnits + 0.2f && playerMove.transform.position.y - item.position.y > 0 && !playerMove.IsJump))
                        {
                            //移動到最接近相機的平台位置,面相前方則移動到面前的,面相後方則移動到後面的
                            if (_myDirection == myDirection.Left && item.position.x < playerMove.transform.position.x)
                                IsCamMove = true;

                            if (_myDirection == myDirection.Right && item.position.x > playerMove.transform.position.x)
                                IsCamMove = true;

                            //開始移動相機位置
                            if (IsCamMove)
                            {
                                //保持與平台相同的x,y座標不變,只平移不同的Z(移動到平台位置)
                                playerMove.transform.position = new Vector3(item.transform.position.x, playerMove.transform.position.y, playerMove.transform.position.z);
                                return true;
                            }
                        }
                    }
                }
            }
        }
        //沒動
        return false;
        
    }

    //右轉後決定轉完的方向在哪裡
   myDirection RotateDirectionRight()
    {
        //強制把enum轉回整數
        int count = (int)(_myDirection);
        count++;
        if (count > 3)
        {
            count = 0;
        }

        //回傳enum值決定方向
        //Debug.Log((myDirection)(count));
        return (myDirection)(count);
    }

    //左轉完後決定轉完的方向在哪裡
    myDirection RotateDirectionLeft()
     {
        //強制把enum轉回整數
        int count = (int)(_myDirection);
        count--;
       
        if (count < 0)
        {
            count = 3;
        }
        //回傳enum值決定方向
       // Debug.Log((myDirection)(count));
        return (myDirection)(count);
       
    }

}

//Player and camera current facing direction
//用enum取代if else,避免重複
public enum myDirection
{
    Front =0,
    Right =1,
    Back=2,
    Left =3,

}