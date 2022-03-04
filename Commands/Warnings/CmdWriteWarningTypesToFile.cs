using System.Linq;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Win32;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Commands
{
    [Transaction(TransactionMode.Manual)]
    class CmdListWarnings : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            //get all failures
            var registry = Autodesk.Revit.ApplicationServices.Application.GetFailureDefinitionRegistry();
            var failures = registry.ListAllFailureDefinitions();

            //filter for failures that are warnings
            var warnings = from f in failures
                           where f.GetSeverity() == FailureSeverity.Warning
                           select f;

            //setup a string builder to hold info
            var sb = new System.Text.StringBuilder();


            //iterate over the warnings
            foreach (var w in warnings)
            {
                sb.AppendLine(w.GetDescriptionText());
            }

            //write the results to file
            var saveAsDialog = new SaveFileDialog();
            saveAsDialog.InitialDirectory = @"c:\";
            saveAsDialog.Title = "Save CSV";
            saveAsDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

            if (saveAsDialog.ShowDialog() == true)
            {
                System.IO.File.WriteAllText(saveAsDialog.FileName, sb.ToString());

            }



            return Result.Succeeded;

        }
    }
}
