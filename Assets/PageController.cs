using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{

    [SerializeField]
    private Transform[] mPages;

    private int mCurrentPage = 0;
    private int mNumOfPages;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        mNumOfPages = mPages.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.RightArrow))
        {

            SwipeToNextPage();

            
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {

            SwipeToPreviousPage();


        }
    }

    private void SwipeToNextPage()
    {
        if (mCurrentPage < mNumOfPages - 1)
        {
            FocusOnPage();

            mPages[mCurrentPage].DOMoveX(-0.142f, 1);

            mCurrentPage++;

            Debug.Log("Current page is: " + mCurrentPage);
        }
    }

    private void SwipeToPreviousPage()
    {
        if (mCurrentPage > 0)
        {
            mCurrentPage--;
            FocusOnPage();
            mPages[mCurrentPage].DOMoveX(0.1429f, 1);


            Debug.Log("Current page is: " + mCurrentPage);
        }
    }

    private void FocusOnPage()
    {
        if (mCurrentPage > 0)
        {
            mPages[mCurrentPage - 1].GetComponentInChildren<Canvas>().sortingOrder = 1;

            if (mCurrentPage > 1)
                mPages[mCurrentPage - 2].GetComponentInChildren<Canvas>().sortingOrder = 0;
        }

        if (mCurrentPage < mNumOfPages - 1)
        {
            mPages[mCurrentPage + 1].GetComponentInChildren<Canvas>().sortingOrder = 1;

            if (mCurrentPage < mNumOfPages - 2)
                mPages[mCurrentPage + 2].GetComponentInChildren<Canvas>().sortingOrder = 0;
        }

        mPages[mCurrentPage].GetComponentInChildren<Canvas>().sortingOrder = 2;
    }
}
