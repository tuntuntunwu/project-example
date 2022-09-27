
#region Copyright SHARP Corporation
//	Copyright (c) 2010-2014 SHARP CORPORATION. All rights reserved.
//
//	Extended Sharp OSA SDK
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
#endregion

namespace CommonOSA
{
    public class PageStateControl
    {
        private int currentPage;        // Current page
        private int showDataCount;      // number of data per pages

        private int pageCount;          // number of pages
        private int startIndex;         // start index number in the page
        private int lastIndex;          // last index number in the page
        private bool previousPageState; // status of up button
        private bool nextPageState;     // status of down button

        //--- Constructor ---
        public PageStateControl(int page, int dataCount)
        {
            //--- set current page ---
            currentPage = page;
            //--- set a number of data per pages ---
            showDataCount = dataCount;
        }

        //--- get a number of page  ---
        public int GetPageCount()
        {
            return this.pageCount;
        }

        //--- get a start index number in the page ---
        public int GetStartIndex()
        {
            return this.startIndex -1;
        }
        //--- get a last index number in the page ---
        public int GetLastIndex()
        {
            return this.lastIndex -1;
        }
        //--- get a status of the up button ---
        public bool GetPreviousPageState()
        {
            return this.previousPageState;
        }
        //--- get a status of the down button ---
        public bool GetNextPageState()
        {
            return this.nextPageState;
        }

        //--- Set start index, last index and a status of up/down button ---
        public void CalcCurrentPageInfo(int allDataCount)
        {
            //--- get a number of pages ---
            pageCount = (allDataCount-1) / showDataCount + 1;

            //--- Set lastIndex to the number of data ---
            lastIndex = allDataCount;
            //--- Set a status of the down button disabled ---
            nextPageState = false;

            if (1 == currentPage)
            {
                //--- Current page is the first page ---
                //--- Start index number must be 1 ---
                startIndex = 1;
                //--- Set a status of the up button disabled ---
                previousPageState = false;
            }
            else
            {
                //--- Current page is not the first page ---
                //--- Set the start index number ---
                startIndex = (currentPage - 1) * showDataCount + 1;
                //--- Set a status of the up button enabled ---
                previousPageState = true;
            }
 
            //--- Update lastIndex if the index of the last data is less than the number of the all data ---
            if (startIndex + showDataCount - 1 < lastIndex)
            {
                //--- Set the last index number in the current page ---
                lastIndex = startIndex + showDataCount - 1;
                //--- Set a status of the down button enabled ---
                nextPageState = true;
            }
        }
    }
}
