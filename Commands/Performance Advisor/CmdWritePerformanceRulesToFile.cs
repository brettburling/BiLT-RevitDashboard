using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Win32;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Commands
{
    [Transaction(TransactionMode.Manual)]
    class CmdListPerformanceRules : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //get ids
            var ids = PerformanceAdviser.GetPerformanceAdviser().GetAllRuleIds();

            //setup a string buildeer to hold info
            var sb = new System.Text.StringBuilder();

            //iterate over the rules
            foreach (var id in ids)
            {
                var name = PerformanceAdviser.GetPerformanceAdviser().GetRuleName(id);
                var description = PerformanceAdviser.GetPerformanceAdviser().GetRuleDescription(id);
                var elementFocused = PerformanceAdviser.GetPerformanceAdviser().WillRuleCheckElements(id);

                sb.AppendLine(name + "|" + description);
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
