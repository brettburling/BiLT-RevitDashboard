using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using RevitDashboard.DB.Extensions;
using RevitDashboard.Widgets;

namespace RevitDashboard.Commands
{

[Transaction(TransactionMode.Manual)]
class CmdDashboardRefresh : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {

        Document doc = commandData.Application.ActiveUIDocument.Document;


        using (Transaction t = new Transaction(doc))
        {
            t.Start("Dashboard Refresh");

            //build list of widgets
            var widgets = new List<Widget>();


            //file info
            widgets.Add(new FileSizeWidget(doc));
            widgets.Add(new FileInfoWidget(doc));


            //sheets
            var sheets = doc.CollectSheets().Cast<Element>().ToList();
            widgets.Add(new ElementCountWidget(doc, "SheetCount", sheets));


            //Views
            widgets.Add(new ViewTypeCountWidget(doc));

            double countViews = doc.CollectViews().Count;
            double countOnSheet = doc.CollectViewsOnSheets().Count;
            var percentage = countOnSheet / countViews * 100;
            widgets.Add(new GuageWidget(doc, "ViewsOnSheet", percentage));


            //links
            widgets.Add(new LinkTypeCountWidget(doc));

            var inWorkset = doc.CADLinksAreInWorkset("Links") && doc.RVTLinksInWorkset("Links");
            widgets.Add(new TickWidget(doc, "Links Workset", inWorkset));

            var onBIM360 = doc.CADLinksAreOnBIM360() && doc.RVTLinksAreOnBIM360();
            widgets.Add(new TickWidget(doc, "Links on BIM360", onBIM360));

            var imports = doc.CollectCadImports().Cast<Element>().ToList();
            widgets.Add(new ElementCountWidget(doc, "Imports", imports));


            //content
            var families = doc.CollectFamilies().Cast<Element>().ToList();
            widgets.Add(new ElementCountWidget(doc, "Families", families));

            var inplaceFamilies = doc.CollectInPlaceFamilies().Cast<Element>().ToList();
            widgets.Add(new ElementCountWidget(doc, "Inplace", inplaceFamilies));

            var groups = doc.CollectGroupInstances().Cast<Element>().ToList();
            widgets.Add(new ElementCountWidget(doc, "GroupCount", groups));


            //warnings
            widgets.Add(new WarningCountWidget(doc));


            //performance adviser
            var duplicates = doc.CollectDuplicateInstances();
            widgets.Add(new ElementCountWidget(doc, "DuplicateInstances", duplicates));

            var complexElements = doc.CollectComplexSketchInstances();
            widgets.Add(new ElementCountWidget(doc, "ComplexSketch", complexElements));

            var overlappingWalls = doc.CollectOverlappingWalls();
            widgets.Add(new ElementCountWidget(doc, "OverlappingWalls", overlappingWalls));


            //purgeable elements
            var purgeableElements = doc.CollectPurgeableElements();
            widgets.Add(new ElementCountWidget(doc, "Purgeable", purgeableElements));


            //coordinates
            var basepoint = doc.CollectProjectBasePoint().FirstOrDefault();
            widgets.Add(new CoordinateWidget(doc, "projectBasePoint", basepoint));

            var isAtOrigin = basepoint.Position.DistanceTo(XYZ.Zero) < double.Epsilon;
            widgets.Add(new TickWidget(doc, "ProjectBasePoint", isAtOrigin));

            var surveyPoint = doc.CollectSharedBasePoint().FirstOrDefault();
            widgets.Add(new CoordinateWidget(doc, "surveyPoint", surveyPoint));


            //styles
            var textTypes = doc.CollectTextTypes().Cast<Element>().ToList();
            widgets.Add(new ElementCountWidget(doc, "TextTypes", textTypes));

            var dimensionTypes = doc.CollectDimensionTypes().Cast<Element>().ToList();
            widgets.Add(new ElementCountWidget(doc, "DimensionTypes", dimensionTypes));

            var lineStyles = doc.CollectLineStyles();
            widgets.Add(new ElementCountWidget(doc, "LineStyles", lineStyles));

            var filledRegionTypes = doc.CollectFilledRegionTypes().Cast<Element>().ToList();
            widgets.Add(new ElementCountWidget(doc, "FilledRegionTypes", filledRegionTypes));

            //refresh widget data
            foreach (var w in widgets)
            {
                w.Refresh();
            }

            t.Commit();
        }

        return Result.Succeeded;

    }
}
}
