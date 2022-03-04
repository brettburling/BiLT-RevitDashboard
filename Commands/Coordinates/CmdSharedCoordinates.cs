using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Commands
{
    [Transaction(TransactionMode.Manual)]
    class CmdSharedCoordinates : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;


            var sp = doc.CollectSharedBasePoint();
            foreach (var i in sp)
            {
                Parameter spEW = i.get_Parameter(BuiltInParameter.BASEPOINT_EASTWEST_PARAM);
                Parameter spNS = i.get_Parameter(BuiltInParameter.BASEPOINT_NORTHSOUTH_PARAM);
                Parameter spElevation = i.get_Parameter(BuiltInParameter.BASEPOINT_ELEVATION_PARAM);
                
                MessageBox.Show(spEW.AsValueString() + ", " + spNS.AsValueString() + ", " + spElevation.AsValueString(),"Survey Point");
            }



            var pbp = doc.CollectProjectBasePoint();
            foreach (var i in pbp)
            {
                Parameter pbpEW = i.get_Parameter(BuiltInParameter.BASEPOINT_EASTWEST_PARAM);
                Parameter pbpNS = i.get_Parameter(BuiltInParameter.BASEPOINT_NORTHSOUTH_PARAM);
                Parameter pbpElevation = i.get_Parameter(BuiltInParameter.BASEPOINT_ELEVATION_PARAM);
                Parameter pbpAngleToNorth = i.get_Parameter(BuiltInParameter.BASEPOINT_ANGLETON_PARAM);

                MessageBox.Show(pbpEW.AsValueString() + ", " + pbpNS.AsValueString() + ", " + pbpElevation.AsValueString() + ", " + pbpAngleToNorth.AsValueString(),"Project Base Point");
            }

            return Result.Succeeded;

        }
    }
}
