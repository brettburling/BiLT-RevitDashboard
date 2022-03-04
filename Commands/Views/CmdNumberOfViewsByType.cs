using System;
using System.Linq;
using System.Text;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Commands
{

    [Transaction(TransactionMode.Manual)]
    class CmdNumberOfViewsByType : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            

            var stringBuilder = new StringBuilder();
            var listToLookFor = Enum.GetValues(typeof(ViewType)).Cast<ViewType>().ToList();

            foreach (var v in listToLookFor)
            {
                var count = doc.CollectViewsOfType(v).Count;
                stringBuilder.AppendLine(count + " " + v + " views in the model");
            }


            MessageBox.Show(stringBuilder.ToString());



            return Result.Succeeded;

        }
    }
}