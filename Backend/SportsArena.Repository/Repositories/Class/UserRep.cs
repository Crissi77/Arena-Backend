

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit.Encodings;
using SportsArena.Database.SportsDb;
using SportsArena.Models.DbModels;
using SportsArena.Models.DTOModels;
using SportsArena.Repository.Repositories.Interface;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SportsArena.Repository.Repositories.Class
{
    public class UserRep : IuserRep
    {
        DataContext _rep;
        IMapper _mapper;
        private readonly string _baseImageUrl;
        public UserRep(DataContext rep,IMapper mapper,IConfiguration configuration)
        {
            _rep = rep; 
            _mapper = mapper;
             _baseImageUrl = configuration.GetSection("AppSettings:BaseImageUrl").Value;

        }


        public SportsDto GetUserByID(int id)
        {
            
            var user = (from details in _rep.userdetails
                        join address in _rep.useraddress
                        on details.UserId equals address.UserId
                        where details.UserId == id 
                        select new SportsDto
                        {
                            FirstName = details.FirstName,
                            LastName = details.LastName,
                            Email = details.Email,
                            PhoneNumber = details.PhoneNumber,
                           RoleId = details.RoleId,
                            AddressLine1 = address.AddressLine1,
                            AddressLine2 = address.AddressLine2,
                            City = address.City,
                            State = address.State,
                            Country = address.Country,
                            Pincode = address.Pincode
                        }).FirstOrDefault();

            
            return user;
        }

        public UpdateProfileDto EditProfile(UpdateProfileDto data, int id)
        {

            var details = _rep.userdetails.Where(s => s.UserId == id).FirstOrDefault();

            var address = _rep.useraddress.Where(x=>x.UserId == id).FirstOrDefault();
            
            if (details != null)
            {
                details.UserId = id;
                details.FirstName = data.FirstName;
                details.LastName = data.LastName;
                details.Email = data.Email;
                details.PhoneNumber = data.PhoneNumber;
                
            }

            if(address != null)
            {
                address.AddressLine1 = data.AddressLine1;
                address.AddressLine2 = data.AddressLine2;
                address.City = data.City;
                address.State = data.State;
                address.Country = data.Country;
                address.Pincode = data.Pincode;
            }
            

            _rep.SaveChanges();
            return data;

        }


        public productDto AddProduct(productDto data,int userId)
        {
           
            if(data == null)
            {
                throw new ArgumentNullException(nameof(data),"Product cannot be null");
            }

            var productinfo = _mapper.Map<Product>(data);


            productinfo.UserId = userId; 
            productinfo.CreatedAt = DateTime.Now;
            productinfo.UpdatedAt = DateTime.Now;
            productinfo.IsActive = true;

            try
            {
                _rep.product.Add(productinfo);
                _rep.SaveChanges();

                return _mapper.Map<productDto>(productinfo);
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error adding product:{ex.Message}", ex);
            }


        }

        public List<productDto> GetProducts()
        {

            var products = _rep.product.ToList();


            var productDtos = _mapper.Map<List<productDto>>(products);

            foreach (var dto in productDtos)
            {
               
                dto.Image = $"{_baseImageUrl}/{dto.Image}";
            }

            return productDtos;
        }


        public string DeleteProduct(int id)
        {
            var product = _rep.product.Where(p => p.ProductId == id).FirstOrDefault();

            var res = _rep.product.Remove(product);
            _rep.SaveChanges();
            if (res != null)
            {
                return "Deleted successfully.";
            }
            return "Product can not be deleted!";
        }

    }
}





