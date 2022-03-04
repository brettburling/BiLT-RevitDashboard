using Autodesk.Revit.DB;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Widgets
{
    class FileSizeWidget : Widget
    {


        //constructors
        public FileSizeWidget(Document document) : base(document, "Widget - File Size", "Family1")
        {
        }



        //modifiers
public override bool Refresh()
{
    var fileSize = Document.FileSizeInMegabytes();
    SetParameterValue("File Size", fileSize);

    return true;
}


    }
}
