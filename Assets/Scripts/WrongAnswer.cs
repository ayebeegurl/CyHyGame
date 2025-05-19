using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongAnswer : MonoBehaviour, IcardDropArea
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCardDrop(DragAndDrop card)
    {
        card.transform.position = transform.position;
        card.StartCoroutine(DestroyAfterDelay(card.gameObject, 0.5f));
        GameManager.Instance.ShowWrong();
    }
    private System.Collections.IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
