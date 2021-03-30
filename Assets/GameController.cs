using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject pointExplosion;
    public GameObject enemyExplosion;
    public GameObject playerExplosion;
    public GameObject coinExplosion;
    public GameObject coinGobble;
    public GameObject playerPrefab;
    public GameObject canvas;
    public GameObject bulletPrefab;
    public int level;
    public int score;
    public int coins;
    public int bullets;
    public int lives;

    private Text levelTextField;
    private Text scoreTextField;
    private Text coinsTextField;
    private Text bulletsTextField;
    private Text livesTextField;
    private GameObject scoreExplosionLocation;
    private GameObject coinsExplosionLocation;
    private GameObject bulletsExplosionLocation;

    // Start is called before the first frame update
    void Start()
    {
        levelTextField = GameObject.Find("LevelTextVal").GetComponent<Text>();
        scoreTextField = GameObject.Find("ScoreTextVal").GetComponent<Text>();
        coinsTextField = GameObject.Find("CoinsTextVal").GetComponent<Text>();
        bulletsTextField = GameObject.Find("BulletsTextVal").GetComponent<Text>();
        livesTextField = GameObject.Find("LivesTextVal").GetComponent<Text>();
        scoreExplosionLocation = GameObject.Find("ScoreExplosionLocation");
        coinsExplosionLocation = GameObject.Find("CoinsExplosionLocation");
        bulletsExplosionLocation = GameObject.Find("BulletsExplosionLocation");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void addScore(int amountToAdd) 
    {
        score += amountToAdd;
        scoreTextField.text = string.Format("{0:#,0}", score);
        
    }

    private void addLives(int amountToAdd) 
    {
        lives += amountToAdd;
        livesTextField.text = string.Format("{0:#,0}", lives);
        
    }

    private void addBullets(int amountToAdd) 
    {
        bullets += amountToAdd;
        bulletsTextField.text = string.Format("{0:#,0}", bullets);
        
    }

    private void addCoins(int amountToAdd) 
    {
        coins += amountToAdd;
        coinsTextField.text = string.Format("{0:#,0}", coins);
        
    }

    private void addLevel(int amountToAdd) 
    {
        level += amountToAdd;
        bulletsTextField.text = string.Format("{0:#,0}", level);
        
    }

    public void destroyEnemy(GameObject enemy)
    {
        GameObject e = Instantiate(enemyExplosion, enemy.transform.position, Quaternion.identity);
        Destroy(enemy);
        Destroy(e, 3.5F);
        addScore(enemy.gameObject.GetComponent<Enemy>().pointVal);
        GameObject e1 = Instantiate(pointExplosion, scoreExplosionLocation.transform.position, Quaternion.identity);
        Destroy(e1, 3.0F);
    }

    public void destroyBullet(GameObject bullet)
    {
        Destroy(bullet);
    }

    public void shootBullet(Vector3 fromLocation)
    {
        if (bullets > 0) 
        {
            Instantiate(bulletPrefab, fromLocation, Quaternion.identity);
            addBullets(-1);
        }
    }

    public void destroyCoin(GameObject coin)
    {
        Destroy(coin);
        GameObject e = Instantiate(coinGobble, coin.transform.position, Quaternion.Euler(new Vector3(-90.0f, 0.0f, 0.0f)));
        Destroy(e, 3.0f);
        int coinPointVal = coin.gameObject.GetComponent<Coin>().pointVal;
        addCoins(coinPointVal);
        addBullets(coinPointVal);
        GameObject e1 = Instantiate(pointExplosion, coinsExplosionLocation.transform.position, Quaternion.identity);
        Destroy(e1, 3.0f);
        GameObject e2 = Instantiate(pointExplosion, bulletsExplosionLocation.transform.position, Quaternion.identity);
        Destroy(e2, 3.0f);
    }

    public void destroyPlayer(GameObject player)
    {
        Destroy(player);
        GameObject e = Instantiate(playerExplosion, player.gameObject.transform.position, Quaternion.identity);
        GameObject ce = Instantiate(coinExplosion, player.gameObject.transform.position, Quaternion.Euler(new Vector3(-90.0f, 0.0f, 0.0f)));
        Destroy(e, 3.0f);
        Destroy(ce, 5.0f);
        StartCoroutine(RespawnPlayer());
        addLives(-1);
    }

    private IEnumerator RespawnPlayer()
    {
        GameObject sp = GameObject.Find("PlayerSpawnPoint");
        yield return new WaitForSeconds(3);
        GameObject newPlayer = Instantiate(playerPrefab, sp.gameObject.transform.position, Quaternion.identity);
        CinemachineVirtualCamera vCam = GameObject.Find("VCam").GetComponent<CinemachineVirtualCamera>();
        vCam.LookAt = newPlayer.gameObject.transform;
        vCam.Follow = newPlayer.gameObject.transform;
    }
}
