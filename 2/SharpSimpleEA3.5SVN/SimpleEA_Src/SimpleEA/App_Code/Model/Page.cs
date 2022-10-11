using System;
using System.Collections.Generic;
using System.Globalization;
//using System.Linq;
using System.Web;
/**
 * module 共通模块
 * class Page.java
 * function 分页容器
 * author  陈优广
 * remark
 */
public class Page <T>
{
    // 当前页
    public int CurrentPage = 1;
    // 每页显示条数
    public int PageSize = 20;
    // 数据总条数
    public int totalCount = 0;
    private int pageCount = 0;

    public List<T> dataList = new List<T>();
    public int PageCount
    {
        get
        {
            return pageCount;
        }

    }
    public int TotalCount
    {
        set
        {
            totalCount = value;
            pageCount = (totalCount + PageSize-1) / PageSize;
        }
    }
}