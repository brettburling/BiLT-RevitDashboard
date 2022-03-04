using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDashboard.DB.Extensions;
using System.Windows;

namespace RevitDashboard.Commands
{

    [Transaction(TransactionMode.Manual)]
    class CmdLocalPath : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;

            MessageBox.Show("The local path of the current document is: " + doc.LocalPath());

            return Result.Succeeded;

        }
    }
}
