using System;
using System.IO;
using Autodesk.Revit.DB;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Widgets
{
    class FileInfoWidget : Widget
    {


        //constructors
        public FileInfoWidget(Document document) : base(document, "Widget - File Name", "Family1")
        {
        }


        //modifiers
public override bool Refresh()
{
    //get full document path
    var filePath = Document.LocalPath();

    // folder
    var folder = Path.GetDirectoryName(filePath);
    SetParameterValue("Folder", folder);

    // file name
    var fileName = Document.Title;
    SetParameterValue("Filename", fileName);

    //date
    var date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm");
    SetParameterValue("Date", date);

    //username
    var username = Environment.UserName;
    SetParameterValue("Username", username);

    return true;
}

    }
}
