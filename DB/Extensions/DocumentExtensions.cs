using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace RevitDashboard.DB.Extensions
{
    static class DocumentExtensions
    {
public static string LocalPath(this Document doc)
{
    //get document path
    var path = doc.PathName;

    if (path.StartsWith("Autodesk Docs"))
    {
        //get document GUID
        var revitGuidName = doc.WorksharingCentralGUID + ".rvt";

        //get local cache location
        var localCacheFolder = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Autodesk\\Revit\\Autodesk Revit " + doc.Application.VersionNumber + "\\CollaborationCache";

        //search cache for file
        path = (from p in Directory.GetFiles(localCacheFolder, revitGuidName, SearchOption.AllDirectories)
                where p.Contains("CentralCache") == false
                select p).FirstOrDefault();
    }

    return path;

}


public static double FileSizeInMegabytes(this Document doc)
{
    //get the local path
    var localPath = doc.LocalPath();

    if (localPath != null)
    {
        //get its size
        return Utils.Utils.GetSizeInMegabytes(localPath);
    }

    return double.NaN;
}


        public static List<View> CollectViews(this Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementCategoryFilter(BuiltInCategory.OST_Views);

            return (from e in collector.WherePasses(filter).WhereElementIsNotElementType().Cast<View>()
                    where e.CanBePrinted
                    select e).ToList();
        }


        public static List<View> CollectViewsNotOnSheets(this Document doc)
        {
            return (from v in doc.CollectViews()
                    where v.IsOnSheet() == false
                    select v).ToList();
        }


        public static List<View> CollectViewsOnSheets(this Document doc)
        {
            return (from v in doc.CollectViews()
                    where v.IsOnSheet()
                    select v).ToList();
        }


public static List<View> CollectViewsOfType(this Document doc, ViewType viewType)
{
    return (from v in doc.CollectViews()
            where v.ViewType == viewType
            select v).ToList();
}


        public static List<ViewSheet> CollectSheets(this Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementCategoryFilter(BuiltInCategory.OST_Sheets);

            return (from e in collector.WherePasses(filter).WhereElementIsNotElementType().Cast<ViewSheet>()
                    where e.IsPlaceholder == false
                    select e).ToList();
        }


        public static List<ImportInstance> CollectCadImports(this Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementClassFilter(typeof(ImportInstance));

            return (from e in collector.WherePasses(filter).Cast<ImportInstance>()
                    where e.IsLinked == false
                    select e).ToList();
        }


        public static List<CADLinkType> CollectCadLinkTypes(this Document doc)
        {
            return (from r in doc.CollectAllExternalReferences()
                    where r is CADLinkType
                    select r as CADLinkType).ToList();
        }


        public static List<CADLinkType> CollectCadLinkTypes(this Document doc, string fileType)
        {
            return (from e in doc.CollectCadLinkTypes()
                    where e.Name.EndsWith(fileType, StringComparison.OrdinalIgnoreCase)
                    select e).ToList();
        }



        public static List<ExternalResourceReference> CollectCadLinkTypesExternalResourceReferences(this Document doc)
        {
            var listToReturn = new List<ExternalResourceReference>();

            foreach (var cadLinkType in doc.CollectCadLinkTypes())
            {

                var map = cadLinkType.GetExternalResourceReferences();
                var keys = map.Keys;

                foreach (var key in keys)
                {
                    var reference = map[key];
                    listToReturn.Add(reference);
                }

            }

            return listToReturn;
        }




        public static List<Element> CollectAllExternalReferences(this Document doc)
        {
            var returnList = new List<Element>();

            ISet<ElementId> xrefs = ExternalResourceUtils.GetAllExternalResourceReferences(doc);

            foreach (ElementId id in xrefs)
            {

                returnList.Add(doc.GetElement(id));
            }

            return returnList;
        }








        public static List<Family> CollectFamilies(this Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementClassFilter(typeof(Family));

            return (from e in collector.WherePasses(filter).Cast<Family>()
                    where e.IsEditable
                    select e).ToList();
        }


public static List<FilledRegionType> CollectFilledRegionTypes(this Document doc)
{
    var collector = new FilteredElementCollector(doc);
    var filter = new ElementClassFilter(typeof(FilledRegionType));

    return (from e in collector.WherePasses(filter).Cast<FilledRegionType>()
            select e).ToList();
}


        public static List<Group> CollectGroupInstances(this Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementClassFilter(typeof(Group));

            return (from e in collector.WherePasses(filter).Cast<Group>()
                    select e).ToList();
        }





        public static List<Family> CollectInPlaceFamilies(this Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementClassFilter(typeof(Family));

            return (from e in collector.WherePasses(filter).Cast<Family>()
                    where e.IsInPlace
                    select e).ToList();
        }


        public static List<BasePoint> CollectProjectBasePoint(this Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementCategoryFilter(BuiltInCategory.OST_ProjectBasePoint);

            return (from e in collector.WherePasses(filter).WhereElementIsNotElementType().Cast<BasePoint>()
                    select e).ToList();
        }


        public static List<BasePoint> CollectSharedBasePoint(this Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementCategoryFilter(BuiltInCategory.OST_SharedBasePoint);

            return (from e in collector.WherePasses(filter).WhereElementIsNotElementType().Cast<BasePoint>()
                    select e).ToList();
        }


        public static List<TextNoteType> CollectTextTypes(this Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementClassFilter(typeof(TextNoteType));

            return (from e in collector.WherePasses(filter).Cast<TextNoteType>()
                    select e).ToList();
        }

        public static List<DimensionType> CollectDimensionTypes(this Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementClassFilter(typeof(DimensionType));

            return (from e in collector.WherePasses(filter).Cast<DimensionType>()
                    select e).ToList();
        }


        public static List<Element> CollectLineStyles(this Document doc)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementCategoryFilter(BuiltInCategory.OST_Lines);


            var lineStyleIds = (from e in collector.WherePasses(filter).WhereElementIsNotElementType().Cast<CurveElement>()
                                select e).FirstOrDefault().GetLineStyleIds();

            return (from id in lineStyleIds
                    select doc.GetElement(id)).ToList();

        }





        public static List<FamilyInstance> CollectAllFamilyInstancesOf(this Document doc, string familyName, string symbolName)
        {
            var collector = new FilteredElementCollector(doc);
            var filter = new ElementClassFilter(typeof(FamilyInstance));

            return (from fi in collector.WherePasses(filter).Cast<FamilyInstance>()
                    where fi.Symbol.Name.ToUpper() == symbolName.ToUpper() &&
                          fi.Symbol.Family.Name.ToUpper() == familyName.ToUpper()
                    select fi).ToList();

        }


        public static List<Element> CollectPurgeableElements(this Document doc)
        {
            return doc.CollectFailingElements("e8c63650-70b7-435a-9010-ec97660c1bda");
        }


        public static List<Element> CollectOverlappingWalls(this Document doc)
        {
            return doc.CollectFailingElements("cbb14baa-a57c-48ee-aab4-4137fcad779e");
        }


        public static List<Element> CollectDuplicateInstances(this Document doc)
        {
            return doc.CollectFailingElements("b341a0f3-a468-4fad-8b26-39237d8486e7");
        }


        public static List<Element> CollectComplexSketchInstances(this Document doc)
        {

            return doc.CollectFailingElements("f4fdc819-9044-4785-b5e3-5a36e89b9bd9");
        }



        private static List<Element> CollectFailingElements(this Document doc, string ruleGuid)
        {
            var ruleId = doc.GetPerformanceAdvisorRuleId(new Guid(ruleGuid));
            var failureMessages = PerformanceAdviser.GetPerformanceAdviser().ExecuteRules(doc, ruleId);

            var returnList = new List<Element>();
            if (failureMessages.Count > 0)
            {
                foreach (var fm in failureMessages)
                {
                    returnList.AddRange(from id in fm.GetFailingElements()
                                        select doc.GetElement(id));
                }
            }

            return returnList;
        }


public static List<Wall> CollectWalls( this Document doc)
{
    var collector = new FilteredElementCollector(doc);
    var filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);

    return (from e in collector.WherePasses(filter).Cast<Wall>()
        select e).ToList();



    var walls = doc.CollectWalls();
}





        public static void GetRuleInfo()
        {
            //get ids
            var ids = PerformanceAdviser.GetPerformanceAdviser().GetAllRuleIds();

            //setup a string builder to hold info
            var sb = new System.Text.StringBuilder();

            //iterate over the rules
            foreach (var id in ids)
            {
                var name = PerformanceAdviser.GetPerformanceAdviser().GetRuleName(id);
                var description = PerformanceAdviser.GetPerformanceAdviser().GetRuleDescription(id);
                var elementFocused = PerformanceAdviser.GetPerformanceAdviser().WillRuleCheckElements(id);

                sb.AppendLine(name + "|" + description + "|" + elementFocused);
            }

            //write the results to file
            File.WriteAllText(@"C:\performanceRules.csv", sb.ToString());
        }



        public static List<RevitLinkType> CollectRevitLinkTypes(this Document doc)
        {
            return (from r in doc.CollectAllExternalReferences()
                    where r is RevitLinkType
                    select r as RevitLinkType).ToList();
        }


        public static List<ExternalResourceReference> CollectRevitLinkTypesExternalResourceReferences(this Document doc)
        {
            var listToReturn = new List<ExternalResourceReference>();

            foreach (var revitLinkType in doc.CollectRevitLinkTypes())
            {

                var map = revitLinkType.GetExternalResourceReferences();
                var keys = map.Keys;

                foreach (var key in keys)
                {
                    var reference = map[key];
                    listToReturn.Add(reference);
                }

            }

            return listToReturn;
        }



        public static List<Workset> CollectWorksets(this Document doc)
        {
            FilteredWorksetCollector worksets
                = new FilteredWorksetCollector(doc)
                    .OfKind(WorksetKind.UserWorkset);

            return worksets.ToWorksets().ToList();
        }


