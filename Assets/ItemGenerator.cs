using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour
{
    //Prefabの入れ物を作る
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;
    //スタート地点
    private int startPos = -160;
    //ゴール地点
    private int goalPos = 120;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;


    //課題仮説１：Unityちゃんの位置で消す。
    //課題仮説４：配列にしてみる。
    private GameObject unitychanPos;
    private GameObject[] cars;
    private GameObject[] tCones;
    private GameObject[] coins;
    //課題仮説5：Update内で比較するときの変数を用意。
    private float difCars;
    private float difCones;
    private float difCoins;

    // Use this for initialization
    void Start()
    {


        /*没。Start()の瞬間にCars、tCones,coinsの中身がHierarchyにいない。「空っぽ」が入って、以後そのまま。         
        //課題仮説１
        unitychanPos = GameObject.Find("unitychan");
        cars = GameObject.FindGameObjectWithTag("CarTag");
        tCones = GameObject.FindGameObjectWithTag("TrafficCone");
        coins = GameObject.FindGameObjectWithTag("CoinTag");
        */


        //一定の距離ごとにアイテムを生成
        for (int i = startPos; i < goalPos; i += 15)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(0, 10);
            if (num <= 1)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);

                    /*失敗：Start関数の中に入れてどうする。
                    //課題仮説：coneがunityちゃんと並んだらDestroyする。
                    if (cone.transform.position.z == this.unityChanPos.transform.position.z)
                    {
                        Destroy(cone);

                    }
                    */
                }
            }
            else
            {

                //レーンごとにアイテムを生成
                for (int j = -1; j < 2; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くz座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //50%コイン配置：30％車配置：10％何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);

                    }

                    //課題仮説3：Updateの中のcarsには、常にできてたのcarしか入ってないのでは？：
                    //          なので、車1つだけ、作る、二つだけ作る、を試す。

                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                    }
                }
            }
            //課題仮説3:失敗。なんか動きはあった気がしたが、消えず。
            /*
                GameObject car = Instantiate(carPrefab) as GameObject;
                car.transform.position = new Vector3(0, car.transform.position.y, 0);
            */
        }
    }

    // Update is called once per frame
    void Update()
    {

        //課題仮説２：負担無視してStart（）のものをここに入れちゃう。
        //課題仮説４：FindGameObject"s"WithTagでいく。
        unitychanPos = GameObject.Find("unitychan");
        cars = GameObject.FindGameObjectsWithTag("CarTag");
        tCones = GameObject.FindGameObjectsWithTag("TrafficConeTag");
        coins = GameObject.FindGameObjectsWithTag("CoinTag");

        //仮説2が失敗したので、carsの中身を見てみるコードを打つ。
        //結果、ちゃんとCarPrefab(Clone)は入っていた。
        //結論から言うと、FindGameObjectWithTagでは、変数に一つのオブジェクトしか入っておらず、
        //新しいCloneとどんどん入れ替わっていた。よって、配列型のFindGameObject"s"WithTagで次を試す
        /*
        if (cars != null)
        {
            Debug.Log(cars.name);
        }
        else
        {
            Debug.Log("nullだよ");
        }
        */

        //課題仮説１：タグで検索して消す。
        //ItemGeneratorは親オブジェクトとしてシーン内にcloneを作ってる。それぞれはきちんとタグを継承している。
        //課題仮説４：cars配列に格納したPrefabどもを一掃
        //↑あかんやった。原因は、浮動小数点同士は誤差があるから等号でつないではいけないこと。
        //仮説5：なので、十分小さな数イプシロンより小さい、ということで真をとる。

        for (int i = 0; i < cars.Length; i++)
        {
            difCars = this.cars[i].transform.position.z - unitychanPos.transform.position.z+10;
            Mathf.Abs(difCars);
            if (difCars <= 0.005)
            {
                Destroy(cars[i]);
            }
        }

        for (int i = 0; i < tCones.Length; i++)
        {
                difCones = this.tCones[i].transform.position.z - unitychanPos.transform.position.z+10;
                Mathf.Abs(difCones);
                if (difCones <= 0.005)
                    {
                        Destroy(tCones[i]);
                    }
            Debug.Log("cones");
        }

        for (int i = 0; i < coins.Length; i++)
        {
              difCones = this.coins[i].transform.position.z - unitychanPos.transform.position.z+10;
              Mathf.Abs(difCoins);
              if (difCones <= 0.005)
                    {
                        Destroy(coins[i]);
                    }
        }
     }
}

