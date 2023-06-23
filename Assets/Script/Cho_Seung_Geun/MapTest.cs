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
    public int wallCount = 0;

    bool isExist = false;                   // 타일 존재 여부

    Vector3 mainTileSize = Vector3.zero;    // 중앙 타일 사이즈
    Vector3 sideTileSize = Vector3.zero;    // 사이드 타일 사이즈
    Vector3 vertexTileSize = Vector3.zero;  // 꼭지점 타일 사이즈

    Vector3 startPos = new Vector3();       // 추후에 캐릭터 놓을 위치. 지금은 임시적으로 (0, 0, 0) 으로 설정

    GameObject[] mapTiles;                // 타일 오브젝트 객체를 담을 배열
    List<GameObject> walls;
    //GameObject[] walls;


    private void Start()
    {
        // 중앙 타일 사이즈 반환
        mainTileSize = centerTile.GetComponentInChildren<BoxCollider>().size * centerTile.transform.GetChild(0).localScale.x;
        // 사이드 타일 사이즈 반환
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

            wallCount = 2 * sizeX + 2 * sizeY - 4;
            //walls = new GameObject[wallCount];      // 벽 타일 갯수 생성
            walls = new List<GameObject>(wallCount);

            GameObject wallObject;

            for (int i = 0; i < tileCount; i++)
            {
                int width = i % sizeX;              // 가로 인덱스 번호
                int length = i / sizeX;             // 세로 인덱스 번호

                // 타일 생성
                if ((width == 0 && length == 0) || (width == 0 && length == sizeY - 1) || (width == sizeX - 1 && length == 0) || (width == sizeX - 1 && length == sizeY - 1))
                {


                    mapTiles[i] = Instantiate(vertexTile, gameObject.transform);            // 꼭지점 타일 생성
                    mapTiles[i].GetComponent<Tile>().Type = (int)TileType.vertexTile;       // 타일 스크립트에 타입 저장
                    wallObject = Instantiate(wall, mapTiles[i].transform);
                    wallObject.transform.Translate(new Vector3(1.0f, 0.0f, -1.7f));
                    //walls.Add(wallObject);
                    wallObject = Instantiate(wall, mapTiles[i].transform);
                    wallObject.transform.Translate(new Vector3(1.7f, 0.0f, 1.0f));
                    //walls.Add(wallObject);
                    if (width == 0 && length == 0)
                    {
                        mapTiles[i].transform.Rotate(new Vector3(0, 180.0f, 0));
                    }
                    else if (width == 0 && length == sizeY - 1)
                    {
                        mapTiles[i].transform.Rotate(new Vector3(0, 270.0f, 0));
                    }
                    else if (width == sizeX - 1 && length == 0)
                    {
                        mapTiles[i].transform.Rotate(new Vector3(0, 90.0f, 0));
                    }
                    else if (width == sizeX - 1 && length == sizeY - 1)
                    {
                        mapTiles[i].transform.Rotate(new Vector3(0, 360.0f, 0));
                    }
                }
                else if (width == 0 || width == sizeX - 1 || length == 0 || length == sizeY - 1)              // 사이드 타일 회전
                {
                    mapTiles[i] = Instantiate(sideTile, gameObject.transform);              // 사이드 타일 생성
                    mapTiles[i].GetComponent<Tile>().Type = (int)TileType.sideTile;         // 타일 스크립트에 타입 저장
                    wallObject = Instantiate(wall, mapTiles[i].transform);
                    wallObject.transform.Translate(new Vector3(1, 0.0f, -1.7f));
                    //walls.Add(wallObject);

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
                    mapTiles[i] = Instantiate(centerTile, gameObject.transform);                        // 중앙 타일 생성
                    mapTiles[i].transform.Rotate(new Vector3(0, 90.0f * Random.Range(0, 4), 0));        // 중앙 타일 랜덤 회전(그냥 미관상)
                    mapTiles[i].GetComponent<Tile>().Type = (int)TileType.centerTile;                   // 타일 스크립트에 타입 저장
                }

                // 타일 위치 이동. startPos는 임시로 넣어놓은 값(0, 0, 0)
                mapTiles[i].transform.position = new Vector3(startPos.x - mainTileSize.x * sizeX / 2 + mainTileSize.x * width,
                                                            0, startPos.z + mainTileSize.z * sizeY - mainTileSize.z * length);
            }
        
            //for (int i = 0; i < wallCount; i++)
            //{
            //    wallObject = Instantiate(wall);
            //    wallObject.transform.position = new Vector3(mapTiles[0].transform.position.x + 1.0f,
            //                                                mapTiles[0].transform.position.y,
            //                                                mapTiles[0].transform.position.z + 1.7f);
            //    //walls.Add(wallObject);
            //}

            isExist = true;
        }

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


        isExist = false;
    }
}