public static bool CADLinksAreInWorkset(this Document doc, string worksetName)
{
    var allCadCorrect = true;

    foreach (var cl in doc.CollectCadLinkTypes())
    {
        foreach (var cli in cl.CollectInstances())
        {
            if (cli.IsInWorkset(worksetName) == false)
            {
                allCadCorrect = false;
                break;
            }
        }
    }

    return allCadCorrect;

}


public static bool CADLinksAreOnBIM360(this Document doc)
{
    var allCadCorrect = true;

    foreach (var cl in doc.CollectCadLinkTypesExternalResourceReferences())
    {
        if (cl.InSessionPath.StartsWith("BIM 360") == false)
        {
            allCadCorrect = false;
            break;
        }
    }

    return allCadCorrect;

}


public static bool RVTLinksInWorkset(this Document doc, string worksetName)
{
    var allRvtCorrect = true;

    foreach (var rl in doc.CollectRevitLinkTypes())
    {
        foreach (var rli in rl.CollectInstances())
        {
            if (rli.IsInWorkset(worksetName) == false)
            {
                allRvtCorrect = false;
                break;
            }
        }
    }

    return allRvtCorrect;

}


public static bool RVTLinksAreOnBIM360(this Document doc)
{
    var allRvtCorrect = true;

    foreach (var rl in doc.CollectRevitLinkTypesExternalResourceReferences())
    {
        if (rl.InSessionPath.StartsWith("Autodesk Docs") == false)
        {
            allRvtCorrect = false;
            break;
        }
    }

    return allRvtCorrect;

}





public static Workset GetWorksetNamed(this Document doc, string worksetName)
{
    return (from ws in doc.CollectWorksets()
            where ws.Name.ToUpper() == worksetName.ToUpper()
            select ws).FirstOrDefault();
}



        public static List<PerformanceAdviserRuleId> GetPerformanceAdvisorRuleId(this Document doc, Guid guid)
        {
            return (from id in PerformanceAdviser.GetPerformanceAdviser().GetAllRuleIds()
                    where id.Guid.Equals(guid)
                    select id).ToList();

        }


    }
}
