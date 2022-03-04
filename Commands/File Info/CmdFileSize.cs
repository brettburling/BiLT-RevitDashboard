using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDashboard.DB.Extensions;
using System.Windows;

namespace RevitDashboard.Commands
{

    [Transaction(TransactionMode.Manual)]
    class CmdFileSize : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;

            MessageBox.Show("The cuurent document size is: " + doc.FileSizeInMegabytes());

            return Result.Succeeded;

        }
    }
}
