using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; } //свойство для обращение к объекту

    public Transform knifeSpawner; //точка спавна ноожей;
    public GameObject knifePrefab; //префаб ножа;
    public GameUI gameUI; //ссылка на класс GameUI;
    public GameObject wood;
    public List<Rigidbody2D> knifes;
    public List<GameObject> woods;

    public Vector2 direction;
    [SerializeField]
    private int knifeCount; //счётчик, для подсчета ножей для прохождения уровня.

    private void Awake()
    {
        Instance = this;
        gameUI = GetComponent<GameUI>();        
    }

    private void Start()
    {
        woods = new List<GameObject>();
        for (int i = 0; i < wood.transform.childCount; i++)
        {
            woods.Add(wood.transform.GetChild(i).gameObject);
        }
        knifes = new List<Rigidbody2D>();
        gameUI.CreateKnifesUI(knifeCount);
        Instantiate(knifePrefab, knifeSpawner.position, Quaternion.identity);
    }

    /// <summary>
    /// Логика, если попал в дерево...
    /// </summary>
    public void SuccessSpawn()
    {
        if (knifeCount > 1)
        {
            SpawnKnife();
        }
        else
        {
            Result(true);
        }
    }

    private void SpawnKnife()
    {
        knifeCount--;        
        Instantiate(knifePrefab, knifeSpawner.position, Quaternion.identity);
    }

    /// <summary>
    /// Логика, результата
    /// </summary>
    /// <param name="win">"win" отвечает за победу или проигрыш</param>
    public void Result(bool win)
    {
        StartCoroutine("ResultCoroutine", win);
    }

    private IEnumerator ResultCoroutine(bool win)
    {
        if (win)
        {
            Vibration.Vibrate(350); //вибро при победе
            WoodDestroy();
            KnifesDestroy();
            yield return new WaitForSecondsRealtime(0.3f);
            Debug.Log("You Won!");
            
            gameUI.restartButton.SetActive(true);            
        }
        else
        {
            
            KnifesDestroy();
            gameUI.restartButton.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// При победе, разрушает объекты;
    /// </summary>
    public void KnifesDestroy()
    {
        foreach (var item in knifes)
        {
            item.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void WoodDestroy()
    {
        foreach (var item in woods)
        {
            item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            item.GetComponent<PolygonCollider2D>().enabled = true;
            item.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
        }
    }

    
}
