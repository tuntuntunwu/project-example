using System;
using System.Collections.Generic;
using System.Web;
using Model;
/// <summary>
///SectionReport 的摘要说明
/// </summary>
public class SectionReport
{
    public SectionInfoEntry sectioninfo = new SectionInfoEntry();
    public List<SectionGroupReport> grouplist = new List<SectionGroupReport>();
    public List<SectionReport> sectionlist = new List<SectionReport>();
    // 若无所属用户组，grouplist.Count == 0；若无所属子集团，sectionlist.Count == 0

    public void AddGroupReport(SectionGroupReport groupreport)
    {
        grouplist.Add(groupreport);
    }
    public void AddSectionReport(SectionReport sectionreport)
    {
        sectionlist.Add(sectionreport);
    }
    public void outPutReport()
    {

    }
}