using Newtonsoft.Json;
using NopVarePreImport.DAL;
using NopVarePreImport.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmnishopToExcelImport
{
    public class PreImportConfig
    {
        private static NopVarePreImportDbContext context = new NopVarePreImportDbContext();
        private static string fsLargeConnectionString = @"Server=.;Database=4sound_umbraco;Trusted_Connection=True;";

        public static void SaveProductsMappingToDB()
        {
            //var productCategoriesWorkbook = ExcelProdvider.GetWorkbook("_data/nopVareMapping.xlsx");

            //var products = ExcelProdvider.GetExcelData(productCategoriesWorkbook, 1, 30, true);
            //File.WriteAllText($"_data/productsNopVare.json", JsonConvert.SerializeObject(products));
            var products = JsonConvert.DeserializeObject<List<List<string>>>(File.ReadAllText($"_data/productsNopVare.json"));

            var headers = ExcelProdvider.GetExcelHeaders(products);
            var descriptions = GetLongDescriptions();

            products.RemoveAt(0);

            int j = 0;
            foreach (var product in products)
            {
                Console.WriteLine(++j);

                if (!string.IsNullOrEmpty(product[headers["SupplierId"]])
                    && !string.IsNullOrEmpty(product[headers["ProductId"]])
                    && !string.IsNullOrEmpty(product[headers["SupplierName"]])
                    && !string.IsNullOrEmpty(product[headers["SupplierProductId"]]))
                {
                    var supplier = context.Suppliers.FirstOrDefault(s => s.SupplierId == product[headers["SupplierId"]]);
                    var isNew = supplier == null;

                    if (isNew)
                    {
                        supplier = new Supplier();

                        supplier.SupplierId = product[headers["SupplierId"]];
                        supplier.SupplierName = product[headers["SupplierName"]];

                        context.Suppliers.Add(supplier);
                        context.SaveChanges();
                    }

                    var newProduct = context.Products.FirstOrDefault(p => p.OmnishopProductId == product[headers["ProductId"]]);
                    isNew = newProduct == null;

                    if (isNew)
                    {
                        newProduct = new Product();

                        newProduct.OmnishopProductId = product[headers["ProductId"]];
                        context.Products.Add(newProduct);

                        var productSupplier = new ProductSupplier();

                        productSupplier.ProductId = newProduct.Id;
                        productSupplier.SupplierId = supplier.Id;
                        productSupplier.ProductSupplierId = product[headers["SupplierProductId"]];

                        context.ProductSuppliers.Add(productSupplier);

                        var uProduct = new UProduct();

                        uProduct.ProductId = newProduct.Id;

                        uProduct.UProductId = product[headers["ProductId2"]];
                        uProduct.PimSKU = product[headers["PimSKU"]];
                        uProduct.ContainerModel = product[headers["ContainerModel"]];
                        uProduct.VariantModel = product[headers["VariantModel"]];
                        uProduct.FullModel = product[headers["FullModel"]];
                        uProduct.DisplayName = product[headers["DisplayName"]];
                        uProduct.ShortDescription = product[headers["ShortDescription"]];
                        uProduct.LongDescription = descriptions.FirstOrDefault(d => d[0].Trim() == uProduct.UProductId.Trim())?[1] ?? "";

                        uProduct.SuggestedRetailPriceIncVat = double.TryParse(product[headers["SuggestedRetailPriceIncVat"]], out double suggestedRetailPriceIncVat) ? suggestedRetailPriceIncVat : 0;
                        uProduct.Price = double.TryParse(product[headers["Price"]], out double price) ? price : 0;

                        uProduct.Currency = product[headers["Currency"]];
                        uProduct.VAT = int.TryParse(product[headers["VAT"]], out int vat) ? vat : 0;

                        uProduct.VendorProductId = product[headers["VendorProductId"]];
                        uProduct.ManufacturerProductId = product[headers["ManufacturerProductId"]];

                        uProduct.PriceSuggestion = double.TryParse(product[headers["PriceSuggestion"]], out double priceSuggestion) ? priceSuggestion : 0;
                        uProduct.ProductStatusName = product[headers["ProductStatusName"]];

                        uProduct.PackedWeight = double.TryParse(product[headers["PackedWeight"]], out double packedWeight) ? packedWeight : 0;
                        uProduct.ModifiedOnUTC = DateTime.TryParse(product[headers["ModifiedOn"]], out DateTime modifiedOn) ? (Nullable<DateTime>)modifiedOn : null;

                        context.UProducts.Add(uProduct);
                        context.SaveChanges();
                    }
                }
            }
        }

        public static void SaveCategoriesToDB()
        {
            var productCategoriesWorkbook = ExcelProdvider.GetWorkbook("_data/nopVareMapping.xlsx");

            var categoryNodes = ExcelProdvider.GetExcelData(productCategoriesWorkbook, 3, 10, false);
            var cnHeaders = ExcelProdvider.GetExcelHeaders(categoryNodes);

            var flCategoryNodes = categoryNodes.Where(n => n[cnHeaders["treeLevel"]] == "1").ToList();
            var slCategoryNodes = categoryNodes.Where(n => n[cnHeaders["treeLevel"]] == "2").ToList();
            var tlCategoryNodes = categoryNodes.Where(n => n[cnHeaders["treeLevel"]] == "3").ToList();

            var fullCategoriesList = new List<List<List<string>>>() { flCategoryNodes, slCategoryNodes, tlCategoryNodes };

            Console.WriteLine("Excel data loaded!");

            foreach (var categoryNodeList in fullCategoriesList)
            {
                foreach (var categoryNode in categoryNodeList)
                {
                    if (!context.Categories.Any(cn => cn.OmnishopCategoryId == categoryNode[cnHeaders["Omnishop ProductCategoryId"]]))
                    {
                        var category = new Category();

                        category.NodeId = categoryNode[cnHeaders["nodeId"]];
                        category.OmnishopCategoryId = categoryNode[cnHeaders["Omnishop ProductCategoryId"]];
                        category.Name = categoryNode[cnHeaders["Omnishop Name"]];
                        category.MenuTitle = categoryNode[cnHeaders["menuTitle"]];
                        category.SortOrder = int.TryParse(categoryNode[cnHeaders["sortOrder"]], out int order) ? order : 0;
                        category.Level = int.TryParse(categoryNode[cnHeaders["treeLevel"]], out int level) ? level : 0;

                        category.ParentCategoryId = context.Categories.FirstOrDefault(c => c.NodeId == categoryNode[cnHeaders["parentNodeId"]])?.Id ?? null;

                        context.Categories.Add(category);
                        context.SaveChanges();
                    }
                }
            }
        }

        public static void SaveCategoriesMappingFromProductsListToDB()
        {
            var productCategoriesWorkbook = ExcelProdvider.GetWorkbook("_data/nopVareMapping.xlsx");

            var excelProducts = ExcelProdvider.GetExcelData(productCategoriesWorkbook, 1, 30, true);
            var headers = ExcelProdvider.GetExcelHeaders(excelProducts);

            excelProducts.RemoveAt(0);

            Console.WriteLine("Excel data loaded!");

            foreach (var product in context.Products)
            {
                Console.WriteLine(product.Id);

                var excelProduct = excelProducts.FirstOrDefault(ep => ep[headers["ProductId"]] == product.OmnishopProductId);
                
                if (excelProduct != null)
                {
                    var category = context.Categories.FirstOrDefault(c => c.OmnishopCategoryId == excelProduct[headers["CategoryId"]]);

                    if (category != null && !context.ProductCategories.Any(pc => pc.ProductId == product.Id && pc.CategoryId == category.Id))
                    {
                        var newProductCategory = new ProductCategory();

                        newProductCategory.ProductId = product.Id;
                        newProductCategory.CategoryId = category.Id;

                        context.ProductCategories.Add(newProductCategory);
                        context.SaveChanges();
                    }
                }
            }
        }

        public static void SaveCategoriesMappingFromProductCategoriesListToDB()
        {
            var productCategoriesWorkbook = ExcelProdvider.GetWorkbook("_data/nopVareMapping.xlsx");

            var productCategories = ExcelProdvider.GetExcelData(productCategoriesWorkbook, 2, 2, false);
            var categoryNodes = ExcelProdvider.GetExcelData(productCategoriesWorkbook, 3, 10, false);

            var pcHeaders = ExcelProdvider.GetExcelHeaders(productCategories);
            var cnHeaders = ExcelProdvider.GetExcelHeaders(categoryNodes);

            var tlCategoryNodes = categoryNodes.Where(n => n[cnHeaders["treeLevel"]] == "3").ToList();
            var folCategoryNodes = categoryNodes.Where(n => n[cnHeaders["treeLevel"]] == "4").ToList();

            Console.WriteLine("Excel data loaded!");

            productCategories.RemoveAt(0);
            categoryNodes.RemoveAt(0);

            foreach (var product in context.Products)
            {
                Console.WriteLine(product.Id);

                foreach (var productCategory in productCategories.Where(pc => pc[pcHeaders["ProductId"]] == product.OmnishopProductId))
                {
                    var categoryNodeId = folCategoryNodes.FirstOrDefault(foln => foln[cnHeaders["nodeId"]] == productCategory[pcHeaders["CategoryNodeId"]])?[cnHeaders["parentNodeId"]] ??
                        (tlCategoryNodes.FirstOrDefault(tln => tln[cnHeaders["nodeId"]] == productCategory[pcHeaders["CategoryNodeId"]])?[cnHeaders["parentNodeId"]] ?? null);

                    if (categoryNodeId != null)
                    {
                        var categoryNode = context.Categories.FirstOrDefault(c => c.NodeId == categoryNodeId);

                        if (categoryNode != null && !context.ProductCategories.Any(pc => pc.ProductId == product.Id && pc.CategoryId == categoryNode.Id))
                        {
                            var newProductCategory = new ProductCategory();

                            newProductCategory.ProductId = product.Id;
                            newProductCategory.CategoryId = categoryNode.Id;

                            context.ProductCategories.Add(newProductCategory);
                            context.SaveChanges();
                        }
                    }
                }
            }

            Console.WriteLine("Product categories checked!");
        }

        public static void SetCategoryPaths()
        {
            foreach(var category in context.Categories)
            {
                var path = category.Name;

                var parent = category.ParentCategoryId != null ? context.Categories.FirstOrDefault(c => c.Id == category.ParentCategoryId) : null;

                while (parent != null)
                {
                    path = parent.Name + " >> " + path;
                    parent = parent.ParentCategoryId != null ? context.Categories.FirstOrDefault(c => c.Id == parent.ParentCategoryId) : null;
                }

                category.Path = path;
                context.Categories.Update(category);
            }

            context.SaveChanges();
        }

        //private static List<string[]> GetProductAttributes(int productId)
        //{
        //    string queryString = "SELECT fs.ProductId, fs.PimSKU, pd.ShortDescription, pd.LongDescriptionHtml "
        //                    + "FROM[4sound_umbraco].[dbo].[PIM_ProductQuery] fs "
        //                    + "join[uCommerce_Product] p on fs.ContainerSku = p.Sku "
        //                    + "join[uCommerce_ProductDescription] pd on pd.ProductId = p.ProductId "
        //                    + "where p.VariantSku is null and fs.Market = '4sound.no' and pd.CultureCode = 'nb-NO'";

        //}

        private static List<string[]> GetLongDescriptions()
        {
            List<string[]> descriptions = new List<string[]>();

            string queryString = "SELECT fs.ProductId, fs.PimSKU, pd.ShortDescription, pd.LongDescriptionHtml "
                            + "FROM[4sound_umbraco].[dbo].[PIM_ProductQuery] fs "
                            + "join[uCommerce_Product] p on fs.ContainerSku = p.Sku "
                            + "join[uCommerce_ProductDescription] pd on pd.ProductId = p.ProductId "
                            + "where p.VariantSku is null and fs.Market = '4sound.no' and pd.CultureCode = 'nb-NO'";

            using (SqlConnection connection = new SqlConnection(fsLargeConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    descriptions.Add(new string[] { reader[0].ToString(), reader[3].ToString() });
                }

                reader.Close();
                connection.Close();
            }

            return descriptions;
        }

        private static string GetLongDescription(string uProductId)
        {
            string description = "";

            string queryString = "SELECT fs.ProductId, fs.PimSKU, pd.ShortDescription, pd.LongDescriptionHtml "
                            + "FROM[4sound_umbraco].[dbo].[PIM_ProductQuery] fs "
                            + "join[uCommerce_Product] p on fs.ContainerSku = p.Sku "
                            + "join[uCommerce_ProductDescription] pd on pd.ProductId = p.ProductId "
                            + "where fs.ProductId = " + uProductId + " and p.VariantSku is null and fs.Market = '4sound.no' and pd.CultureCode = 'nb-NO'";

            using (SqlConnection connection = new SqlConnection(fsLargeConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    description = reader[3].ToString();
                }

                reader.Close();
                connection.Close();
            }

            return description;
        }
    }
}
