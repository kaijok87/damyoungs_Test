using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapTest : TestBase
{
    public GameObject centerTile;           // 중앙에 사용할 타일
    public GameObject sideTile;             // 외곽에 배치될 타일
    public GameObject vertexTile;           // 꼭지점 타일

    public GameObject wall;                 // 벽


    int sizeX = 0;                          // 타일 가로 갯수
    int sizeY = 0;                          // 타일 세로 갯수

    public int tileCount = 0;               // 타일의 수

    bool isExist = false;                   // 타일 존재 여부

    Vector3 mainTileSize = Vector3.zero;    // 중앙 타일 사이즈
    Vector3 sideTileSize = Vector3.zero;    // 외곽 타일 사이즈
    Vector3 vertexTileSize = Vector3.zero;  // 꼭지점 타일 사이즈
    Vector3 wallSize = Vector3.zero;        // 벽 사이즈
    Vector3 startPos = new Vector3();       // 추후에 캐릭터 놓을 위치. 지금은 임시적으로 (0, 0, 0) 으로 설정

    GameObject[] mapTiles;                // 타일 오브젝트 객체를 담을 배열(혹시나 싶어 이중배열 외에 하나로 하는 배열을 남겨둠)
    //GameObject[,] mapTiles;                 // 타일 오브젝트 객체를 담을 이중 배열

    private void Start()
    {
        // 중앙 타일 사이즈 반환
        mainTileSize = centerTile.GetComponentInChildren<BoxCollider>().size * centerTile.transform.GetChild(0).localScale.x;
        // 외곽 타일 사이즈 반환
        sideTileSize = sideTile.GetComponentInChildren<BoxCollider>().size * sideTile.transform.GetChild(0).localScale.x;      
        // 꼭지점 타일 사이즈 반환
        vertexTileSize = vertexTile.GetComponentInChildren<BoxCollider>().size * vertexTile.transform.GetChild(0).localScale.x;
    }

    /// <summary>
    /// 타일 랜덤 생성
    /// </summary>
    protected override void Test1(InputAction.CallbackContext context)
    {
        if (!isExist)                   // 타일이 존재하지 않을 경우에만 생성
        {
            sizeX = Random.Range(10, 21);       // 타일 가로 갯수 랜덤 생성
            sizeY = Random.Range(10, 21);       // 타일 세로 갯수 랜덤 생성
            tileCount = sizeX * sizeY;          // 총 타일 갯수
            mapTiles = new GameObject[tileCount];   // 배열 동적 생성
        
        
            for (int i = 0; i < tileCount; i++)
            {
                int width = i % sizeX;
                int length = i / sizeX;

                // 사이드 타일 생성
                if ((width == 0 && length == 0) || (width == 0 && length == sizeY - 1) || (width == sizeX - 1 && length == 0) || (width == sizeX - 1 && length == sizeY - 1))
                {
                    mapTiles[i] = Instantiate(vertexTile, gameObject.transform);
                }
                else if (width == 0 || width == sizeX - 1 || length == 0 || length == sizeY - 1)              // 사이드 타일 회전
                {
                    mapTiles[i] = Instantiate(sideTile, gameObject.transform);
                    if (width == 0)                                                             // 왼쪽 세로줄
                    {
                        mapTiles[i].transform.Rotate(new Vector3(0, 90.0f, 0));
                    }
                    else if (width == sizeX - 1)                                                // 오른쪽 세로줄
                    {
                        mapTiles[i].transform.Rotate(new Vector3(0, 270.0f, 0));
                    }
                    else if (length == 0)                                                        // 맨 윗줄
                    {
                        mapTiles[i].transform.Rotate(new Vector3(0, 180.0f, 0));
                    }
                    //else if (j == sizeY - 1)                                              // 맨 아랫줄
                    //{
                    //    mapTiles[i, j].transform.Rotate(new Vector3(0, 360.0f, 0));
                    //}
                }
                else
                {
                    mapTiles[i] = Instantiate(centerTile, gameObject.transform);                         // 중앙 타일 생성
                    mapTiles[i].transform.Rotate(new Vector3(0, 90.0f * Random.Range(0, 4), 0));         // 중앙 타일 랜덤 회전(그냥 미관상)
                }

                // 타일 위치 이동. startPos는 임시로 넣어놓은 값(0, 0, 0)
                mapTiles[i].transform.position = new Vector3(startPos.x - mainTileSize.x * sizeX / 2 + mainTileSize.x * width,
                                                            0, startPos.z + mainTileSize.z * sizeY - mainTileSize.z * length);
            }
        
            isExist = true;
        }

        //if (!isExist)                   // 타일이 존재하지 않을 경우에만 생성
        //{
        //    sizeX = Random.Range(10, 21);       // 타일 가로 갯수 랜덤 생성
        //    sizeY = Random.Range(10, 21);       // 타일 세로 갯수 랜덤 생성
        //    tileCount = sizeX * sizeY;          // 총 타일 갯수
        //    mapTiles = new GameObject[sizeX, sizeY];   // 배열 동적 생성


        //    for (int j = 0; j < sizeY; j++)                             // 타일 생성 i = 가로 방향으로 증가, j = 세로 방향으로 증가
        //    {
        //        for (int i = 0; i < sizeX; i++)
        //        {
        //            // 사이드 타일 생성
        //            if ((i == 0 && j == 0) || (i == 0 && j == sizeY - 1) || (i == sizeX - 1 && j == 0) || (i == sizeX - 1 && j == sizeY - 1))
        //            {
        //                mapTiles[i, j] = Instantiate(vertexTile);
        //            }
        //            else if (i == 0 || i == sizeX - 1 || j == 0 || j == sizeY - 1)              // 사이드 타일 회전
        //            {
        //                mapTiles[i, j] = Instantiate(sideTile);
        //                if (i == 0)                                                             // 왼쪽 세로줄
        //                {
        //                    mapTiles[i, j].transform.Rotate(new Vector3(0, 90.0f, 0));
        //                }
        //                else if (i == sizeX - 1)                                                // 오른쪽 세로줄
        //                {
        //                    mapTiles[i, j].transform.Rotate(new Vector3(0, 270.0f, 0));
        //                }
        //                else if (j == 0)                                                        // 맨 윗줄
        //                {
        //                    mapTiles[i, j].transform.Rotate(new Vector3(0, 180.0f, 0));
        //                }
        //                //else if (j == sizeY - 1)                                              // 맨 아랫줄
        //                //{
        //                //    mapTiles[i, j].transform.Rotate(new Vector3(0, 360.0f, 0));
        //                //}
        //            }
        //            else
        //            {
        //                mapTiles[i, j] = Instantiate(centerTile);                                               // 중앙 타일 생성
        //                mapTiles[i, j].transform.Rotate(new Vector3(0, 90.0f * Random.Range(0, 4), 0));         // 중앙 타일 랜덤 회전(그냥 미관상)
        //            }
        //            // 타일 위치 이동. startPos는 임시로 넣어놓은 값(0, 0, 0)
        //            mapTiles[i, j].transform.position = new Vector3(startPos.x - mainTileSize.x * sizeX / 2 + mainTileSize.x * i,
        //                                                            0, startPos.z + mainTileSize.z * sizeY - mainTileSize.z * j);
        //        }
        //    }

        //    isExist = true;             // 타일 생성 여부(타일이 생성되어 있다)
        //}
    }

    /// <summary>
    /// 타일 제거
    /// </summary>
    protected override void Test2(InputAction.CallbackContext context)
    {
        for (int i = 0; i < tileCount; i++)
        {
            Destroy(mapTiles[i]);
        }

        //for (int i = 0; i < sizeX; i++)
        //{
        //    for(int j = 0;j < sizeY; j++)
        //    {
        //        Destroy(mapTiles[i, j]);
        //    }
        //}
        isExist = false;
    }
}
