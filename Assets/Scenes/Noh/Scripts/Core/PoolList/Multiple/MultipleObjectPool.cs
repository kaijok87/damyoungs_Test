using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기본적으로 여러개 생산되는 오브젝트일경우 
/// 해당클래스를 상속받아서 사용 
/// </summary>
/// <typeparam name="T">ObjectIsPool을 상속받은 클래스</typeparam>
public class MultipleObjectPool<T> : MonoBehaviour where T : ObjectIsPool
{
    /// <summary>
    /// 풀에 담아 놓을 오브젝트의 프리팹
    /// </summary>
    public GameObject origianlPrefab;

    /// <summary>
    /// 풀의 크기. 처음에 생성하는 오브젝트의 갯수. 갯수는 2^n으로 잡는 것이 좋다.
    /// </summary>
    public int poolSize = 8;

    /// <summary>
    /// 풀이 생성한 모든 오브젝트가 들어있는 배열
    /// </summary>
    T[] pool;

    /// <summary>
    /// 사용가능한(비활성화되어있는) 오브젝트들이 들어있는 큐
    /// </summary>
    Queue<T> readyQueue;


    /// <summary>
    /// 오브젝트 생성위치 변경할 변수 추가 
    /// </summary>
   protected Transform setPosition;
   
    
    /// <summary>
    /// 객체 생성 전에 처리해야할 내용 추가하기위해 함수하나 추가로작성하였다.
    /// </summary>
    protected virtual void SettingFuntion() { 
        
    }
    public void Initialize()
    {
        SettingFuntion();  //객체 생성되기전에 처리할 내용이 있는경우를 위해 추가
        if (setPosition == null) {//오브젝트 생성위치 변경하는 로직을위해 조건문추가
            setPosition = transform;
        }

        
        if (pool == null)
        {
            pool = new T[poolSize];                 // 풀 전체 크기로 배열 할당
            readyQueue = new Queue<T>(poolSize);    // 레디큐 생성(capacity는 poolSize로 지정)

            //readyQueue.Count;       // 실제로 들어있는 갯수
            //readyQueue.Capatity;    // 현재 미리 준비해 놓은 갯수

            GenerateObjects(0, poolSize, pool);
        }
        else
        {
            // 두번째 씬이 불려져서 이미 풀은 만들어져 있는 상황
            foreach (T obj in pool)
            {
                obj.gameObject.SetActive(false);    // 전부 비활성화
            }
        }

    }

    /// <summary>
    /// 풀에서 오브젝트를 하나 꺼낸 후 돌려주는 함수
    /// </summary>
    /// <returns>레디큐에서 꺼내고 활성화시킨 오브젝트</returns>
    public T GetObject()
    {
        if (readyQueue.Count > 0)    // 레디큐에 남아있는 오브젝트가 있는지 확인
        {
            // 남아있으면
            T comp = readyQueue.Dequeue();      // 하나 꺼내고
            comp.gameObject.SetActive(true);    // 활성화시킨 다음에 
            return comp;                        // 꺼낸 것 리턴
        }
        else
        {
            // 남은 오브젝트가 없으면
            ExpandPool();           // 풀 확장시키고
            return GetObject();     // 다시 요청
        }
    }

    /// <summary>
    /// 풀을 두배로 확장시키는 함수
    /// </summary>
    private void ExpandPool()
    {
        Debug.LogWarning($"{gameObject.name} 풀 사이즈 증가. {poolSize} -> {poolSize * 2}");

        int newSize = poolSize * 2;     // 새로운 크기 구하기
        T[] newPool = new T[newSize];   // 새로운 크기만큼 새 배열 만들기
        for (int i = 0; i < poolSize; i++)     // 이전 배열에 있던 것을 새 배열에 복사
        {
            newPool[i] = pool[i];
        }

        GenerateObjects(poolSize, newSize, newPool);    // 새 배열에 남은 부분에 오브젝트 새엉해서 설정
        pool = newPool;     // 새 배열을 pool로 설정
        poolSize = newSize; // 새 크기를 크기로 설정
    }

    /// <summary>
    /// 오브젝트 생성해서 배열에 추가해주는 함수
    /// </summary>
    /// <param name="start">배열의 시작 인덱스</param>
    /// <param name="end">배열의 마지막 인덱스-1</param>
    /// <param name="newArray">생성된 오브젝트가 들어갈 배열</param>
    private void GenerateObjects(int start, int end, T[] newArray)
    {
        for (int i = start; i < end; i++)    // 새로 만들어진 크기만큼 반복
        {
            //특정위치에 생성하기 기본적으로는 풀아래에 있다.
            GameObject obj = Instantiate(origianlPrefab, setPosition);
            obj.name = $"{origianlPrefab.name}_{i}";            // 이름 구분되도록 설정

            T comp = obj.GetComponent<T>();                     // PooledObject 컴포넌트 받아와서
            comp.onDisable += () => readyQueue.Enqueue(comp);   // PooledObject가 disable될 때 래디큐로 되돌리기

            newArray[i] = comp;     // 배열에 저장
            obj.SetActive(false);   // 생성한 게임 오브젝트 비활성화(=>비활성화 되면서 레디큐에도 추가된다)
        }
    }


}