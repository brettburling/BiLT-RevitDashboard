using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Commands
{
    [Transaction(TransactionMode.Manual)]
    class CmCadLinkInWorkset : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {


            var doc = commandData.Application.ActiveUIDocument.Document;

            bool CADok = doc.CADLinksAreInWorkset("Links");
            bool RvtOk = doc.RVTLinksInWorkset("Links");


            if (RvtOk && CADok)
            {
                MessageBox.Show("All links are on in the correct workset", "Link Location", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("All Links are NOT in the correct workset", "Link Location", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }


            return Result.Succeeded;
        }




    }
}
