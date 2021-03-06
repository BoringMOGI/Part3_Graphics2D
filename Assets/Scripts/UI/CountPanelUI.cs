using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountPanelUI : MonoBehaviour, IMobileInput
{
    // 선택한 수량과 결정,취소 여부.
    public delegate void CountEvent(int count, bool isSubmit);
    CountEvent Callback;

    [SerializeField] Text countText;
    [SerializeField] Text totalPriceText;
    [SerializeField] Animation totalPriceAnim;

    const int MIN_COUNT = 1;
    const int MAX_COUNT = 99;
    int count = 1;

    ItemData item;

    public void Open(ItemData item, CountEvent Callback)
    {
        this.item = item;
        this.Callback = Callback;

        InputManager.Instance.RegestedEventer(this);

        count = 1;
        countText.text = count.ToString();

        gameObject.SetActive(true);
        UpdateTotalPrice();
    }
    private void Close()
    {
        InputManager.Instance.ReleaseEventer();
        gameObject.SetActive(false);
    }

    public void InputVector(VECTOR v, bool isDown)
    {
        if (isDown)
            return;

        if(v == VECTOR.Left)
        {
            count -= 1;
        }
        else if(v == VECTOR.Right)
        {
            count += 1;
        }
        else if(v == VECTOR.Up)
        {
            count += 10;
        }
        else if(v == VECTOR.Down)
        {
            count -= 10;
        }

        count = Mathf.Clamp(count, MIN_COUNT, MAX_COUNT);
        countText.text = count.ToString();
        UpdateTotalPrice();
    }
    public void Submit()
    {
        int totalPrice = item.itemPrice * count;
        bool isEnough = Inventory.Instance.IsEnoughCoin(totalPrice);

        if (isEnough)
        {
            Callback(count, true);          // 결과 값 전달.
            Close();
        }
        else
        {
            totalPriceAnim.Play("Blink");
        }
    }
    public void Cancel()
    {        
        Callback(-1, false);            // 결과 값 전달.
        Close();
    }

    private void UpdateTotalPrice()
    {
        int totalPrice = item.itemPrice * count;
        totalPriceText.text = string.Format("{0:#,##0}", totalPrice);

        bool isEnough = Inventory.Instance.IsEnoughCoin(totalPrice);
        totalPriceText.color = isEnough ? Color.white : Color.red;
    }

}
