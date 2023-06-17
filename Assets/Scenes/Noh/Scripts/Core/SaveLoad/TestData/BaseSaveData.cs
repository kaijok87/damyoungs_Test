using System;
using System.Collections.Generic;
using UnityEngine;
/*
 JsonUtility.ToJson  다중배열 지원안됨 . 
 
 테스트용 코드 이파일에 대해서는 참고만 하시면됩니다.
 
 
 
 */
/// <summary>
/// 테스트 클래스 참고용
/// </summary>
[Serializable]
public struct dumyData
{
    public string[][] charcterName;//다중배열 저장안됨
    public int age;
    public float height;
    public double weight;
    public short min;
    public long money;
    public ulong score;
    public string[] toStirngs;
    public ulong[] ulongs;
    public long[][] longs;//저장안됨

    public dumyData(
        string[][] charcterName ,
        int age                 ,
        float height            ,
        double weight           , 
        short min               ,
        long money              ,
        ulong score             ,
        string[] toStirngs      ,
        ulong[] ulongs          ,
        long[][] longs          
        ) { 
        this.charcterName   = charcterName;
        this.toStirngs      = toStirngs;
        this.age            = age;
        this.height         = height;
        this.weight         = weight;
        this.min            = min;
        this.money          = money;
        this.score          = score;
        this.longs          = longs;
        this.ulongs         = ulongs;
    }

}
[Serializable]
public struct stringArray
{
    public string[] values;
    public stringArray(string[] values) { 
        this.values = values;
    }
}
[Serializable]
public struct stringDoubleArray {
    public stringArray[] values;
    public stringArray[] Values { 
        get => values;
        set => values = value;
    }
    public stringDoubleArray(stringArray[] values) {
        this.values = values;
    }
}
[Serializable]
public struct stringTripleArray
{
    public stringDoubleArray[] values;
    public stringTripleArray(stringDoubleArray[] values) {
        this.values = values;
    }
}

[Serializable]
public class BaseSaveData<T> : JsonGameData 
{
    public BaseSaveData() { }   
    [SerializeField]stringTripleArray stringTripleArray = new stringTripleArray();
    [SerializeField]dumyData dumyData = new dumyData();
    [SerializeField]dumyData dumyEmpty;
    [SerializeField]Vector3 vector3Test = new Vector3(55.5f, 22.3f, 44.5f);
    [SerializeField]Vector2 vector2Test = new Vector2(12.4f, 11.3f);
    [SerializeField]int[] invenIndex = { 75444545, 12213234, 1234556123, 234234211, 352112345, 1262343673, 1231215161 };
    [SerializeField]List<T> test = new List<T>();
    public List<T> Test {
        get => test;
        set => test = value;
    }
    [SerializeField]List<T> test1;
    [SerializeField]int[][] test3 = new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } }; ///다중배열은 안된다.
    [SerializeField]string[] tempString = {"이","게","저","장","되","네" };
    [SerializeField]stringArray[] tempArray = new stringArray[3];
    [SerializeField]stringDoubleArray[] tempDoubleArray = new stringDoubleArray[3];
    public void TestFunc() {

        //다중배열 처리 방식 
        for (int i=0; i< tempArray.Length; i++) {
            tempArray[i].values = tempString;
        }
        for (int i = 0; i < tempDoubleArray.Length; i++)
        {
            tempDoubleArray[i].Values = tempArray;
        }
        stringTripleArray.values = tempDoubleArray;

        Money = 99955555999;
        //foreach는 읽기 전용으로 만들어져있어서 value 변수는 기본적으로 readonly 값을 가진다.
        //foreach (stringArray value in tempArray) { 
        //    value.values = tempString;
        //}

    }
}
