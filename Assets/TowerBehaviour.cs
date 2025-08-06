using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{ 
    [SerializeField] private Tower towerInfo;
    [SerializeField] private BoxCollider2D attackArea;
    private float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (towerInfo != null)
            SetTowerInfo(towerInfo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTowerInfo(Tower thisTowerInfo)
    {
        towerInfo = thisTowerInfo;
        health = towerInfo.health;
        GetComponent<SpriteRenderer>().sprite = towerInfo.baseSprite;
        //var oldSize = attackArea.size;
        attackArea.size = new Vector2(towerInfo.attackRange, 1);
        attackArea.offset = new Vector2(-1 * towerInfo.attackRange, 0);
        //attackArea.transform.position = attackArea.transform.position + new Vector3(attackArea.size.x - oldSize.x, 0f, 0f);
    }

    public Tower GetTowerInfo()
    {
        return towerInfo;
    }
}
