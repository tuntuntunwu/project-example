#region Copyright SHARP Corporation
//	Copyright (c) 2010 SHARP CORPORATION. All rights reserved.
//
//	SHARP Simple EA Application
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
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
/// GridView In Simple EA Application
/// </summary>
/// <Date>2010.06.07</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public class ListMainPage : MainPage 
{
    // JavaScript Function Name for Confirm dialog.
    // When Delete Button Click
    private const string CON_JSCRIPT_FUN = "scriptConfirm";

    // GridView
    private GridView detailGridView;
    public GridView CustomGridView
    {
        get { return detailGridView; }
    }

    // The Select/DeSelect All Button.
    private Button selectbtn;
    public Button SelectButton
    {
        get { return selectbtn; }
    }


    // The PageIndex property to display that page in GridView.
    private DropDownList numperpage;
    public DropDownList CustomNumPerPage
    {
        get { return numperpage; }
    }

    // The number of records which show per page by the user.
    private DropDownList pageList;
    public DropDownList CustomPageList
    {
        get { return pageList; }
    }

    // The count page Control of this GridView.
    private Label totalpageLabel;
    public Label CustomTotalpageLabel
    {
        get { return totalpageLabel; }
    }

    // The DataKeys in this GridView.
    private string strFieldNames;
    public string FieldNames
    {
        get { return strFieldNames; }
    }

    // add by Zheng Wei 2012.03.14
    // The next page button in this GridView.
    private ImageButton nextButton;
    public ImageButton NextButton
    {
        get { return nextButton; }
    }

    // add by Zheng Wei 2012.03.14
    // The per page button in this GridView.
    private ImageButton perButton;
    public ImageButton PerButton
    {
        get { return perButton; }
    }

    #region "Simple EA GridView"

    /// <summary>
    /// Simple EA GridView
    /// For Group List page and User List page
    /// </summary>
    /// <param name="_detailGridView">GridView in this page</param>
    /// <param name="_numperpage">The PageIndex property to display that page in GridView.</param>
    /// <param name="_pageList">The number of records which show per page by the user.</param>
    /// <param name="_totalpageLabel">The count page Control of this GridView.</param>
    /// <param name="_selectbtn">The Select/DeSelect All Button.</param>
    /// <param name="_strFieldNames">The DataKeys in this GridView.</param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void SetListMainPgae(GridView _detailGridView,
            DropDownList _numperpage,
            DropDownList _pageList,
            Label _totalpageLabel,
            Button _selectbtn,
            string _strFieldNames)
    {
        // GridView in this page
        detailGridView = _detailGridView;
        detailGridView.DataBound += new EventHandler(CustomView_DataBound);
        detailGridView.PreRender += new EventHandler(CustomView_PreRender);
        detailGridView.Sorted += new EventHandler(CustomView_OnSorted);
        detailGridView.Sorting += new GridViewSortEventHandler(CustomView_OnSorting);
        detailGridView.AllowPaging = true;
        detailGridView.AutoGenerateColumns = false;

        // AllowPaging
        detailGridView.AllowPaging = true;
        // AllowSorting
        detailGridView.AllowSorting = true;
        // Number Control for GridView
        numperpage = _numperpage;
        // PageList Control for GridView
        // The number of records which show per page by the user.
        pageList = _pageList;
        // Total page Control for GridView
        totalpageLabel = _totalpageLabel;
        // Select/DeSelect All Button
        selectbtn = _selectbtn;
        // the DataKeys in this GridView
        strFieldNames = _strFieldNames;

        Page.ClientScript.RegisterStartupScript(this.GetType(), "SetFocusInFirstItemBtn", "<script type='text/javascript'>SetFocusInFirstItemBtn();</script>");
    }
    #endregion

    #region "Simple EA GridView with pagecontrl"

    /// <summary>
    /// Simple EA GridView
    /// For Group List page and User List page
    /// </summary>
    /// <param name="_detailGridView">GridView in this page</param>
    /// <param name="_numperpage">The PageIndex property to display that page in GridView.</param>
    /// <param name="_pageList">The number of records which show per page by the user.</param>
    /// <param name="_totalpageLabel">The count page Control of this GridView.</param>
    /// <param name="_selectbtn">The Select/DeSelect All Button.</param>
    /// <param name="_strFieldNames">The DataKeys in this GridView.</param>
    /// <Date>2012.03.14</Date>
    /// <Author>SLC Zheng Wei</Author>
    /// <Version>1.2</Version>
    public void SetListMainPgae(GridView _detailGridView,
            DropDownList _numperpage,
            DropDownList _pageList,
            Label _totalpageLabel,
            Button _selectbtn,
            string _strFieldNames,
            ImageButton _nextButton,
            ImageButton _perButton

        )
    {
        // GridView in this page
        detailGridView = _detailGridView;
        detailGridView.DataBound += new EventHandler(CustomView_DataBound);
        detailGridView.PreRender += new EventHandler(CustomView_PreRender);
        detailGridView.Sorted += new EventHandler(CustomView_OnSorted);
        detailGridView.Sorting += new GridViewSortEventHandler(CustomView_OnSorting);
        detailGridView.AllowPaging = true;
        detailGridView.AutoGenerateColumns = false;

        // AllowPaging
        detailGridView.AllowPaging = true;
        // AllowSorting
        detailGridView.AllowSorting = true;
        // Number Control for GridView
        numperpage = _numperpage;
        // PageList Control for GridView
        // The number of records which show per page by the user.
        pageList = _pageList;
        // Total page Control for GridView
        totalpageLabel = _totalpageLabel;
        // Select/DeSelect All Button
        selectbtn = _selectbtn;
        // the DataKeys in this GridView
        strFieldNames = _strFieldNames;

        perButton = _perButton;
        nextButton = _nextButton;

        Page.ClientScript.RegisterStartupScript(this.GetType(), "SetFocusInFirstItemBtn", "<script type='text/javascript'>SetFocusInFirstItemBtn();</script>");
    }
    #endregion

    #region "Set the The number of records to show per page by the user."
    /// <summary>
    /// Set the The number of records to show per page by the user.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void ddlNumPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        // edit by Zheng Wei 2012.03.14
        // Set the The number of records to show per page by the user.
        //this.CustomGridView.PageSize = int.Parse(this.CustomNumPerPage.SelectedValue.ToString());
        //this.CustomGridView.PageIndex = 0;

        // edit by Zheng Wei 2012.03.14
        if (!string.IsNullOrEmpty(CustomNumPerPage.SelectedValue))
        {
            // Set the The number of records to show per page by the user.
            this.CustomGridView.PageSize = int.Parse(this.CustomNumPerPage.SelectedValue.ToString());
            this.CustomGridView.PageIndex = 0;
            if (nextButton != null)
                NextButton.Enabled = true;
            if (PerButton != null)
                PerButton.Enabled = true;
        }
        else
        {
            this.CustomGridView.AllowPaging = false;
            if (nextButton != null)
                NextButton.Enabled = false;
            if (PerButton != null)
                PerButton.Enabled = false;
        }
    }
    #endregion

    #region "Set the PageIndex property to display that page selected by the user."
    /// <summary>
    /// Set the PageIndex property to display that page selected by the user.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void ddlIndexOfPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        // Set the PageIndex property to display that page selected by the user.
        this.CustomGridView.PageSize = int.Parse(this.CustomNumPerPage.SelectedValue.ToString());
        this.CustomGridView.PageIndex = list.SelectedIndex;
    }
    #endregion

    #region "Show Next page."
    /// <summary>
    /// Show Next page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void IndexOfPage_Next(object sender, EventArgs e)
    {
        this.CustomGridView.PageSize = int.Parse(this.CustomNumPerPage.SelectedValue.ToString());
        if (this.CustomGridView.PageIndex < this.CustomGridView.PageCount - 1)
        {
            // Show Next page.
            this.CustomGridView.PageIndex = this.CustomGridView.PageIndex + 1;
        }
    }
    #endregion

    #region "Show Pre Page."
    /// <summary>
    /// Show Pre Page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void IndexOfPage_Pre(object sender, EventArgs e)
    {
        this.CustomGridView.PageSize = int.Parse(this.CustomNumPerPage.SelectedValue.ToString());
        if (this.CustomGridView.PageIndex > 0)
        {
            // Show Pre Page.
            this.CustomGridView.PageIndex = this.CustomGridView.PageIndex - 1;
        }
    }
    #endregion

    #region "Select Or Deselect All records. "
    /// <summary>
    /// Select Or Deselect All records.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnSelectAll_Click(object sender, EventArgs e)
    {
        Button btnSelectAll = (Button)sender;
        string btnName = btnSelectAll.Text;

        if (this.CustomGridView.DataSourceID == "")
        {
            return;
        }

        // Select Or Deselect All records.
        if (UtilConst.CON_DESELECT_ALL.Equals(btnName))
        {
            // Select All records.
            if (SelectAllOrCancel(false))
            {
                btnSelectAll.Text = UtilConst.CON_SELECT_ALL;
            }

        }
        else
        {
            // Deselect All records.
            if (SelectAllOrCancel(true))
            {
                btnSelectAll.Text = UtilConst.CON_DESELECT_ALL;
            }

        }

    }

    #endregion

    #region "Function:Select Or Deselect All records."
    /// <summary>
    /// Function:Select Or Deselect All records.
    /// </summary>
    /// <param name="checkstuts">
    ///     True:CheckBox SelectAll
    ///     False:CheckBox DeSelectAll
    /// </param>
    /// <returns>
    ///     True:Checked
    ///     False:No Item Can be Select.
    /// </returns>
    /// <seealso cref="btnSelectAll_Click"/>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private Boolean SelectAllOrCancel(Boolean checkstuts)
    {
        Boolean retVal = false;
        for (int i = 0; i <= this.CustomGridView.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)this.CustomGridView.Rows[i].FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
            if (cbox != null && cbox.Enabled == true)
            {
                cbox.Checked = checkstuts;
                retVal = true;
            }
        }
        return retVal;
    }
    #endregion

    #region "Occurs when a data is bound to data in this GridView."
    /// <summary>
    /// Occurs when a data is bound to data in this GridView.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void CustomView_DataBound(object sender, EventArgs e)
    {
        GridView CustomGridView = (GridView)sender;

        this.CustomPageList.Items.Clear();

        if (this.CustomPageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < CustomGridView.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == CustomGridView.PageIndex)
                {
                    item.Selected = true;
                }

                // Add the ListItem object to the Items collection of the 
                // DropDownList.
                this.CustomPageList.Items.Add(item);

            }

        }

        if (this.CustomTotalpageLabel != null)
        {

            // Calculate the current page number.
            //int currentPage = CustomersGridView.PageIndex + 1;

            // Update the Label control with the current page information.
            //totalpageLabel.Text = "Page " + currentPage.ToString() +
            //  " of " + CustomersGridView.PageCount.ToString();

            this.CustomTotalpageLabel.Text = CustomGridView.PageCount.ToString();

        }

        //this.SelectButton.Text = UtilConst.CON_SELECT_ALL;

    }
    #endregion

    #region "Raises the GridView.Sorted event."
    /// <summary>
    /// Raises the GridView.Sorted event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <seealso cref="SortGridView"/>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void CustomView_OnSorted(object sender, EventArgs e)
    {
        GridView CustomGridView = (GridView)sender;
        SortGridView(CustomGridView);
    }
    #endregion

    #region "Function:Raises the GridView.Sorted event."
    /// <summary>
    /// Function:Raises the GridView.Sorted event.
    /// It'll be overrided In each page.
    /// </summary>
    /// <param name="gv"></param>
    /// <seealso cref="SortGridView"/>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public virtual void SortGridView(GridView gv)
    {
    }
    #endregion

    #region "Raises the GridView.PreRender event."
    /// <summary>
    /// Raises the GridView.PreRender event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <seealso cref="renderEmptyGridView"/>
    /// <seealso cref="CustomView_OnUnload"/>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void CustomView_PreRender(object sender, EventArgs e)
    {
        GridView CustomGridView = (GridView)sender;
        // When DataSource is Null Or row's Count is 0.
        // Show the GridView Header In page;
        if (CustomGridView.Rows.Count == 0)
        {
            // Add A Null Row In GridView
            //renderEmptyGridView(CustomGridView, "GroupName,UserCount");
            renderEmptyGridView(CustomGridView);
        }
    }
    #endregion

    #region "Raises the GridView.PreRender event."
    /// <summary>
    /// Raises the GridView.Unload event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <seealso cref="CustomView_PreRender"/>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void CustomView_OnUnload(object sender, EventArgs e)
    {
        GridView CustomGridView = (GridView)sender;

        // When DataSource is Null Or row's Count is 0.
        // In CustomView_PreRender.renderEmptyGridView 
        // Set DataSourceID is null.
        if (CustomGridView.DataSourceID == null)
        {
            CustomGridView.DataSource = null;
        }
    }
    #endregion

    #region "CustomView_OnSorting"
    /// <summary>
    /// CustomView_OnSorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void CustomView_OnSorting(object sender, EventArgs e)
    {
        if (this.CustomGridView.DataSourceID == "")
        {
            return;
        }
    }
    #endregion 

    #region "Add A Null Row In GridView"
    /// <summary>
    /// Add A Null Row In GridView
    /// </summary>
    /// <param name="EmptyGridView">GridView In page</param>
    /// <seealso cref="CustomView_PreRender"/>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void renderEmptyGridView(GridView EmptyGridView)
    {
        DataTable dTable = new DataTable();
        char[] delimiterChars = { ',' };
        string[] colName = FieldNames.Split(delimiterChars);

        foreach (string myCol in colName)
        {
            DataColumn dColumn = new DataColumn(myCol.Trim());
            dTable.Columns.Add(dColumn);
        }

        DataRow dRow = dTable.NewRow();
        foreach (string myCol in colName)
        {
            dRow[myCol.Trim()] = DBNull.Value;

        }

        dTable.Rows.Add(dRow);
        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
        EmptyGridView.Sort("", SortDirection.Ascending);
        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ED
        EmptyGridView.DataSourceID = null;
        EmptyGridView.DataSource = dTable;
        EmptyGridView.DataBind();
        EmptyGridView.Rows[0].Visible = false;
    }
    #endregion

    #region "the confirmation dialog with msg Item."
    /// <summary>
    /// the confirmation dialog with msg Item.
    /// </summary>
    /// <seealso cref="ConfirmFunction"/>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void ScriptConfirmDel() {
        ScriptConfirm(CON_JSCRIPT_FUN, "");
    }
    #endregion

    #region "message for page"
    /// <summary>
    /// message for page
    /// </summary>
    /// <param name="DelectConfirmmsg">delect Confirm Message</param>
    /// <returns>OnClientClick Function</returns>
    /// <seealso cref="ScriptConfirmList"/>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected string ConfirmFunction(string DelectConfirmmsg)
    {
        string strMsg;
        strMsg = CON_JSCRIPT_FUN + "('{0}')";
        strMsg = string.Format(strMsg, DelectConfirmmsg);
        // confirmation dialog
        ScriptConfirmDel();

        return strMsg;
    }
    #endregion

    #region"return the Item List"
    /// <summary>
    /// return the Search Item List
    /// </summary>
    /// <param name="ItemValue"></param>
    /// <param name="ItemText"></param>
    /// <returns>ListItem</returns>
    /// <Date>2010.12.3</Date>
    /// <Author>SES ZhouMiao</Author>
    /// <Version>1.1</Version>
    public ListItem ComBoxListItem(String ItemValue, String ItemText)
    {
        ListItem item = new ListItem();
        item.Value = ItemValue;
        item.Text = ItemText;
        return item;

    }
    #endregion

}
