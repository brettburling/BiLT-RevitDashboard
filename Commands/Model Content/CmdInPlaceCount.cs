using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Commands
{
    [Transaction(TransactionMode.Manual)]
    class CmdInPlaceCount : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            int inPlaceCount = doc.CollectInPlaceFamilies().Count;
            
            MessageBox.Show("There are " + inPlaceCount.ToString() + " in place families in the model.\n\nYou need to have a hard word with yourself!");

            return Result.Succeeded;
        }
    }
}
