using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
///ImportData 的摘要说明
/// </summary>
public class ImportData : ListMainPage
{
    private FileUpload fileUpload;

	public ImportData(FileUpload fileUploadControl)
	{
        fileUpload = fileUploadControl;
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        String path = Server.MapPath(@"~\Img\") + fileUpload.FileName + DateTime.Now.ToString("yyMMddHHmmss");
        if (fileUpload.HasFile) //判断是否有文件上来了
        {
            String fileExtension =
                System.IO.Path.GetExtension(fileUpload.FileName).ToLower();//获取文件扩展名
            String[] allowedExtensions = { ".xls", ".csv", ".xlsx" }; //允许上传的文件格式
            List<string> extenstionList = new List<string>(allowedExtensions);

            if (extenstionList.Contains(fileExtension))
            {
                try
                {
                    fileUpload.PostedFile.SaveAs(path);
                    CheckInfo(path);
                }
                catch (Exception ex)
                {
                    ErrorAlert("导入发生错误，请注意检查格式是否正确！");
                    Global.Log(ex.ToString());
                }
            }
        }
        else
        {
            ErrorAlert("请选择文件！");
        }
    }

    private void CheckInfo(string path)
    {

    }
}