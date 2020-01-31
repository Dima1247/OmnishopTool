using ClosedXML.Excel;
using FourSoundCom.DAL;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OmnishopFourSound.DAL;
using OmnishopFourSound.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmnishopToExcelImport
{
    public static class Common
    {
        private static OmnishopFSContext context = new OmnishopFSContext();
        private static FourSoundDbContext oldContext = new FourSoundDbContext();
        private static string fsLargeConnectionString = @"Server=.;Database=4sound_umbraco;Trusted_Connection=True;";

        public static void SaveCategoriesToDB()
        {
            var categoriesWorkbook = ExcelProdvider.GetWorkbook("_data/fs_categories2.xlsx");

            var fl_categories = ExcelProdvider.GetExcelData(categoriesWorkbook, 4, 6);
            var sl_categories = ExcelProdvider.GetExcelData(categoriesWorkbook, 5, 6);
            var tl_categories = ExcelProdvider.GetExcelData(categoriesWorkbook, 6, 6);

            for (int i = 0; i < 3; i++)
            {
                var cat = fl_categories[i];

                var currentCat = context.Categories.FirstOrDefault(c => c.OmnishopCategoryId == int.Parse(cat[0]));

                if (currentCat != null)
                {
                    currentCat.Name = cat[2];
                    currentCat.OmnishopCategoryId = int.Parse(cat[0]);
                    currentCat.OmnishopParentCategoryId = 0;
                    currentCat.Level = 1;
                    currentCat.SortOrder = int.Parse(cat[4]);
                    currentCat.UrlPart = cat[5];

                    context.Update(currentCat);
                }
                else
                {
                    context.Add(new entOmnishopCategory()
                    {
                        Name = cat[2],
                        OmnishopCategoryId = int.Parse(cat[0]),
                        OmnishopParentCategoryId = 0,
                        Level = 1,
                        SortOrder = int.Parse(cat[4]),
                        UrlPart = cat[5]
                    });
                }
            }

            context.SaveChanges();

            for (int i = 0; i < sl_categories.Count; i++)
            {
                var cat = sl_categories[i];
                try
                {
                    var parentCat = context.Categories.FirstOrDefault(c => c.OmnishopCategoryId == int.Parse(cat[1]));
                    var currentCat = context.Categories.FirstOrDefault(c => c.OmnishopCategoryId == int.Parse(cat[0]));

                    if (parentCat != null)
                    {
                        if (currentCat != null)
                        {
                            currentCat.Name = cat[2];
                            currentCat.OmnishopCategoryId = int.Parse(cat[0]);
                            currentCat.OmnishopParentCategoryId = parentCat.OmnishopCategoryId;
                            currentCat.ParentCategoryId = parentCat.Id;
                            currentCat.Level = 2;
                            currentCat.SortOrder = int.Parse(cat[4]);
                            currentCat.UrlPart = cat[5];

                            context.Update(currentCat);
                        }
                        else
                        {
                            context.Add(new entOmnishopCategory()
                            {
                                Name = cat[2],
                                OmnishopCategoryId = int.Parse(cat[0]),
                                OmnishopParentCategoryId = parentCat.OmnishopCategoryId,
                                ParentCategoryId = parentCat.Id,
                                Level = 2,
                                SortOrder = int.Parse(cat[4]),
                                UrlPart = cat[5]
                            });
                        }
                    }
                }
                catch
                {
                    Console.WriteLine(cat[2] + " wasn` t added!");
                }
            }

            context.SaveChanges();

            for (int i = 0; i < tl_categories.Count; i++)
            {
                var cat = tl_categories[i];
                try
                {
                    var parentCat = context.Categories.FirstOrDefault(c => c.OmnishopCategoryId == int.Parse(cat[1]));
                    var currentCat = context.Categories.FirstOrDefault(c => c.OmnishopCategoryId == int.Parse(cat[0]));

                    if (parentCat != null)
                    {
                        if (currentCat != null)
                        {
                            currentCat.Name = cat[2];
                            currentCat.OmnishopCategoryId = int.Parse(cat[0]);
                            currentCat.OmnishopParentCategoryId = parentCat.OmnishopCategoryId;
                            currentCat.ParentCategoryId = parentCat.Id;
                            currentCat.Level = 3;
                            currentCat.SortOrder = int.Parse(cat[4]);
                            currentCat.UrlPart = cat[5];

                            context.Update(currentCat);
                        }
                        else
                        {
                            context.Add(new entOmnishopCategory()
                            {
                                Name = cat[2],
                                OmnishopCategoryId = int.Parse(cat[0]),
                                OmnishopParentCategoryId = parentCat.OmnishopCategoryId,
                                ParentCategoryId = parentCat.Id,
                                Level = 3,
                                SortOrder = int.Parse(cat[4]),
                                UrlPart = cat[5]
                            });
                        }
                    }
                }
                catch
                {
                    Console.WriteLine(cat[2] + " wasn` t added!");
                }
            }

            context.SaveChanges();
        }
        public static void SaveBrandsToDB()
        {
            var productsWorkbook = ExcelProdvider.GetWorkbook("_data/fs_products.xlsx");

            var brands = ExcelProdvider.GetExcelData(productsWorkbook, 2, 4);

            for (int i = 0; i < brands.Count; i++)
            {
                var cat = brands[i];

                context.Add(new entOmnishopBrand() { Name = cat[3], OmnishopBrandId = int.Parse(cat[0]), UrlPart = cat[3].ToLower() });
            }

            context.SaveChanges();
        }
        public static void SaveProductsToDB(string productsWbName, string categoriesWbName)
        {
            //XLWorkbook productsWorkbook = null, categoriesWorkbook = null;

            //if (!string.IsNullOrEmpty(productsWbName))
            //    productsWorkbook = ExcelProdvider.GetWorkbook($"_data/{productsWbName}.xlsx");
            //if (!string.IsNullOrEmpty(categoriesWbName))
            //    categoriesWorkbook = ExcelProdvider.GetWorkbook($"_data/{categoriesWbName}.xlsx");

            //var allProducts = ExcelProdvider.GetExcelData(productsWorkbook, 1, 11);
            //var productsMap1 = ExcelProdvider.GetExcelData(productsWorkbook, 3, 5);
            //var productsMap2 = ExcelProdvider.GetExcelData(productsWorkbook, 4, 5);
            //var catMap = ExcelProdvider.GetExcelData(categoriesWorkbook, 1, 2);
            //var cat4 = ExcelProdvider.GetExcelData(categoriesWorkbook, 6, 2);
            //var cat3 = ExcelProdvider.GetExcelData(categoriesWorkbook, 5, 2);

            //var genList = new List<List<string>>();

            //for (int i = 0; i < productsMap1.Count; i++)
            //{
            //    if (!genList.Any(el => el[1] == productsMap1[i][1]))
            //        genList.Add(productsMap1[i]);
            //}

            //for (int i = 0; i < productsMap2.Count; i++)
            //{
            //    if (!genList.Any(el => el[1] == productsMap2[i][1]))
            //        genList.Add(productsMap2[i]);
            //}

            //string connectionString = @"Server=.;Database=4sound_umbraco;Trusted_Connection=True;";

            //string queryString = "SELECT fs.ProductId, fs.PimSKU, pd.ShortDescription, pd.LongDescriptionHtml "
            //    + "FROM[4sound_umbraco].[dbo].[PIM_ProductQuery] fs "
            //    + "join[uCommerce_Product] p on fs.ContainerSku = p.Sku "
            //    + "join[uCommerce_ProductDescription] pd on pd.ProductId = p.ProductId "
            //    + "where fs.ProductId IN (" + String.Join(", ", genList.Select(el => el[1]).ToArray()) + ") and p.VariantSku is null and fs.Market = '4sound.no' and pd.CultureCode = 'nb-NO'";

            //var list = new List<entOmnishopProduct>();
            //var productsFull = oldContext.Products.Include(p => p.Pictures).Select(p => new { Pictures = p.Pictures, Sku = JsonConvert.DeserializeObject<ProductFromSite>(p.JsonSource).ProductVariantSku });

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(queryString, connection);

            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();
            //    int i = 0, j = 0;
            //    while (reader.Read())
            //    {
            //        if (!list.Any(el => el.UComProductId.ToString() == reader[0].ToString()))
            //        {
            //            var prodFromFScom = productsFull.FirstOrDefault(p => reader[1].ToString() == p.Sku);

            //            if (prodFromFScom != null)
            //            {
            //                j++;
            //            }

            //            var product = new entOmnishopProduct()
            //            {
            //                UComProductId = int.Parse(reader[0].ToString()),
            //                OmnishopProductId = genList.FirstOrDefault(el => el[1] == reader[0].ToString())[0],
            //                ShortDescription = reader[2].ToString(),
            //                LongDescription = reader[3].ToString(),
            //                Published = true,
            //                Picture1 = prodFromFScom != null && prodFromFScom.Pictures != null && prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("0")) != null ? prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("0")).Name : null,
            //                Picture2 = prodFromFScom != null && prodFromFScom.Pictures != null && prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("1")) != null ? prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("1")).Name : null,
            //                Picture3 = prodFromFScom != null && prodFromFScom.Pictures != null && prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("2")) != null ? prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("2")).Name : null,
            //            };
            //            i++;
            //            Console.WriteLine("product " + i + " | j = " + j);

            //            list.Add(product);
            //        }
            //    }
            //    reader.Close();
            //    connection.Close();
            //}

            //File.WriteAllText($"_data/dd.json", JsonConvert.SerializeObject(list));

            //var withImgs = list.Where(l => !string.IsNullOrEmpty(l.Picture1)).ToList();

            //int i = 0; 
            //foreach (var product in list)
            //{ i++;
            //    var prodFromExcel = allProducts.FirstOrDefault(ep => ep[0] == product.OmnishopProductId);
            //    product.Vat = int.Parse(prodFromExcel[9].Split('.')[0]);
            //    product.Price = Math.Round(double.Parse(prodFromExcel[2]) + (double.Parse(prodFromExcel[2]) / 100 * product.Vat), 2);
            //    product.Cost = Math.Round(double.Parse(prodFromExcel[2]), 2);
            //    product.Name = prodFromExcel[1];
            //    var brand = context.Brands.FirstOrDefault(b => prodFromExcel[5] != "NULL" && b.OmnishopBrandId == int.Parse(prodFromExcel[5]));
            //    if (brand != null)
            //        product.OmnishopBrandId = brand.Id;

            //    Console.WriteLine("product " + i);
            //}


            //var list = new List<entOmnishopProduct>();
            //list = JsonConvert.DeserializeObject<List<entOmnishopProduct>>(File.ReadAllText("_data/dd2.json"));

            //foreach (var prod in list)
            //{
            //    if (prod.OmnishopBrandId == 0)
            //    {
            //        prod.OmnishopBrandId = null;
            //    }
            //    context.Add(prod);
            //}
            //int j = 0;
            //Console.WriteLine(genList.Count);
            //foreach (var el in genList)
            //{
            //    var prod = context.Products.FirstOrDefault(p => p.UComProductId.ToString() == el[1].ToString());
            //    if (prod != null)
            //        prod.OmnishopProductId = el[0];
            //    j++;
            //    Console.WriteLine(j);
            //}
            //context.SaveChanges();

            //var catProdList = new List<entOmnishopProductCategory>();
            //int j = 0;
            //foreach (var l in context.Products)
            //{
            //    j++;
            //    foreach (var cat in catMap)
            //    {
            //        if (cat[1].ToString() == l.UComProductId.ToString())
            //        {
            //            var category = context.Categories.FirstOrDefault(c => c.OmnishopCategoryId.ToString() == cat[0].ToString());
            //            if (category != null)
            //            {
            //                catProdList.Add(new entOmnishopProductCategory() { ProductId = l.OmnishopProductId, CategoryId = cat[0].ToString(), OmnishopCategoryId = category.Id, OmnishopProductId = l.Id });
            //            }
            //        }
            //    }
            //    Console.WriteLine("product " + j);
            //}

            //foreach(var cat in catProdList)
            //{
            //    context.ProductCategories.Add(cat);
            //}
            //context.SaveChanges();

            //int i = 0;
            //var common = list.Where(l =>
            //{
            //    i++;
            //    Console.WriteLine(i);
            //    foreach (var cat in catMap)
            //    {
            //        if (cat[1].ToString() == l.UComProductId.ToString())
            //        {
            //            var category = context.Categories.FirstOrDefault(c => c.OmnishopCategoryId.ToString() == cat[0].ToString());
            //            if (category != null)
            //                return true;
            //        }
            //    }
            //    return false;
            //}).ToList();

            //File.WriteAllText($"_data/dd2.json", JsonConvert.SerializeObject(list));
            //var withbrands = list.Where(l => l.OmnishopBrandId != 0).ToList();

            //var withShortdesc = list.Where(el => !string.IsNullOrEmpty(el[3])).ToList();
            //var withLargedesc = list.Where(el => !string.IsNullOrEmpty(el[4])).ToList();

            //var list = new List<entOmnishopProduct>();
            //list = JsonConvert.DeserializeObject<List<entOmnishopProduct>>(File.ReadAllText("_data/dd3.json"));

            //foreach(var l in list)
            //{
            //    context.Products.Add(l);
            //}
            //context.SaveChanges();

            //var newList = new List<entOmnishopProduct>();
            //int i = 0;

            //foreach (var prodFromExcel in allProducts)
            //{
            //    if (!context.Products.Any(p => p.OmnishopProductId == prodFromExcel[0].ToString()))
            //    {
            //        var product = new entOmnishopProduct();
            //        product.OmnishopProductId = prodFromExcel[0];
            //        product.Published = false;
            //        product.Vat = int.Parse(prodFromExcel[9].Split('.')[0]);
            //        product.Price = Math.Round(double.Parse(prodFromExcel[2]) + (double.Parse(prodFromExcel[2]) / 100 * product.Vat), 2);
            //        product.Cost = Math.Round(double.Parse(prodFromExcel[2]), 2);
            //        product.Name = prodFromExcel[1];
            //        var brand = context.Brands.FirstOrDefault(b => prodFromExcel[5] != "NULL" && b.OmnishopBrandId == int.Parse(prodFromExcel[5]));
            //        if (brand != null)
            //            product.OmnishopBrandId = brand.Id;
            //        newList.Add(product);

            //        i++;
            //        Console.WriteLine("product " + i);
            //    }
            //}

            //File.WriteAllText($"_data/dd3.json", JsonConvert.SerializeObject(newList));
            //context.SaveChanges();

            //foreach (var cat in catMap)
            //{
            //    var cat4Cat = cat4.FirstOrDefault(c => c[0] == cat[0]);

            //    if (cat4Cat != null)
            //    {
            //        cat[0] = cat4Cat[1];
            //    }
            //}

            //var catProdList = new List<entOmnishopProductCategory>();
            //int j = 0;
            //foreach (var l in context.Products)
            //{
            //    if (l.UComProductId != 0)
            //    {
            //        j++;
            //        foreach (var cat in catMap)
            //        {
            //            if (cat[1].ToString() == l.UComProductId.ToString())
            //            {
            //                var category = context.Categories.FirstOrDefault(c => c.OmnishopCategoryId.ToString() == cat[0].ToString());
            //                if (category != null)
            //                {
            //                    catProdList.Add(new entOmnishopProductCategory() { ProductId = l.OmnishopProductId, CategoryId = cat[0].ToString(), OmnishopCategoryId = category.Id, OmnishopProductId = l.Id });
            //                }
            //            }
            //        }
            //        Console.WriteLine("product " + j);
            //    }
            //}

            //File.WriteAllText($"_data/cp1.json", JsonConvert.SerializeObject(catProdList));


            //var list = JsonConvert.DeserializeObject<List<entOmnishopProductCategory>>(File.ReadAllText("_data/cp1.json"));
            //foreach (var l in list)
            //{
            //    context.ProductCategories.Add(l);
            //}

            //context.SaveChanges();

            //var lin3 = list.Where(l => cat3.Any(c => c[0] == l.CategoryId)).ToList();
            //var lin4 = list.Where(l => cat4.Any(c => c[0] == l.CategoryId)).ToList();

            //var listToCorrect = new List<entOmnishopProduct>();
            //foreach(var prod in context.Products)
            //{
            //    if (prod.Name.StartsWith(","))
            //    {
            //        prod.Name = prod.Name.Remove(0, 1);
            //        prod.Name = prod.Name.Trim();

            //        listToCorrect.Add(prod);
            //    }

            //    if (prod.Name.EndsWith(","))
            //    {
            //        prod.Name = prod.Name.Remove(prod.Name.Length-1, 1);
            //        prod.Name = prod.Name.Trim();

            //        listToCorrect.Add(prod);
            //    }
            //}

            //foreach (var prod in context.Products)
            //{
            //    var whiteSpaceStr = "  ";

            //    for (int i = 0; i < 20; i++)
            //    {
            //        whiteSpaceStr += " ";
            //    }

            //    for (int i = 0; i < 20; i++)
            //    {
            //        if (prod.Name.Contains(whiteSpaceStr))
            //        {
            //            prod.Name = prod.Name.Replace(whiteSpaceStr, " ");

            //            listToCorrect.Add(prod);
            //        }

            //        whiteSpaceStr = whiteSpaceStr.Remove(0, 1);
            //    }
            //}

            //foreach (var prod in context.Products)
            //{
            //    if (prod.Name.Contains(" ,"))
            //    {
            //        prod.Name = prod.Name.Replace(" ,", ",");

            //        listToCorrect.Add(prod);
            //    }
            //}

            //foreach (var prod in listToCorrect)
            //{
            //    var product = context.Products.FirstOrDefault(p => p.Id == prod.Id);
            //    product.Name = prod.Name;
            //}

            //context.SaveChanges();
        }
        public static void SaveBrandsToDB2()
        {
            var productsWorkbook = ExcelProdvider.GetWorkbook("_data/fs_products2.xlsx");

            var brands = ExcelProdvider.GetExcelData(productsWorkbook, 3, 2);

            for (int i = 0; i < brands.Count; i++)
            {
                var cat = brands[i];

                if (!context.Brands.Any(b => b.OmnishopBrandId == int.Parse(cat[0])))
                    context.Add(new entOmnishopBrand() { Name = cat[1], OmnishopBrandId = int.Parse(cat[0]) });
            }

            context.SaveChanges();
        }
        public static void SaveProductsToDB2(string productsWbName, string categoriesWbName)
        {
            XLWorkbook productsWorkbook = null, categoriesWorkbook = null;

            if (!string.IsNullOrEmpty(productsWbName))
                productsWorkbook = ExcelProdvider.GetWorkbook($"_data/{productsWbName}.xlsx");
            if (!string.IsNullOrEmpty(categoriesWbName))
                categoriesWorkbook = ExcelProdvider.GetWorkbook($"_data/{categoriesWbName}.xlsx");

            //var allProducts = ExcelProdvider.GetExcelData(productsWorkbook, 1, 11);

            //var catMap = ExcelProdvider.GetExcelData(categoriesWorkbook, 8, 2);
            //var cat4 = ExcelProdvider.GetExcelData(categoriesWorkbook, 7, 2);
            //var cat3 = ExcelProdvider.GetExcelData(categoriesWorkbook, 6, 2);
            //var cat2 = ExcelProdvider.GetExcelData(categoriesWorkbook, 5, 2);

            //string connectionString = @"Server=.;Database=4sound_umbraco;Trusted_Connection=True;";

            //string queryString = "SELECT fs.ProductId, fs.PimSKU, pd.ShortDescription, pd.LongDescriptionHtml "
            //    + "FROM[4sound_umbraco].[dbo].[PIM_ProductQuery] fs "
            //    + "join[uCommerce_Product] p on fs.ContainerSku = p.Sku "
            //    + "join[uCommerce_ProductDescription] pd on pd.ProductId = p.ProductId "
            //    + "where fs.ProductId IN (" + String.Join(", ", allProducts.Where(el => !string.IsNullOrEmpty(el[8])).Select(el => el[8]).ToArray()) + ") and p.VariantSku is null and fs.Market = '4sound.no' and pd.CultureCode = 'nb-NO'";

            //string queryString2 = "SELECT fs.ProductId, fs.PimSKU, pd.ShortDescription, pd.LongDescriptionHtml, p.ParentProductId "
            //    + "FROM[4sound_umbraco].[dbo].[PIM_ProductQuery] fs "
            //    + "join[uCommerce_Product] p on fs.ContainerSku = p.Sku "
            //    + "join[uCommerce_ProductDescription] pd on pd.ProductId = p.ProductId "
            //    + "where p.ParentProductId in (" + String.Join(", ", allProducts.Skip(allProducts.Count / 2).Where(el => !string.IsNullOrEmpty(el[9])).Select(el => el[9]).ToArray()) + ") and fs.Market = '4sound.no' and pd.CultureCode = 'nb-NO'";

            //Console.WriteLine(allProducts.Take(allProducts.Count).Where(el => !string.IsNullOrEmpty(el[8])).Count());
            //Console.WriteLine(allProducts.Take(allProducts.Count).Where(el => !string.IsNullOrEmpty(el[9])).Count());

            //var list = new List<entOmnishopProduct>();
            //var productsFull = oldContext.Products.Include(p => p.Pictures).Select(p => new { Pictures = p.Pictures, Sku = JsonConvert.DeserializeObject<ProductFromSite>(p.JsonSource).ProductVariantSku });

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(queryString, connection);

            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();
            //    int i = 0, j = 0;
            //    while (reader.Read())
            //    {
            //        if (!list.Any(el => el.UComProductId.ToString() == reader[0].ToString()))
            //        {
            //            var prodFromFScom = productsFull.FirstOrDefault(p => reader[1].ToString() == p.Sku);

            //            if (prodFromFScom != null)
            //            {
            //                j++;
            //            }

            //            var product = new entOmnishopProduct()
            //            {
            //                UComProductId = int.Parse(reader[0].ToString()),
            //                OmnishopProductId = allProducts.FirstOrDefault(el => el[8] == reader[0].ToString())[0],
            //                ShortDescription = reader[2].ToString(),
            //                LongDescription = reader[3].ToString(),
            //                Published = true,
            //                Picture1 = prodFromFScom != null && prodFromFScom.Pictures != null && prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("0")) != null ? prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("0")).Name : null,
            //                Picture2 = prodFromFScom != null && prodFromFScom.Pictures != null && prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("1")) != null ? prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("1")).Name : null,
            //                Picture3 = prodFromFScom != null && prodFromFScom.Pictures != null && prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("2")) != null ? prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("2")).Name : null,
            //            };
            //            i++;
            //            Console.WriteLine("product " + i + " | j = " + j);

            //            list.Add(product);
            //        }
            //    }

            //    var newProds = list.Where(el => !context.Products.Any(p => p.UComProductId == el.UComProductId)).ToList();
            //    var newOmnProds = list.Where(el => !context.Products.Any(p => p.OmnishopProductId == el.OmnishopProductId)).ToList();

            //    reader.Close();

            //    SqlCommand command2 = new SqlCommand(queryString2, connection);
            //    connection.Open();

            //    SqlDataReader reader2 = command2.ExecuteReader();
            //    int i = 0; int j = 0;
            //    while (reader2.Read())
            //    {
            //        if (!list.Any(el => el.UComProductId.ToString() == reader2[0].ToString()))
            //        {
            //            //var prodFromFScom = productsFull.FirstOrDefault(p => reader[1].ToString() == p.Sku);

            //            //if (prodFromFScom != null)
            //            //{
            //            //    j++;
            //            //}

            //            var product = new entOmnishopProduct()
            //            {
            //                UComProductId = int.Parse(reader2[0].ToString()),
            //                OmnishopProductId = allProducts.FirstOrDefault(el => el[9] == reader2[4].ToString())[0],
            //                ShortDescription = reader2[2].ToString(),
            //                LongDescription = reader2[3].ToString(),
            //                Published = true
            //                //Picture1 = prodFromFScom != null && prodFromFScom.Pictures != null && prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("0")) != null ? prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("0")).Name : null,
            //                //Picture2 = prodFromFScom != null && prodFromFScom.Pictures != null && prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("1")) != null ? prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("1")).Name : null,
            //                //Picture3 = prodFromFScom != null && prodFromFScom.Pictures != null && prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("2")) != null ? prodFromFScom.Pictures.FirstOrDefault(p => p.Name.EndsWith("2")).Name : null,
            //            };
            //            i++;
            //            Console.WriteLine("product " + i + " | j = " + j);

            //            list.Add(product);
            //        }
            //        else
            //        {
            //            Console.WriteLine("wasn`t found product " + i + " | j = " + j);

            //        }
            //    }

            //    connection.Close();
            //}

            //File.WriteAllText($"_data/_differentJsons/fs2_2_half_parentProductId.json", JsonConvert.SerializeObject(list));

            //var list1 = JsonConvert.DeserializeObject<List<entOmnishopProduct>>(File.ReadAllText($"_data/_differentJsons/fs2_1_half_parentProductId.json"));
            //var list2 = JsonConvert.DeserializeObject<List<entOmnishopProduct>>(File.ReadAllText($"_data/_differentJsons/fs2_2_half_parentProductId.json"));
            //var list3 = list1;

            //foreach (var l in list2)
            //{
            //    if (!list3.Any(le => le.OmnishopProductId == l.OmnishopProductId))
            //        list3.Add(l);
            //}

            //var list = JsonConvert.DeserializeObject<List<entOmnishopProduct>>(File.ReadAllText($"_data/_differentJsons/fs2_new_parentProductId.json"));

            //var listz = JsonConvert.DeserializeObject<List<entOmnishopProduct>>(File.ReadAllText($"_data/_differentJsons/fs2_common_parentProductId.json"));

            //string connectionString = @"Server=.;Database=4sound_umbraco;Trusted_Connection=True;";

            //string queryString = "SELECT fs.ProductId, fs.PimSKU, pd.ShortDescription, pd.LongDescriptionHtml "
            //    + "FROM[4sound_umbraco].[dbo].[PIM_ProductQuery] fs "
            //    + "join[uCommerce_Product] p on fs.ContainerSku = p.Sku "
            //    + "join[uCommerce_ProductDescription] pd on pd.ProductId = p.ProductId "
            //    + "where fs.ProductId IN (" + String.Join(", ", listz.Select(el => el.UComProductId).ToArray()) + ") and p.VariantSku is null and fs.Market = '4sound.no' and pd.CultureCode = 'nb-NO'";

            //var list = new List<entOmnishopProduct>();
            //var productsFull = oldContext.Products.Include(p => p.Pictures).Select(p => new { Pictures = p.Pictures, Sku = JsonConvert.DeserializeObject<ProductFromSite>(p.JsonSource).ProductVariantSku });

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(queryString, connection);

            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();
            //    int i = 0, j = 0;
            //    while (reader.Read())
            //    {
            //        if (!list.Any(el => el.UComProductId.ToString() == reader[0].ToString()))
            //        {
            //            listz.FirstOrDefault(l => l.UComProductId == int.Parse(reader[0].ToString())).SKU = reader[1].ToString();
            //            i++;
            //            Console.WriteLine("product " + i + " | j = " + j);
            //        }
            //    }

            //    i = 0;

            //    foreach (var prod in productsFull)
            //    {
            //        var el = listz.FirstOrDefault(l => l.SKU == prod.Sku);

            //        if (el != null)
            //        {
            //            el.Picture1 = prod != null && prod.Pictures != null && prod.Pictures.FirstOrDefault(p => p.Name.EndsWith("0")) != null ? prod.Pictures.FirstOrDefault(p => p.Name.EndsWith("0")).Name : null;
            //            el.Picture2 = prod != null && prod.Pictures != null && prod.Pictures.FirstOrDefault(p => p.Name.EndsWith("1")) != null ? prod.Pictures.FirstOrDefault(p => p.Name.EndsWith("1")).Name : null;
            //            el.Picture3 = prod != null && prod.Pictures != null && prod.Pictures.FirstOrDefault(p => p.Name.EndsWith("2")) != null ? prod.Pictures.FirstOrDefault(p => p.Name.EndsWith("2")).Name : null;
            //        }

            //        Console.WriteLine("product " + ++i + " | j = " + j);
            //    }

            //    File.WriteAllText($"_data/_differentJsons/fs2_new_withimages_parentProductId.json", JsonConvert.SerializeObject(listz));

            //    reader.Close();
            //    connection.Close();
            //}
            //var listz = JsonConvert.DeserializeObject<List<entOmnishopProduct>>(File.ReadAllText($"_data/_differentJsons/fs2_new_withimages_parentProductId.json"));

            //foreach (var el in listz)
            //{
            //    var prod = allProducts.FirstOrDefault(p => el.OmnishopProductId == p[0]);

            //    if (prod != null)
            //    {
            //        el.Name = prod[1];
            //        el.Price = double.Parse(prod[2]);
            //        el.Cost = el.Price;

            //        if (int.TryParse(prod[3], out int brandId))
            //            el.OmnishopBrandId = context.Brands.FirstOrDefault(b => b.OmnishopBrandId == brandId).Id;

            //        if (double.TryParse(prod[7], out double vatRate))
            //            el.Vat = int.Parse(vatRate.ToString());
            //    }
            //}



            //foreach (var el in listz)
            //{
            //    var prod = allProducts.FirstOrDefault(p => el.OmnishopProductId == p[0]);

            //    if (prod != null)
            //    {
            //        if (int.TryParse(prod[5], out int status))
            //        {
            //            el.Published = status == 100;
            //        }
            //    }
            //}

            //File.WriteAllText($"_data/_differentJsons/fs2_catgories_last_2_parentProductId.json", JsonConvert.SerializeObject(listz));


            //foreach (var el in listz)
            //{
            //    var productFromDb = context.Products.FirstOrDefault(p => p.OmnishopProductId == el.OmnishopProductId);

            //    if (productFromDb == null)
            //    {
            //        context.Add(el);
            //    }
            //    else
            //    {
            //        productFromDb.Name = el.Name;
            //        productFromDb.ShortDescription = el.ShortDescription;
            //        productFromDb.LongDescription = el.LongDescription;
            //        productFromDb.Price = el.Price;
            //        productFromDb.Cost = el.Cost;
            //        productFromDb.Vat = el.Vat;
            //        productFromDb.Picture1 = el.Picture1;
            //        productFromDb.Picture2 = el.Picture2;
            //        productFromDb.Picture3 = el.Picture3;
            //        context.Update(productFromDb);
            //    }
            //}

            //var withUMBIds = listz.Where(l => context.Products.Any(p => p.UComProductId == l.UComProductId)).ToList();
            //var withOmnIds = listz.Where(l => context.Products.Any(p => p.OmnishopProductId == l.OmnishopProductId && p.UComProductId == l.UComProductId)).ToList();
            //var catMap = JsonConvert.DeserializeObject<List<List<string>>>(File.ReadAllText($"_data/_differentJsons/catMap.json"));
            //var listz = JsonConvert.DeserializeObject<List<entOmnishopProduct>>(File.ReadAllText($"_data/_differentJsons/fs2_catgories_last_2_parentProductId.json"));

            //Console.WriteLine("Write cat map"); 
            //File.WriteAllText($"_data/_differentJsons/catMap.json", JsonConvert.SerializeObject(catMap));

            //Console.WriteLine("Start product groups");
            //var gbUMBIds = listz.GroupBy(l => l.UComProductId).ToList();

            //var gbUMBIds2 = listz.GroupBy(l => l.OmnishopProductId).ToList();

            //var gbUMBIds2List = gbUMBIds2.Select(l => l.First()).ToList();

            //var sameGbUMB = gbUMBIds2.Where(g => g.Count() > 1).ToList();

            //var i = 0;
            //foreach(var gbUmb in gbUMBIds2)
            //{
            //    Console.WriteLine("Product:" + i++);
            //    var byOmn = gbUMBIds2List.FirstOrDefault(g => g.OmnishopProductId == gbUmb.First().OmnishopProductId);
            //    var catids = catMap.Where(cat => gbUmb.Any(g => g.UComProductId.ToString() == cat[1])).Select(c => c[0]).ToList();
            //    byOmn.CatIds = catids;
            //}

            //Console.WriteLine("now 4 level");
            //var listz2 = JsonConvert.DeserializeObject<List<entOmnishopProduct>>(File.ReadAllText($"_data/_differentJsons/fs2_prods_with_catIds.json"));

            //var withcats = listz2.Where(l => l.CatIds.Count > 0).ToList();
            //File.WriteAllText($"_data/_differentJsons/fs2_prods_with_catIds.json", JsonConvert.SerializeObject(gbUMBIds2List));

            //foreach (var prod in listz2)
            //{
            //    var newCats = new List<string>();
            //    for (int i = 0; i < prod.CatIds.Count; i++)
            //    {
            //        var cat = prod.CatIds[i];
            //        var catFromCat4 = cat4.FirstOrDefault(c => c[0] == cat);

            //        if (catFromCat4 != null)
            //        {
            //            newCats.Add(catFromCat4[1]);
            //        }
            //        else
            //        {
            //            newCats.Add(cat);
            //        }
            //    }

            //    prod.CatIds = newCats;
            //}

            //Console.WriteLine("now 3 level");

            //foreach (var prod in listz2)
            //{
            //    for (int i = 0; i < prod.CatIds.Count; i++)
            //    {
            //        var cat = prod.CatIds[i];
            //        var catFromCat3 = cat3.FirstOrDefault(c => c[0] == cat);

            //        if (catFromCat3 == null)
            //        {
            //            prod.CatIds.Remove(cat);
            //        }
            //    }
            //}

            //File.WriteAllText($"_data/_differentJsons/fs2_prods_with_catIds_3Level.json", JsonConvert.SerializeObject(listz2));
            //var listz2 = JsonConvert.DeserializeObject<List<entOmnishopProduct>>(File.ReadAllText($"_data/_differentJsons/fs2_prods_with_catIds_3Level.json"));

            //var oldProducts = new List<List<string>>();
            //foreach (var p in oldContext.Products.Include(pc => pc.ProductCategories).ToList())
            //{
            //    var listStrings = new List<string>();
            //    listStrings.Add(JsonConvert.DeserializeObject<ProductFromSite>(p.JsonSource).ProductId.ToString());

            //    foreach (var pc in p.ProductCategories)
            //    {
            //        listStrings.Add(oldContext.Categories.FirstOrDefault(c => c.Id == pc.CategoryId).ExtId.ToString());
            //    }
            //    oldProducts.Add(listStrings);
            //}

            //File.WriteAllText($"_data/_differentJsons/fs2_categories_of_old_products.json", JsonConvert.SerializeObject(oldProducts));
            //var withCat = listz2.Where(l => l.CatIds.Count > 0).ToList();
            //Console.WriteLine(withCat.Count);

            //foreach (var prod in listz2)
            //{
            //    var list = new List<string>();
            //    for (int i = 0; i < prod.CatIds.Count; i++)
            //    {
            //        var cat = context.Categories.FirstOrDefault(c => c.OmnishopCategoryId.ToString() == prod.CatIds[i]);
            //        if (cat != null)
            //        {
            //            list.Add(cat.Id.ToString());
            //        }
            //    }
            //    prod.CatIds = list;
            //}

            //var withCats2 = listz2.Where(l => l.CatIds.Count > 0).ToList();

            //var oldProducts = JsonConvert.DeserializeObject<List<List<string>>>(File.ReadAllText($"_data/_differentJsons/fs2_categories_of_old_products.json"));

            //int k = 0;

            //foreach (var gbUmb in gbUMBIds2)
            //{
            //    Console.WriteLine("Product:" + k++);
            //    var byOmn = listz2.FirstOrDefault(g => g.OmnishopProductId == gbUmb.First().OmnishopProductId);
            //    var catids = oldProducts.FirstOrDefault(cat => gbUmb.Any(g => g.UComProductId.ToString() == cat[0]));

            //    if (catids != null)
            //    {
            //        for (int j = 1; j < catids.Count; j++)
            //        {
            //            if (!byOmn.CatIds.Contains(catids[j]))
            //            {
            //                byOmn.CatIds.Add(catids[j]);
            //            }
            //        }
            //    }
            //}

            //var withCats3 = listz2.Where(l => l.CatIds.Count > 0).ToList();

            //File.WriteAllText($"_data/_differentJsons/fs2_products_with_all_categories.json", JsonConvert.SerializeObject(listz2));
            //var listz2 = JsonConvert.DeserializeObject<List<entOmnishopProduct>>(File.ReadAllText($"_data/_differentJsons/fs2_products_with_all_categories.json"));
            //var k = 0;

            //foreach (var prod in listz2)
            //{
            //    Console.WriteLine("Product:" + k++);

            //    if (prod.Published)
            //    {
            //        var dbProd = context.Products.FirstOrDefault(p => p.OmnishopProductId == prod.OmnishopProductId);

            //        if (dbProd == null)
            //            dbProd = new entOmnishopProduct();

            //        if (!string.IsNullOrEmpty(prod.Name))
            //            dbProd.Name = prod.Name;
            //        if (!string.IsNullOrEmpty(prod.ShortDescription))
            //            dbProd.ShortDescription = prod.ShortDescription;
            //        if (!string.IsNullOrEmpty(prod.LongDescription))
            //            dbProd.LongDescription = prod.LongDescription;
            //        dbProd.OmnishopProductId = prod.OmnishopProductId;
            //        dbProd.OmnishopBrandId =  prod.OmnishopBrandId;
            //        dbProd.Published = true;
            //        dbProd.UComProductId = prod.UComProductId;
            //        dbProd.Price = prod.Price;
            //        dbProd.Cost = prod.Cost;
            //        dbProd.Vat = prod.Vat;
            //        if (!string.IsNullOrEmpty(prod.Picture1))
            //            dbProd.Picture1 = prod.Picture1;
            //        if (!string.IsNullOrEmpty(prod.Picture2))
            //            dbProd.Picture2 = prod.Picture2;
            //        if (!string.IsNullOrEmpty(prod.Picture3))
            //            dbProd.Picture3 = prod.Picture3;

            //        if (string.IsNullOrEmpty(dbProd.Id.ToString()))
            //            context.Add(dbProd);
            //        else
            //            context.Update(dbProd);
            //    }
            //}

            //context.SaveChanges();
            //var k = 0;


            //foreach (var prod in listz2)
            //{
            //    Console.WriteLine("Product:" + k++);
            //    foreach (var cat in prod.CatIds)
            //    {
            //        if (!context.ProductCategories.Any(pc => pc.OmnishopCategoryId.ToString() == cat && pc.ProductId == prod.OmnishopProductId))
            //        {
            //            var category = context.Categories.FirstOrDefault(c => c.Id.ToString() == cat);
            //            var product = context.Products.FirstOrDefault(p => p.OmnishopProductId == prod.OmnishopProductId);
            //            if (category != null && product != null)
            //            {
            //                context.ProductCategories.Add(new entOmnishopProductCategory()
            //                {
            //                    OmnishopCategoryId = category.Id,
            //                    CategoryId = category.OmnishopCategoryId.ToString(),
            //                    ProductId = product.OmnishopProductId,
            //                    OmnishopProductId = product.Id
            //                });

            //                Console.WriteLine("Product:" + product.Id + " got category");
            //            }
            //        }
            //    }
            //}


            //var combinationsOfCategories = new List<List<entOmnishopProductCategory>>();

            //foreach (var pc in context.ProductCategories)
            //{
            //    var list = context.ProductCategories.Where(pcl => pcl.OmnishopCategoryId == pc.OmnishopCategoryId && pcl.OmnishopProductId == pc.OmnishopProductId).ToList();

            //    combinationsOfCategories.Add(list);
            //}
            ////int j = 0;
            //var moreThanOne = combinationsOfCategories.Where(l => l.Count > 2).ToList();
            //foreach (var coc in combinationsOfCategories.Where(l => l.Count > 1).ToList())
            //{
            //    for (int i = 1; i < coc.Count; i++)
            //    {
            //        context.ProductCategories.Remove(context.ProductCategories.FirstOrDefault(pc => pc.Id == coc[i].Id));
            //        j++;
            //    }
            //}
            //Console.WriteLine(j);
            //context.SaveChanges();
            //int k = 0;
            //foreach (var prod in allProducts)
            //{
            //    Console.WriteLine("Product:" + k++);

            //    if (prod[5] == "100")
            //    {
            //        var dbProd = context.Products.FirstOrDefault(p => p.OmnishopProductId == prod[0]);

            //        if (dbProd == null || dbProd.UComProductId == 0)
            //        {
            //            if (dbProd == null)
            //                dbProd = new entOmnishopProduct();

            //            if (!string.IsNullOrEmpty(prod[1]))
            //                dbProd.Name = prod[1];
            //            dbProd.OmnishopProductId = prod[0];
            //            if (!string.IsNullOrEmpty(prod[3]))
            //                dbProd.OmnishopBrandId = context.Brands.FirstOrDefault(b => b.OmnishopBrandId.ToString() == prod[3]).Id;
            //            dbProd.Published = true;
            //            if (!string.IsNullOrEmpty(prod[2]))
            //                dbProd.Price = double.Parse(prod[2]);
            //            dbProd.Cost = dbProd.Price;
            //            if (!string.IsNullOrEmpty(prod[7]))
            //                dbProd.Vat = Convert.ToInt32(double.Parse(prod[7]));

            //            if (string.IsNullOrEmpty(dbProd.Id.ToString()))
            //                context.Add(dbProd);
            //            else
            //                context.Update(dbProd);
            //        }
            //    }
            //}
            //context.SaveChanges();

            var listToCorrect = new List<entOmnishopProduct>();
            foreach (var prod in context.Products)
            {
                if (prod.Name.StartsWith(","))
                {
                    prod.Name = prod.Name.Remove(0, 1);
                    prod.Name = prod.Name.Trim();

                    listToCorrect.Add(prod);
                }

                if (prod.Name.EndsWith(","))
                {
                    prod.Name = prod.Name.Remove(prod.Name.Length - 1, 1);
                    prod.Name = prod.Name.Trim();

                    listToCorrect.Add(prod);
                }
            }

            foreach (var prod in context.Products)
            {
                var whiteSpaceStr = "  ";

                for (int i = 0; i < 20; i++)
                {
                    whiteSpaceStr += " ";
                }

                for (int i = 0; i < 20; i++)
                {
                    if (prod.Name.Contains(whiteSpaceStr))
                    {
                        prod.Name = prod.Name.Replace(whiteSpaceStr, " ");

                        listToCorrect.Add(prod);
                    }

                    whiteSpaceStr = whiteSpaceStr.Remove(0, 1);
                }
            }

            foreach (var prod in context.Products)
            {
                if (prod.Name.Contains(" ,"))
                {
                    prod.Name = prod.Name.Replace(" ,", ",");

                    listToCorrect.Add(prod);
                }
            }

            foreach (var prod in listToCorrect)
            {
                var product = context.Products.FirstOrDefault(p => p.Id == prod.Id);
                product.Name = prod.Name;
            }

            context.SaveChanges();
        }


        public static void CreateExcelForCategories()
        {
            var headers = new List<string> { "Id", "Name", "Description", "CategoryTemplateId", "ParentCategoryId", "Picture", "DisplayOrder" };
            var values = new List<List<string>>();

            foreach (var cat in context.Categories)
            {
                var value = new List<string>() { cat.Id.ToString(), cat.Name, "", "1", cat.ParentCategoryId.ToString(), "C:\\inetpub\\wwwroot\\4sound\\wwwroot\\images\\thumbs\\default-image.png", cat.SortOrder.ToString() };
                values.Add(value);
            }

            ExcelProdvider.CreateWorkSheet(headers, values, "_data/categories", "categories");
        }
        public static void CreateExcelForBrands()
        {
            var headers = new List<string> { "Id", "Name", "Description", "ManufacturerTemplateId", "Picture", "DisplayOrder" };
            var values = new List<List<string>>();

            foreach (var br in context.Brands)
            {
                var value = new List<string>() { br.Id.ToString(), br.Name, "", "1", "C:\\inetpub\\wwwroot\\4sound\\wwwroot\\images\\thumbs\\default-image.png", "1" };
                values.Add(value);
            }

            ExcelProdvider.CreateWorkSheet(headers, values, "_data/brands", "brands");
        }
        public static void CreateExcelForProducts(List<entOmnishopProduct> products)
        {
            var headers = new List<string> { "ProductId", "Name", "ShortDescription", "FullDescription", "MetaKeywords", "MetaTitle", "SKU", "StockQuantity", "Published",
                "MinStockQuantity", "OrderMinimumQuantity", "OrderMaximumQuantity", "Price", "ProductCost", "IsShipEnabled", "Categories", "Manufacturers", "Picture1", "Picture2", "Picture3" };
            var values = new List<List<string>>();

            foreach (var p in context.Products.Include(p => p.OmnishopProductCategories).Include(p => p.OmnishopBrand).Where(p => products == null || products.Any(ps => p.Id == ps.Id)).ToList())
            {
                var catStr = "";

                foreach (var c in p.OmnishopProductCategories)
                {
                    var thCat = context.Categories.FirstOrDefault(cat => cat.Id == c.OmnishopCategoryId);
                    var seCat = context.Categories.FirstOrDefault(cat => cat.Id == thCat.ParentCategoryId);
                    var frCat = context.Categories.FirstOrDefault(cat => cat.Id == seCat.ParentCategoryId);

                    var str = frCat.Name + " >> " + seCat.Name + " >> " + thCat.Name + ";";
                    if (!catStr.Contains(str))
                        catStr += str;
                }

                var value = new List<string>() { p.Id.ToString(), p.Name, p.ShortDescription, p.LongDescription, p.OmnishopBrand != null? p.OmnishopBrand.Name : "", p.Name, p.OmnishopProductId,
                    "100", p.Published.ToString(), "1", "1", "999", p.Price.ToString(), p.Cost.ToString(), true.ToString(), string.IsNullOrEmpty(catStr) ? "Uncategorized;" : catStr, p.OmnishopBrand != null? p.OmnishopBrand.Name +  ";" : "", $@"C:\inetpub\wwwroot\4sound\wwwroot\images\thumbs\{p.Picture1}.png",
                    $@"C:\inetpub\wwwroot\4sound\wwwroot\images\thumbs\{p.Picture2}.png", $@"C:\inetpub\wwwroot\4sound\wwwroot\images\thumbs\{p.Picture3}.png" };

                values.Add(value);
            }

            ExcelProdvider.CreateWorkSheet(headers, values, "_data/products", "products");
        }

        public static void GenerateActiveImages()
        {
            var files = Directory.GetFiles("_data/Images");
            var pictures = new List<string>();

            foreach (var prod in context.Products)
            {
                if (!string.IsNullOrEmpty(prod.Picture1))
                    pictures.Add(prod.Picture1);
                if (!string.IsNullOrEmpty(prod.Picture2))
                    pictures.Add(prod.Picture2);
                if (!string.IsNullOrEmpty(prod.Picture3))
                    pictures.Add(prod.Picture3);
            }

            var newPircturesPaths = new List<string>();
            Directory.CreateDirectory("_data/newImages");

            foreach (var f in files)
            {
                var fileInfo = new FileInfo(f);
                if (pictures.Any(p => p == fileInfo.Name.Split('.')[0]))
                {
                    File.WriteAllBytes("_data/newImages/" + fileInfo.Name, File.ReadAllBytes(f));

                    newPircturesPaths.Add(fileInfo.FullName);
                }
            }

        }

        public static void CrazyLink()
        {
            var cats = new List<object>();
            foreach (var cat in oldContext.Categories)
            {
                if (cat.ExtId != 0)
                {
                    var nc = context.Categories.FirstOrDefault(c => c.Id == cat.ExtId);
                    cats.Add(new { nc = nc, oc = cat });
                    Console.WriteLine("" + nc.Name + " - " + cat.Name);
                }
            }

            //int i = 0;
            //var localList = new List<object>();
            //foreach (var pc in oldContext.ProductCategories.Include(p => p.Product).Include(c => c.Category).Where(c => c.Category.ExtId != 0).ToList())
            //{
            //    i++;
            //    var newProd = context.Products.FirstOrDefault(p => p.UComProductId == JsonConvert.DeserializeObject<ProductFromSite>(pc.Product.JsonSource).ProductId);
            //    if (newProd != null)
            //    {
            //        var newCat = context.Categories.FirstOrDefault(c => c.Id == pc.Category.ExtId);
            //        if (newCat.Name == "Elektrisk")
            //        {
            //            var r = 0;
            //        }
            //        localList.Add(new entOmnishopProductCategory() { ProductId = newProd.OmnishopProductId, CategoryId = newCat.OmnishopCategoryId.ToString(), OmnishopCategoryId = newCat.Id, OmnishopProductId = newProd.Id });
            //    }
            //    Console.WriteLine("prodCat: " + i);
            //}


            //File.WriteAllText($"_data/wts.json", JsonConvert.SerializeObject(localList));


            //var list = JsonConvert.DeserializeObject<List<entOmnishopProductCategory>>(File.ReadAllText("_data/wts.json"));

            //var difference = list.Where(p => !context.ProductCategories.Any(pc => pc.OmnishopCategoryId == p.OmnishopCategoryId && pc.OmnishopProductId == p.OmnishopProductId)).ToList();

            //File.WriteAllText($"_data/difference.json", JsonConvert.SerializeObject(difference));
            //var difference = JsonConvert.DeserializeObject<List<entOmnishopProductCategory>>(File.ReadAllText("_data/difference.json"));


            //foreach (var dif in difference)
            //{
            //    context.ProductCategories.Add(dif);
            //}
            //context.SaveChanges();

            //var difProds = context.Products.Where(p => difference.Any(d => d.OmnishopProductId == p.Id)).ToList();
            //CreateExcelForProducts(difProds);

            //foreach(var prod in context.Products)
            //{
            //    if (string.IsNullOrEmpty(prod.Name))
            //    {
            //        var product = context.Products.FirstOrDefault(p => p.Id == prod.Id);
            //        product.WIthoutName = true;
            //        context.Products.Update(product);
            //    }
            //}


            //context.SaveChanges();

            var wn = context.Products.Where(p => p.WIthoutName).ToList();
            var list = new List<List<string>>();

            using (SqlConnection connection = new SqlConnection(fsLargeConnectionString))
            {
                string getWithoutNameProducts = "SELECT [ProductId] ,[DisplayName] FROM[4sound_umbraco].[dbo].[PIM_ProductQuery] where ProductId in(" + string.Join(",", wn.Select(p => p.UComProductId).ToArray()) + ") and Market = '4sound.no'";

                SqlCommand command2 = new SqlCommand(getWithoutNameProducts, connection);

                connection.Open();
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    list.Add(new List<string>() { reader2[0].ToString(), reader2[1].ToString() });
                }
                reader2.Close();
                connection.Close();
            }


        }

        public static void SaveProductSpecsToDB()
        {
            using (SqlConnection connection = new SqlConnection(fsLargeConnectionString))
            {
                string getSpecs = "SELECT pdfd.DisplayName, Value, pdf.DisplayOnSite, Deleted, DataTypeId FROM uCommerce_ProductProperty PP " +
                  "join uCommerce_ProductDefinitionField PDF ON PP.ProductDefinitionFieldId = PDF.ProductDefinitionFieldId " +
                  "join uCommerce_ProductDefinitionFieldDescription pdfd on pdfd.ProductDefinitionFieldId = pp.ProductDefinitionFieldId ";

                foreach (var prod in context.Products.Where(p => p.UComProductId != 0))
                {
                    prod.OmnishopProductSpecs = new List<entOmnishopProductSpec>();

                    var qst = getSpecs + "where ProductId = " + prod.UComProductId + " and pdfd.CultureCode = 'nb-NO' and DisplayName not in ('Image', 'Product state', 'Product URL', 'Manufacturer ID'," +
                        " 'Media gallery', 'Thomann product ID', 'Commitment Type', 'Replaced by VariantSku') order by ProductId, SortOrder";
                    SqlCommand command2 = new SqlCommand(qst, connection);

                    connection.Open();
                    SqlDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        var spec = new entOmnishopProductSpec();
                        spec.UComProductId = prod.UComProductId;
                        //spec.SKU = prod.OmnishopProductId;
                        spec.Name = reader2[0].ToString();
                        spec.Value = reader2[1].ToString();
                        var val = reader2[2].ToString();
                        spec.IsDisplayed = Boolean.Parse(reader2[2].ToString());
                        spec.IsDeleted = Boolean.Parse(reader2[3].ToString());
                        spec.DataTypeId = int.Parse(reader2[4].ToString());
                        spec.OmnishopProductId = prod.Id;

                        context.ProductSpecs.Add(spec);
                    }
                    reader2.Close();
                    connection.Close();

                }

                var unusualSpecs = context.ProductSpecs.Local.Where(ps => ps.Name != "UPC Barcode").ToList();
                var usualProducts = context.ProductSpecs.Local.GroupBy(p => p.OmnishopProductId).ToList();
                var unusualProducts = unusualSpecs.GroupBy(u => u.OmnishopProductId).ToList();
            }
        }

        public static void GenerateJsonProductImport()
        {
            var products = context.Products.ToList();
            var infoProducts = new List<entOmnishopProduct>();

            int i = 0;
            int counter = 1;

            while (i < products.Count)
            {
                int j = 0;

                for (int k = i; k < products.Count; k++)
                {
                    var product = products[k];

                    if (j == 5000)
                        break;
                    ++j;

                    Console.WriteLine("Product: " + ++i);

                    if (product.OmnishopBrandId != null)
                    {
                        var brand = context.Brands.FirstOrDefault(b => b.Id == product.OmnishopBrandId);
                        product.OmnishopBrand = new entOmnishopBrand() { Name = brand.Name };
                    }

                    product.OmnishopProductCategories = context.ProductCategories.Where(pc => pc.OmnishopProductId == product.Id).Select(s => new entOmnishopProductCategory()
                    {
                        OmnishopCategory = s.OmnishopCategory,
                        OmnishopCategoryId = s.OmnishopCategoryId,
                        CategoryId = s.CategoryId,
                    }).ToList();

                    foreach (var productCategory in product.OmnishopProductCategories)
                    {
                        var category = context.Categories.FirstOrDefault(c => c.Id == productCategory.OmnishopCategoryId);
                        category.ParentCategory = context.Categories.FirstOrDefault(c => c.Id == category.ParentCategoryId);
                        category.ParentCategory.ParentCategory = context.Categories.FirstOrDefault(c => c.Id == category.ParentCategory.ParentCategoryId);

                        productCategory.OmnishopCategory = new entOmnishopCategory()
                        {
                            Name = category.Name,
                            ParentCategory = new entOmnishopCategory()
                            {
                                Name = category.ParentCategory.Name,
                                ParentCategory = new entOmnishopCategory() { Name = category.ParentCategory.ParentCategory.Name }
                            }
                        };
                    }

                    infoProducts.Add(product);
                }

                File.WriteAllText("_data/json_import/productForImport" + counter++ + ".json", JsonConvert.SerializeObject(infoProducts));
                infoProducts = new List<entOmnishopProduct>();
            }
        }

        public static void GenerateJsonCategoryImport()
        {
            var categories = context.Categories.ToList();
            var infoCategories = new List<entOmnishopCategory>();

            //File.WriteAllText("_data/json_import/categoriesForImport" + ".json", JsonConvert.SerializeObject(categories, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

            int i = 0;
            int counter = 1;

            while (i < categories.Count)
            {
                int j = 0;

                for (int k = i; k < categories.Count; k++)
                {
                    var category = categories[k];

                    if (j == 5000)
                        break;
                    ++j;

                    Console.WriteLine("Category: " + ++i);

                    var parentCategory1 = categories.FirstOrDefault(p => p.Id == category.ParentCategoryId);
                    if (parentCategory1 != null)
                    {
                        category.ParentCategory = new entOmnishopCategory() { Name = parentCategory1.Name, ParentCategoryId = parentCategory1.Id };

                        var parentCategory2 = categories.FirstOrDefault(p => p.Id == parentCategory1.ParentCategoryId);

                        if (parentCategory2 != null)
                            category.ParentCategory.ParentCategory = new entOmnishopCategory() { Name = parentCategory2.Name, ParentCategoryId = parentCategory2.Id };
                    }

                    infoCategories.Add(category);
                }

                File.WriteAllText("_data/json_import/categoriesForImport" + counter++ + ".json", JsonConvert.SerializeObject(infoCategories, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                infoCategories = new List<entOmnishopCategory>();
            }
        }

        public static void GenerateJsonManufacturerImport()
        {
            var manufacturers = context.Brands.ToList();

            File.WriteAllText("_data/json_import/manufacturersForImport" + ".json", JsonConvert.SerializeObject(manufacturers, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }
}
