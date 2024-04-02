using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class RotateManager : MonoBehaviour
{
    [Header("Player Settings")]
    //���a����P�{�b���諸��V
    public myDirection _myDirection; 
    public GameObject Player;

    [Header("Map Settings")]
    //�a�ϥ��x�y��(�i�H����)
    public Transform Platform;
    //�����ؿv�y��(���i�樫)
    public Transform Building;
    //��ɱ���᪺3D���x���t
    public GameObject UnseeCube;
    //����j�p�Ҷ��@��,�i�վ�
    public float WorldUnits = 5.0f;

    //���a���ʱ���}��
   [SerializeField] PlayerMove playerMove;
    //���ਤ��
    float degree =0;
    //�����Τ����List
    List<Transform> UnseeList = new List<Transform>();

    //�¦V���ܮɤ~�ͦ�unseeCube,�O�s�̫�@frame����m             
    myDirection LastDirection;
    float LastDepth;


    void Start()
    {
        //�w�q���a��l�¦V���a�Ϥ�V
        _myDirection = myDirection.Back;
        playerMove = Player.GetComponent<PlayerMove>();
        //�j���s���Τ������m
        Upd_UnseeCube_Data(true);

    }

    void Update()
    {
        //fezMove.IsJump==false
        //�S�����D�~�i�檱�a�۾��P���a��m���P�w
        //--------------------------------------------------------���ٻݭn�g�S���������ɭ�-----------------------------------------------------------
        if (!playerMove.IsJump)
        {
           
            bool updateLocation = false;
            if (IsOnInvisiCube())
            {
                Debug.Log("IsOninvisiCube");
                //�p�G���ʪ��a����񪺥��x�F
                if (MovePlayerToNearPlatform())
                {
                    Debug.Log("Move");
                    updateLocation = true;
                }

                //�p�G�۾����ʤ�V�F
                if (MoveCamToNearPlatfoem())
                {
                    Debug.Log("CamMove");
                    updateLocation = true;
                }

                if (updateLocation)
                {
                    //��s���Τ������m,�����j��rebuild
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
            _myDirection = RotateDirectionLeft();//������
            //����90��
            degree -= 90f;//������
            Upd_UnseeCube_Data(false);
            playerMove.Upd_myFacingDirection(_myDirection, degree);//������
        }

        //���k��
        else if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (IsOnInvisiCube())
            {
                MovePlayerToNearPlatform();
            }
            LastDirection = _myDirection;
            _myDirection = RotateDirectionRight();
            //�k��90��
            degree += 90f;
            Upd_UnseeCube_Data(false);
            playerMove.Upd_myFacingDirection(_myDirection, degree);
        }
    }
    void Upd_UnseeCube_Data( bool ForceRebuild)
    {
        //�S���J��ݭn�j���s���Τ����m�����p
        if (!ForceRebuild)
        {
            //�S�����( �̫�¦V��V==���a�_�l�¦V��V  && �S���`�׮t'
            if( LastDirection==_myDirection && LastDepth == GetPlayerDepth())
            {
                Debug.Log("Didnt Move");
                return; 
            }
        }
        //�R���¦�m���Τ��
        foreach(var item in UnseeList)
        {
            //����Destroy�|�v�T��즳���C������,�k��(0,0,0,)�A�R
            item.position = Vector3.zero;
            Destroy(item.gameObject);
        }
        //�ظm�s��m����P�s��List
        UnseeList.Clear();
        float New_Depth = 0f;
        New_Depth = GetPlayerDepth();//�P���a���s��m�ۦP
 
    
    }

    //�гy�s���Τ��
    
    //False���b���Τ��,True�b���Τ���W
    bool IsOnInvisiCube()
    {
        //�ˬdList���Ҧ�cube�򪱮a����m
        foreach (var item in UnseeList)
        {
            //��^���Τ����m  - ���a������m������ȡC������m�t<������, �N�N��b���Τ���W
            if (Mathf.Abs(item.position.x - playerMove.transform.position.x) < WorldUnits && (item.position.z - playerMove.transform.position.z) < WorldUnits)
            {
                //��^���Τ����m  - ���a������m������ȡC������m�t<������, �N�N��b���Τ���W,�T�O���a�b���x�W
                if (playerMove.transform.position.y - item.position.y <= WorldUnits + 0.2f && playerMove.transform.position.y - item.position.y > 0) ;
                return true;
            }                         
        }       
        return false;
    }

    //������a��(x,0,0)��(0,0,z)
     float GetPlayerDepth()
    {
        float ClosestPoint = 0f;
        // �e��
        if(_myDirection ==myDirection.Front || _myDirection == myDirection.Back)
        {
            ClosestPoint = playerMove.transform.position.z;
        }
        //���k
       else if (_myDirection == myDirection.Left || _myDirection == myDirection.Right)
        {
            ClosestPoint = playerMove.transform.position.x;
        }
        //Debug.Log("Player" +Mathf.Round(ClosestPoint));
        return Mathf.Round(ClosestPoint);
       
    }

    //���ʪ��a����񥭥x(Update�P�_�L���D�F,�ҥH�H�U�{�����ΧP�_�F)
    bool  MovePlayerToNearPlatform()
    {
        //�M���i�樫�����x
        foreach (Transform item in Platform)
        {
            //�ˬd�¦V�e��
            if(_myDirection == myDirection.Front ||_myDirection == myDirection.Back)
            {
                //������m�t < ������, �N�N��b���Τ���W,�PIsOnInvisiCube()
                if (Mathf.Abs(item.position.x - playerMove.transform.position.x) < WorldUnits + 0.1f)
                {
                    //���a�񥭥x��,�B�����@�ɳ��+0.2f,�~�|�Q�P�w�ݭn�վ��m
                    if ( (playerMove.transform.position.y - item.position.y <= WorldUnits + 0.2f && playerMove.transform.position.y - item.position.y > 0))
                    {
                        //�O���P���x�ۦP��x,y�y�Ф���,�u�������P��Z(���ʨ쥭�x��m)
                        playerMove.transform.position = new Vector3(playerMove.transform.position.x, playerMove.transform.position.y, item.position.z);
                        //������
                        return true;
                    }
                }
                //�¦V���k
                else
                {
                    if (Mathf.Abs(item.position.z - playerMove.transform.position.z) < WorldUnits + 0.1f)
                    {
                        //���a�񥭥x��,�B�����@�ɳ��+0.2f,�~�|�Q�P�w�ݭn�վ��m
                        if ((playerMove.transform.position.y - item.position.y <= WorldUnits + 0.2f && playerMove.transform.position.y - item.position.y > 0))
                        {
                            //�O���P���x�ۦP��Y,Z�y�Ф���,�u�������P��X(���ʨ쥭�x��m)
                            playerMove.transform.position = new Vector3(item.position.x, playerMove.transform.position.y, playerMove.transform.position.z);
                            //������
                            return true;
                        }
                    }
                }
            }
        }
        //�S��
        return false;
    }
   //���ʬ۾�����񥭥x
   bool MoveCamToNearPlatfoem()
    {
        bool IsCamMove = false;
        //�M���i�樫�����x
        foreach (Transform item in Platform)
        {
            //�ˬd�¦V�e��
            if (_myDirection == myDirection.Front || _myDirection == myDirection.Back)
            {
                //������m�t < ������, �N�N��b���Τ���W,�PIsOnInvisiCube()
                if (Mathf.Abs(item.position.x - playerMove.transform.position.x) < WorldUnits + 0.1f)
                {
                    //���a�񥭥x��,�B�����@�ɳ��+0.2f,�~�|�Q�P�w�ݭn�վ��m
                    if ((playerMove.transform.position.y - item.position.y <= WorldUnits + 0.2f && playerMove.transform.position.y - item.position.y > 0 && !playerMove.IsJump))
                    {
                        //���ʨ�̱���۾������x��m,���۫e��h���ʨ쭱�e��,���۫��h���ʨ�᭱��
                        if (_myDirection == myDirection.Front && item.position.z < playerMove.transform.position.z)
                            IsCamMove = true; 

                        if (_myDirection == myDirection.Back && item.position.z > playerMove.transform.position.z)
                            IsCamMove= true;

                        //�}�l���ʬ۾���m
                        if (IsCamMove)
                        {
                            //�O���P���x�ۦP��x,y�y�Ф���,�u�������P��Z(���ʨ쥭�x��m)
                            playerMove.transform.position = new Vector3(playerMove.transform.position.x, playerMove.transform.position.y, item.position.z);
                            return true;
                        }
                    }
                }
                //�¦V���k
                else
                {
                    if (Mathf.Abs(item.position.z - playerMove.transform.position.z) < WorldUnits + 0.1f)
                    {
                        //���a�񥭥x��,�B�����@�ɳ��+0.2f,�~�|�Q�P�w�ݭn�վ��m
                        if ((playerMove.transform.position.y - item.position.y <= WorldUnits + 0.2f && playerMove.transform.position.y - item.position.y > 0 && !playerMove.IsJump))
                        {
                            //���ʨ�̱���۾������x��m,���۫e��h���ʨ쭱�e��,���۫��h���ʨ�᭱��
                            if (_myDirection == myDirection.Left && item.position.x < playerMove.transform.position.x)
                                IsCamMove = true;

                            if (_myDirection == myDirection.Right && item.position.x > playerMove.transform.position.x)
                                IsCamMove = true;

                            //�}�l���ʬ۾���m
                            if (IsCamMove)
                            {
                                //�O���P���x�ۦP��x,y�y�Ф���,�u�������P��Z(���ʨ쥭�x��m)
                                playerMove.transform.position = new Vector3(item.transform.position.x, playerMove.transform.position.y, playerMove.transform.position.z);
                                return true;
                            }
                        }
                    }
                }
            }
        }
        //�S��
        return false;
        
    }

    //�k���M�w�৹����V�b����
   myDirection RotateDirectionRight()
    {
        //�j���enum��^���
        int count = (int)(_myDirection);
        count++;
        if (count > 3)
        {
            count = 0;
        }

        //�^��enum�ȨM�w��V
        //Debug.Log((myDirection)(count));
        return (myDirection)(count);
    }

    //���৹��M�w�৹����V�b����
    myDirection RotateDirectionLeft()
     {
        //�j���enum��^���
        int count = (int)(_myDirection);
        count--;
       
        if (count < 0)
        {
            count = 3;
        }
        //�^��enum�ȨM�w��V
       // Debug.Log((myDirection)(count));
        return (myDirection)(count);
       
    }

}

//Player and camera current facing direction
//��enum���Nif else,�קK����
public enum myDirection
{
    Front =0,
    Right =1,
    Back=2,
    Left =3,

}