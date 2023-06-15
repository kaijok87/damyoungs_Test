using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapTest : TestBase
{
    /// <summary>
    /// 주로 사용하는 타일
    /// </summary>
    public GameObject mainTile;
    int sizeX = 0;                          // 타일 가로 갯수
    int sizeY = 0;                          // 타일 새로 갯수

    public int tileCount = 0;               // 타일의 수

    bool isExist = false;                   // 타일 존재 여부

    Vector3 mainTileSize = Vector3.zero;    // 타일 사이즈
    Vector3 startPos = new Vector3();       // 추후에 캐릭터 놓을 위치. 지금은 임시적으로 (0, 0, 0) 으로 설정

    GameObject[] mapTiles;                  // 타일 오브젝트 객체를 담을 배열

    private void Start()
    {
        mainTileSize = mainTile.gameObject.GetComponent<BoxCollider>().size * mainTile.transform.localScale.x;  // 타일 사이즈 반환
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
                int sizex = i % sizeX;
                int sizey = i / sizeX;
                mapTiles[i] = Instantiate(mainTile);

                // 타일 위치 이동. startPos는 임시로 넣어놓은 값(0, 0, 0)
                mapTiles[i].transform.position = new Vector3(startPos.x - mainTileSize.x * sizeX / 2 + mainTileSize.x * sizex,
                                                            0, startPos.z + mainTileSize.z * sizeY - mainTileSize.z * sizey);
            }

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
