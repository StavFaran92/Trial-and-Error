using DG.Tweening;
using UnityEngine;

public class PageController : MonoBehaviour
{
    private static string TAG = "PageController";

    private const float mPagePeakValueY = 0.8f;
    private const float mPageStartValueY = 0.74f;
    private const float mPageTweenVerDuration = .3f;
    private const float mPageTweenHorDuration = .7f;
    [SerializeField]
    private Transform[] mPages;

    private int mCurrentPage = 0;
    private int mNumOfPages;

    // Start is called before the first frame update
    void Start()
    {
        Debug.unityLogger.Log(TAG, "Start()");

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
        Debug.unityLogger.Log(TAG, "SwipeToNextPage()");

        if (mCurrentPage < mNumOfPages - 1)
        {
            FocusOnPage();
            SwipePageAnimation(mCurrentPage, -0.142f);

            mCurrentPage++;

            Debug.Log("Current page is: " + mCurrentPage);
        }
    }

    private void SwipeToPreviousPage()
    {
        Debug.unityLogger.Log(TAG, "SwipeToPreviousPage()");

        if (mCurrentPage > 0)
        {
            mCurrentPage--;
            FocusOnPage();
            SwipePageAnimation(mCurrentPage, 0.1429f);


            Debug.Log("Current page is: " + mCurrentPage);
        }
    }

    private void FocusOnPage()
    {
        Debug.unityLogger.Log(TAG, "FocusOnPage()");

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

    private void SwipePageAnimation(int pageIndex, float endValueX)
    {
        Debug.unityLogger.Log(TAG, "SwipePageAnimation()");

        mPages[pageIndex].DOMoveY(mPagePeakValueY, mPageTweenVerDuration)
            .OnComplete(()=>mPages[pageIndex].DOMoveX(endValueX, mPageTweenHorDuration)
            .OnComplete(() => mPages[pageIndex].DOMoveY(mPageStartValueY, mPageTweenVerDuration)));
        ;
    }
}
