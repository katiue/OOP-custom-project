using OfficeOpenXml;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.IO;

namespace OOP_custom_project
{
    public class Database
    {
        public Database()
        {
        }
        public void ExportMineralsToExcel(List<Mineral> minerals, string filePath)
        {
            if(minerals.Count < 1)
            {
                return;
            }

            // Sort minerals by ID
            var sortedMinerals = minerals.OrderBy(m => ToInt(m.ID)).ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Minerals");

                // Adding headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Description";
                worksheet.Cells[1, 4].Value = "Type Name";
                worksheet.Cells[1, 5].Value = "Type Description";
                worksheet.Cells[1, 6].Value = "Stiffness";
                worksheet.Cells[1, 7].Value = "Max Quantity";
                worksheet.Cells[1, 8].Value = "Color";
                worksheet.Cells[1, 9].Value = "Points";
                worksheet.Cells[1, 10].Value = "Showed Points";

                // Adding mineral data
                for (int i = 0; i < sortedMinerals.Count; i++)
                {
                    Mineral mineral = sortedMinerals[i];
                    worksheet.Cells[i + 2, 1].Value = string.Join(",", mineral.ID);
                    worksheet.Cells[i + 2, 2].Value = mineral.Name;
                    worksheet.Cells[i + 2, 3].Value = mineral.ShortDescription;
                    worksheet.Cells[i + 2, 4].Value = mineral.Type._name;
                    worksheet.Cells[i + 2, 5].Value = mineral.Type._description;
                    worksheet.Cells[i + 2, 6].Value = mineral.Type._stiffness;
                    worksheet.Cells[i + 2, 7].Value = mineral.Type._maxquantity;
                    worksheet.Cells[i + 2, 8].Value = ColorToString(mineral.Type._color);
                    worksheet.Cells[i + 2, 9].Value = PointsToString(mineral.points);
                    worksheet.Cells[i + 2, 10].Value = PointsToString(mineral.showedPoints);
                }

                // Saving the file
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
        public List<Mineral> ImportMineralsFromExcel(string filePath)
        {
            List<Mineral> minerals = new List<Mineral>();

            FileInfo fileInfo = new FileInfo(filePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Minerals"];
                if (worksheet == null)
                    throw new Exception("Worksheet 'Minerals' not found in the Excel file.");

                int rows = worksheet.Dimension.Rows;
                for (int row = 2; row <= rows; row++)
                {
                    string? idCellValue = worksheet.Cells[row, 1].Value?.ToString();
                    string? name = worksheet.Cells[row, 2].Value?.ToString();
                    string? description = worksheet.Cells[row, 3].Value?.ToString();
                    string? typeName = worksheet.Cells[row, 4].Value?.ToString();
                    string? typeDescription = worksheet.Cells[row, 5].Value?.ToString();
                    string? stiffnessCellValue = worksheet.Cells[row, 6].Value?.ToString();
                    string? maxQuantityCellValue = worksheet.Cells[row, 7].Value?.ToString();
                    string? colorString = worksheet.Cells[row, 8].Value?.ToString();
                    string? pointsString = worksheet.Cells[row, 9].Value?.ToString();
                    string? showedPointsString = worksheet.Cells[row, 10].Value?.ToString();

                    if (idCellValue != null && name != null && description != null && typeName != null && typeDescription != null && stiffnessCellValue != null && maxQuantityCellValue != null && colorString != null && pointsString != null && showedPointsString != null)
                    {
                        string[] ids = idCellValue.Split(',');
                        int stiffness = int.Parse(stiffnessCellValue);
                        int maxQuantity = int.Parse(maxQuantityCellValue);
                        Color color = ParseColor(colorString);
                        List<Point2D> points = StringToPoints(pointsString);
                        List<Point2D> showedPoints = StringToPoints(showedPointsString);

                        MineralType type = new MineralType(typeName, typeDescription, stiffness, maxQuantity, color);

                        Mineral mineral = new Mineral(ids, name, description, type)
                        {
                            points = points,
                            showedPoints = showedPoints
                        };

                        minerals.Add(mineral);
                    }
                }
            }
            return minerals;
        }
        public void ExportComponentsToExcel(List<Weapon> components, string filePath)
        {
            if (components.Count < 1)
            {
                return;
            }

            // Sort components by Order
            var sortedComponents = components.OrderBy(c => ToInt(c.ID)).ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Components");

                // Adding headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Description";
                worksheet.Cells[1, 4].Value = "Stiffness";
                worksheet.Cells[1, 5].Value = "ForgedTimes";
                worksheet.Cells[1, 6].Value = "Durability";
                worksheet.Cells[1, 7].Value = "First Mineral";
                worksheet.Cells[1, 8].Value = "Second Mineral";

                // Adding component data
                for (int i = 0; i < sortedComponents.Count; i++)
                {
                    Weapon component = sortedComponents[i];
                    worksheet.Cells[i + 2, 1].Value = string.Join(",", component.ID);
                    worksheet.Cells[i + 2, 2].Value = component.Name;
                    worksheet.Cells[i + 2, 3].Value = component.ShortDescription;
                    worksheet.Cells[i + 2, 4].Value = component.Stiffness;
                    worksheet.Cells[i + 2, 5].Value = component.ForgedTimes;
                    worksheet.Cells[i + 2, 6].Value = component.Durability;
                    worksheet.Cells[i + 2, 7].Value = component.Mineral1;
                    worksheet.Cells[i + 2, 8].Value = component.Mineral2;
                }

                // Saving the file
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
        public List<Weapon> ImportComponentsFromExcel(string filePath)
        {
            List<Weapon> components = new List<Weapon>();

            FileInfo fileInfo = new FileInfo(filePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Components"];
                if (worksheet == null)
                    throw new Exception("Worksheet 'Components' not found in the Excel file.");

                int rows = worksheet.Dimension.Rows;
                for (int row = 2; row <= rows; row++)
                {
                    string? idCellValue = worksheet.Cells[row, 1].Value?.ToString();
                    string? name = worksheet.Cells[row, 2].Value?.ToString();
                    string? description = worksheet.Cells[row, 3].Value?.ToString();;
                    string? stiffnessCellValue = worksheet.Cells[row, 4].Value?.ToString();
                    string? forgedTimesCellValue = worksheet.Cells[row, 5].Value?.ToString();
                    string? durabilityCellValue = worksheet.Cells[row, 6].Value?.ToString();
                    string? mineral1 = worksheet.Cells[row, 7].Value?.ToString();
                    string? mineral2 = worksheet.Cells[row, 8].Value?.ToString();

                    if (idCellValue != null && name != null && description != null && stiffnessCellValue != null && forgedTimesCellValue != null && durabilityCellValue != null && mineral1 != null && mineral2 != null)
                    {
                        string[] ids = idCellValue.Split(',');
                        int stiffness = int.Parse(stiffnessCellValue);
                        int forgedTimes = int.Parse(forgedTimesCellValue);
                        int durability = int.Parse(durabilityCellValue);

                        Weapon component = new Weapon(ids, name, description, stiffness, forgedTimes, mineral1, mineral2);
                        component.Durability = durability;

                        components.Add(component);
                    }
                }
            }
            return components;
        }

        private static Color ParseColor(string colorString)
        {
            string[] rgba = colorString.Replace("Color [", "").Replace("]", "").Split(',');
            double r = double.Parse(rgba[0]);
            double g = double.Parse(rgba[1]);
            double b = double.Parse(rgba[2]);
            double a = double.Parse(rgba[3]);
            return Color.RGBAColor(r, g, b, a);
        }
        private string ColorToString(Color color)
        {
            return $"Color [{color.R},{color.G},{color.B},{color.A}]";
        }
        private static List<Point2D> StringToPoints(string pointsString)
        {
            List<Point2D> points = new List<Point2D>();
            string[] pointsArray = pointsString.Split(';');
            foreach (string point in pointsArray)
            {
                string[] coords = point.Trim('(', ')').Split(',');
                double x = double.Parse(coords[0]);
                double y = double.Parse(coords[1]);
                points.Add(new Point2D() { X = x, Y = y });
            }
            return points;
        }
        private string PointsToString(List<Point2D> points)
        {
            return string.Join(";", points.Select(p => $"({p.X},{p.Y})"));
        }
        private int ToInt(string[] s)
        {
            int result = 0;
            foreach (string i in s)
            {   
                result = result * 10 + int.Parse(i);
            }
            return result;
        }
    }
}
