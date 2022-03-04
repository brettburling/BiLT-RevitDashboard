using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Commands
{

    [Transaction(TransactionMode.Manual)]
    class CmdNumberOfLineStyles: IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;

            int count = doc.CollectLineStyles().Count;
            MessageBox.Show("There are " + count + " line styles  in the model.");



            return Result.Succeeded;
        }
    }
}