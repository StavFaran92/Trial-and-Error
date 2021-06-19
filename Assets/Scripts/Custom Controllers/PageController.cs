using AC;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(HorizontalSwipeGestureRecognizer))]
public class PageController : MonoBehaviour
{
    private const float PageSideOffset = 0.142f;

    [SerializeField] private float pageHeightOffset = 0.76f;
    [SerializeField] private float PagePeakHeight = 0.8f;
    [SerializeField] private float pageTweenVerticalDuration = 0.3f;
    [SerializeField] private float pageTweenHorizontalDuration = 0.7f;
    [SerializeField] private Transform[] mPages;
    [SerializeField] private string initialPageIndexVarLabel;

    private GVar initialPageOnStart;
    private int mCurrentPage = 0;
    private bool isPageSwappingEnabled = true;
    private AudioSource pageSwapSFX;

    private void Start()
    {
        initialPageOnStart = GlobalVariables.GetVariable(initialPageIndexVarLabel);
        pageSwapSFX = GetComponent<AudioSource>();

        SetupInitialPagesHeights();
        SetupInitialPage();

        var recognizer = GetComponent<HorizontalSwipeGestureRecognizer>();
        recognizer.SwipeCallback = (direction) =>
        {
            if (isPageSwappingEnabled && direction == HorizontalSwipeGestureRecognizer.SwipeDirection.Right)
            {
                SwapToNextPage();
            }

            if (isPageSwappingEnabled && direction == HorizontalSwipeGestureRecognizer.SwipeDirection.Left)
            {
                SwapToPreviousPage();
            }
        };
    }

    private void SetupInitialPagesHeights()
    {
        for (int pageIndex = 0; pageIndex < mPages.Length; pageIndex++)
        {
            var position = mPages[pageIndex].position;
            mPages[pageIndex].position = new Vector3(position.x, PageHeight(pageIndex, false), position.z);
        }
    }

    private void SetupInitialPage()
    {
        mCurrentPage = initialPageOnStart.IntegerValue;

        for (int i = 0; i < initialPageOnStart.IntegerValue; i++)
        {
            mPages[i].position = new Vector3(-PageSideOffset, PageHeight(i, true), mPages[i].position.z);
        }

        SetPagesHotspot();
    }

    private void SwapToNextPage()
    {
        if (mCurrentPage < mPages.Length - 1)
        {
            SwipePageAnimation(mCurrentPage, true);
            mCurrentPage++;
            pageSwapSFX.Play();
        }

        SetPagesHotspot();
        initialPageOnStart.IntegerValue = mCurrentPage;
    }


    private void SwapToPreviousPage()
    {
        if (mCurrentPage > 0)
        {
            mCurrentPage--;
            SwipePageAnimation(mCurrentPage, false);
            pageSwapSFX.Play();
        }

        SetPagesHotspot();
        initialPageOnStart.IntegerValue = mCurrentPage;
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

    private void SetHotspotIfNotAbsent(int pageIndex, bool shouldEnableHotspot)
    {
        //I would have preffered to use the Hotspot component, yet AC's manual activation of hotspot is a bit problematic 
        //So this is a small workaround.
        var hotspot = mPages[pageIndex].GetComponentInChildren<Hotspot>(true);

        if (hotspot != null)
        {
            var gameobject = hotspot.gameObject;
            gameobject.SetActive(shouldEnableHotspot);
        }
    }

    private void SwipePageAnimation(int pageIndex, bool isMovingToLeftSide)
    {
        isPageSwappingEnabled = false;
        mPages[pageIndex].DOMoveY(PagePeakHeight, pageTweenVerticalDuration)
            .OnComplete(() => mPages[pageIndex].DOMoveX(PageSideOffset * (isMovingToLeftSide ? -1 : 1), pageTweenHorizontalDuration)
            .OnComplete(() => mPages[pageIndex].DOMoveY(PageHeight(pageIndex, isMovingToLeftSide), pageTweenVerticalDuration)
            .OnComplete(() => isPageSwappingEnabled = true)));
    }

    // Setup page height relative to its index.
    private float PageHeight(int pageIndex, bool isMovingToLeftSide)
    {
        var pageOffset = pageIndex / 200.0f;
        return pageHeightOffset + (pageOffset * (isMovingToLeftSide ? 1 : -1));
    }

    public void OnFolderEnter()
    {
        SetHotspotIfNotAbsent(mCurrentPage, true);

        if (mCurrentPage > 0)
        {
            SetHotspotIfNotAbsent(mCurrentPage - 1, true);
        }
    }

    public void OnFolderExit()
    {
        SetHotspotIfNotAbsent(mCurrentPage, false);

        if (mCurrentPage > 0)
        {
            SetHotspotIfNotAbsent(mCurrentPage - 1, false);
        }
    }
}
