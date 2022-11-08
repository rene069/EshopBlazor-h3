using Datalayer;
using Datalayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository
{
    public class CreateService : ICreateService
    {
        public readonly EShopContext _eShopContext;

        public CreateService(EShopContext eShopContext)
        {
            _eShopContext = eShopContext;
        }

        public void AddNewEntryGeneric<T>(T Entry) where T : class
        {
            _eShopContext.Add(Entry);
            _eShopContext.SaveChanges();
        }

        
        public void UpdateEntryGeneric<T>(T entry) where T : class
        {
            _eShopContext.Update(entry);
            _eShopContext.SaveChanges();
        }

       
        public void DeleteEntryGeneric<T>(T entry) where T : class
        {
            _eShopContext.Remove(entry);
            _eShopContext.SaveChanges();

        }



    }
}
