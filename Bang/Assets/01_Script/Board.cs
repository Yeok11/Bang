using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private HandCard currentCard;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Card"))
        {
            currentCard = collision.GetComponent<HandCard>();
            currentCard.inBoard = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Card"))
        {
            currentCard.inBoard = false;
        }
            
    }
}
