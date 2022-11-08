using ServiceLayer;
using Datalayer.Models;
using Datalayer;
using ServiceLayer.Interface;
using ServiceLayer.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services;
using ServiceLayer.QueryObjects;
using System;
using System.Text.Json;

namespace ConsoleEshop
{


    internal class Program
    {
        static ServiceProvider? Repository = new ServiceCollection()
              .AddSingleton<IProductService, IProductService>()
              .BuildServiceProvider();

        static ServiceLayer.Interface.IProductService? _IRepo = Repository.GetService<ServiceLayer.Interface.IProductService>();

        static void Main(string[] args)
        {
            

            bool exit = true;
            while (exit)
            {


                Menu();

                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        CreateProduct();
                        break;
                    case 2:
                        UpdateProdukt();
                        break;
                    case 3:
                        DeleteProdukt();
                        break;
                    case 4:
                        SearchProdukt();
                        break;
                    case 5:
                        OrderProduktsBy();
                        break;
                    case 6:
                        PageProdukts();
                        break;
                    case 7:
                        SoftDeleteProdukt();
                        break;
                    case 8:
                        SelectProduktDto();
                        break;
                    case 0:
                        exit = false;
                        break;
                    default:                     
                        break;
                }
            }


        }

        static void CreateProduct()
        {
            Console.Clear();
            List<Types> types = _IRepo.GetAllTypes();
            List<Brand> brands = _IRepo.GetAllBrands();

            Produkt produkt = new();
            Console.Write("Enter Produkt Name:");
            produkt.ProduktName = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter Price: ");
            produkt.Price = Convert.ToDecimal(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Enter BrandId:");
            foreach (var item in brands)
            {
                Console.WriteLine($"{item.BrandId} - {item.BrandName}");
            }
            produkt.BrandId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter TypeId:");
            foreach (var item in types)
            {
                Console.WriteLine($"{item.TypesId} - {item.TypeName}");
            }
            produkt.TypesId = Convert.ToInt32(Console.ReadLine());

            //_IRepo.AddNewEntryGeneric(produkt);
        }

        static void UpdateProdukt()
        {
            Console.Clear();
            List<Types> types = _IRepo.GetAllTypes();
            List<Brand> brands = _IRepo.GetAllBrands();
            List<Produkt> produkts = _IRepo.GetAllProducts();
            Produkt produkt;
            int id;


            Console.WriteLine("Select which product you wanna update: ");
            foreach (var item in produkts)
            {
                Console.WriteLine($"{item.ProduktId}: {item.ProduktName}");
            }

            id = Convert.ToInt32(Console.ReadLine());

            produkt = produkts.Where(x => x.ProduktId == id).FirstOrDefault();

            Console.Clear();

            Console.WriteLine($"you chose: Product Id: {produkt.ProduktId} Produkt name: {produkt.ProduktName}");

            Console.Write("Enter New Produkt Name:");
            produkt.ProduktName = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter Price: ");
            produkt.Price = Convert.ToDecimal(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Enter BrandId:");
            foreach (var item in brands)
            {
                Console.WriteLine($"{item.BrandId} - {item.BrandName}");
            }
            produkt.BrandId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter TypeId:");
            foreach (var item in types)
            {
                Console.WriteLine($"{item.TypesId} - {item.TypeName}");
            }
            produkt.TypesId = Convert.ToInt32(Console.ReadLine());
            //_IRepo.UpdateEntryGeneric(produkt);

        }

        static void DeleteProdukt()
        {
            Console.Clear();
            List<Produkt> produkts = _IRepo.GetAllProducts();
            int id;
            Console.WriteLine("Select which product you wanna Delete: ");
            foreach (var item in produkts)
            {
                Console.WriteLine($"{item.ProduktId}: {item.ProduktName}");
            }

            id = Convert.ToInt32(Console.ReadLine());
            Produkt produkt = produkts.Where(s => s.ProduktId == id).FirstOrDefault();
            //_IRepo.DeleteEntryGeneric(produkt);
        }

        static void SearchProdukt()
        {
            Console.Clear();
            Console.WriteLine("Search: ");
            string SearchString = Console.ReadLine();

            List<Produkt> produkts = _IRepo.FindProdukts(SearchString);

            if (produkts != null)
            {
                foreach (var item in produkts)
                {
                    Console.WriteLine($"Produkt ID:{item.ProduktId} ProduktName = {item.ProduktName} Price: {item.Price} Brand: {item.Brand.BrandName} Type: {item.Type.TypeName}");
                }
            }
            else
            {
                Console.WriteLine("No such Produkt was found");
            }
            Console.WriteLine("Press anykey to continue");
            Console.ReadKey();

        }

        static void OrderProduktsBy()
        {
            SortFilterOptions sortFilterOptions = new();
            Console.Clear();
            Console.WriteLine("how do you wanna sort produkts");

            List<Produkt> produkts = new();
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    Console.Clear();
                    sortFilterOptions.OrderByOptions = OrderByOptions.SimpleOrder;
                    //produkts = _IRepo.SortProdukts(sortFilterOptions);
                    break;
                case 2:
                    Console.Clear();
                    sortFilterOptions.OrderByOptions = OrderByOptions.ByPrice;
                    //produkts = _IRepo.SortProdukts(sortFilterOptions);
                    break;
                case 3:
                    Console.Clear();
                    sortFilterOptions.OrderByOptions = OrderByOptions.ByBrand;
                    //produkts = _IRepo.SortProdukts(sortFilterOptions);
                    break;
                default:
                    break;
            }

            foreach (var item in produkts)
            {
                Console.WriteLine($"ProduktId: {item.ProduktId} ProduktName: {item.ProduktName} Price: {item.Price} BrandName: {item.Brand.BrandName}");
            };

            Console.WriteLine("Press anykey to continue");
            Console.ReadKey();
        }

        static void PageProdukts()
        {
            Console.WriteLine("Which Site do you wanna see?");
            List<Produkt> Produkts = _IRepo.Paging(Convert.ToInt32(Console.ReadLine()));


            foreach (var item in Produkts)
            {
                Console.WriteLine($"ProduktId: {item.ProduktId} ProduktName: {item.ProduktName} Price: {item.Price} BrandName: {item.Brand.BrandName}");
            };

            Console.WriteLine("Press anykey to continue");
            Console.ReadKey();

        }

        static void SoftDeleteProdukt()
        {
            Console.Clear();
            List<Produkt> produkts = _IRepo.GetAllProducts();
            int id;

            Console.WriteLine("Which produkt do you wanna remove from the site");

            foreach (var item in produkts)
            {

                Console.WriteLine($"{item.ProduktId}: {item.ProduktName}");

            }

            id = Convert.ToInt32(Console.ReadLine());

            Produkt produkt = produkts.Where(x => x.ProduktId == id).FirstOrDefault();
            if (produkt != null)
            {
                produkt.IsSoftDeleted = true;

                //_IRepo.UpdateEntryGeneric(produkt);
            }
            else
            {
                Console.WriteLine("Produkt not found");
            }

        }

        static void SelectProduktDto()
        {
            var produkts = _IRepo.ProduktsToProduktListDto();

            foreach (var item in produkts)
            {

                Console.WriteLine($"Produkt ID: {item.ProduktId} Produkt Name: {item.ProduktName}  Price: {item.Price} BrandName: {item.BrandName}");

            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(); 
        }
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("*** Welcome to {0} ***", "This Shop");
            Console.WriteLine("--------------------------");
            Console.WriteLine("Please Select");
            Console.WriteLine("1 = Create New Produkt");
            Console.WriteLine("2 = Update Produkt");
            Console.WriteLine("3 = Delete Produkt");
            Console.WriteLine("4 = Search Produkt");
            Console.WriteLine("5 = Order Produkts");
            Console.WriteLine("6 = Page Produkts");
            Console.WriteLine("7 = Soft Delete Produkt");
            Console.WriteLine("8 = ProduktDto");
            Console.WriteLine("--------------------------");
        }
    }

}