using UnityEngine;
using System.Collections;


public class ItemGenerator2 : MonoBehaviour {
        //carPrefabを入れる
        public GameObject carPrefab;
        //coinPrefabを入れる
        public GameObject coinPrefab;
        //cornPrefabを入れる
        public GameObject conePrefab;
        //スタート地点
        private int startPos = -160;
        //ゴール地点
        private int goalPos = 120;
        //アイテムを出すx方向の範囲
        private float posRange = 3.4f;

    //Unityちゃんのオブジェクト
    GameObject unitychan;
    GameObject cone;
    GameObject car;
    GameObject coin;

    //アイテムが出る地点
    float itemPos;

        // Use this for initialization
    void Start () {

        //Unityちゃんオブジェクト取得
        unitychan = GameObject.Find("unitychan");
        //Unityちゃんの正面40mの地点を取得
        itemPos = 40 + unitychan.transform.position.z;

        //なぜか、最初しかオブジェクト現れない。Uptodateの負担が重過ぎるのか？
        cone = Instantiate(conePrefab) as GameObject;
        coin = Instantiate(coinPrefab) as GameObject;
        car = Instantiate(carPrefab) as GameObject;



    }

    // Update is called once per frame
    void Update () {

        //ユニティちゃんの40m先にアイテムを生成
        if (itemPos<=120)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(0, 200);
            if (num <= 1)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, itemPos);
                }
                Debug.Log("cone");
            }
            else if(num <= 10)
            {

                //レーンごとにアイテムを生成
                for (int j = -1; j < 2; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //50%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, itemPos + offsetZ);
                        Debug.Log("coin");
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, itemPos + offsetZ);
                        Debug.Log("car");
                    }
                }
            }
        }

    }
}
