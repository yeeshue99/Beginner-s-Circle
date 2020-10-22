using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private Player player;
    private Base playerBase;
    private List<GameObject> enemies = new List<GameObject>();
    private bool gameover = false;

    public GameObject enemyPrefab;
    public ScoreHandler scoreHandler;
    public SceneChanger sc;

    public int roundIncrementer = 0;
    public float temp = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        temp = 1f - (float)roundIncrementer / 1000;
    }

    public void setGameover(bool state)
    {
        gameover = state;
        sc.ScreenLoader("Game Over");
    }

    Vector2 GetUnitOnCircle(float angleDegrees, float radius)
    {

        // initialize calculation variables
        float _x = 0;
        float _y = 0;
        float angleRadians = 0;
        Vector2 _returnVector;

        // convert degrees to radians
        angleRadians = angleDegrees * Mathf.PI / 180.0f;

        // get the 2D dimensional coordinates
        _x = radius * Mathf.Cos(angleRadians);
        _y = radius * Mathf.Sin(angleRadians);

        // derive the 2D vector
        _returnVector = new Vector2(_x, _y);

        // return the vector info
        return _returnVector;
    }

    IEnumerator SpawnEnemies()
    {
        while (!gameover)
        {
            Vector2 randomPointOnCircle = GetUnitOnCircle(Random.Range(0.0f, 360.0f), 15.0f);
            //randomPointOnCircle *= 3;
            GameObject temp = Instantiate(enemyPrefab, new Vector3(randomPointOnCircle.x, randomPointOnCircle.y, 0f), enemyPrefab.transform.rotation);
            temp.GetComponent<Enemy>().scoreHandler = scoreHandler;
            enemies.Add(temp);
            roundIncrementer++;
            yield return new WaitForSeconds(1f - (float) roundIncrementer / 1000);
        }
    }
}
