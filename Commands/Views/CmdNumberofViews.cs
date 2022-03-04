using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Commands
{

    [Transaction(TransactionMode.Manual)]
    class CmdNumberOfViews : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;

            int count = doc.CollectViews().Count;
            MessageBox.Show("There are " + count.ToString()+" views in the model.");
            


            return Result.Succeeded;

        }
    }
}
