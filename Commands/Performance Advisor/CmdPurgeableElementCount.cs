using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Commands
{
    [Transaction(TransactionMode.Manual)]
    class CmdPurgeableItemCount : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            int count = doc.CollectPurgeableElements().Count;
            
            MessageBox.Show("There are " + count + " items that could be purged");

            return Result.Succeeded;
        }
    }
}
