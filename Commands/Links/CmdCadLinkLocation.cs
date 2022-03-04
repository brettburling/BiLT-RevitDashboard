using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Commands
{
    [Transaction(TransactionMode.Manual)]
    class CmCadLinkLocation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var doc = commandData.Application.ActiveUIDocument.Document;

            bool CADok = doc.CADLinksAreOnBIM360();
            bool RvtOk = doc.RVTLinksAreOnBIM360();


            if (RvtOk && CADok)
            {
                MessageBox.Show("All links are on BIM360", "Link Location", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("All Links are NOT BIM360", "Link Location", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }


            return Result.Succeeded;
        }




    }
}
