using AC;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    public float mPageHeightOffset = 0.72f;

    private const float mPagePeakHeight = 0.8f;
    private const float mPageTweenVerDuration = .3f;
    private const float mPageTweenHorDuration = .7f;
    private const float pageSideOffset = 0.142f;
    [SerializeField] private Transform[] mPages;


    private int mCurrentPage = 0;

    void Start()
    {
        SetupInitialPagesHeights();
    }

    private void SetupInitialPagesHeights()
    {
        for (int pageIndex = 0; pageIndex < mPages.Length; pageIndex++)
        {
            var position = mPages[pageIndex].position;
            mPages[pageIndex].position = new Vector3(position.x, PageHeight(pageIndex, false), position.z);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwipeToNextPage();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwipeToPreviousPage();
        }
    }

    private void SwipeToNextPage()
    {
        if (mCurrentPage < mPages.Length - 1)
        {
            SwipePageAnimation(mCurrentPage, true);
            mCurrentPage++;
        }

        SetPagesHotspot();
    }


    private void SwipeToPreviousPage()
    {
        if (mCurrentPage > 0)
        {
            mCurrentPage--;
            SwipePageAnimation(mCurrentPage, false);
        }

        SetPagesHotspot();
    }

    private void SetPagesHotspot()
    {
        //reset all
        for(int i=0; i<mPages.Length; i++)
        {
            SetHotspotIfNotAbsent(i, false);
        }

        //set necessary ones
        SetHotspotIfNotAbsent(mCurrentPage, true);

        if (mCurrentPage > 0)
        {
            SetHotspotIfNotAbsent(mCurrentPage - 1, true);
        }
    }

    /// <summary>
    /// This is a bit wastefull but since it not supposed to be called many times We can handle it
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="value"></param>
    private void SetHotspotIfNotAbsent(int pageIndex, bool value)
    {
        //I would have preffered to use the Hotspot component, yet AC's manual activation of hotspot is a bit problematic 
        //So this is a small workaround.
        var hotspot = mPages[pageIndex].GetComponentInChildren<Hotspot>(true);

        if (hotspot != null)
        {
            var gameobject = hotspot.gameObject;

            if (value)
            {
                gameobject.SetActive(true);
            }
            else
            {
                gameobject.SetActive(false);
            }
        }
    }

    private void SwipePageAnimation(int pageIndex, bool isMovingToLeftSide)
    {
        mPages[pageIndex].DOMoveY(mPagePeakHeight, mPageTweenVerDuration)
            .OnComplete(()=>mPages[pageIndex].DOMoveX(pageSideOffset * (isMovingToLeftSide ? -1 : 1), mPageTweenHorDuration)
            .OnComplete(() => mPages[pageIndex].DOMoveY(PageHeight(pageIndex, isMovingToLeftSide), mPageTweenVerDuration)));
        ;
    }

    // Setup page height relative to its index.
    private float PageHeight(int pageIndex, bool isMovingToLeftSide)
    {
        var pageOffset = pageIndex / 200.0f;
        return mPageHeightOffset + (pageOffset * (isMovingToLeftSide ? 1 : -1));
    }
}
