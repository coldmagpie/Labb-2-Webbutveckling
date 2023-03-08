﻿using DataAccess.DataAccess.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccess.Interfaces;
using DataAccess.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using WebbLabb2.Shared;

namespace DataAccess.DataAccess.Repositories
{
    public class UserRepository : IUserRepository<UserModel>
    {
        private readonly StoreContext _storeContext;

        public UserRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<ServiceResponse<List<UserModel>>> GetAllUsers()
        {
            var response = new ServiceResponse<List<UserModel>>();
            var users = await _storeContext.Users.ToListAsync();
            if (users is null)
            {
                response.Error = true;
                response.Message = "No user found";
            }

            response.Error = false;
            response.Data = users;
            
            return response;
        }
        public async Task<ServiceResponse<UserModel>> GetUserById(int userId)
        {
            var user = await _storeContext.Users.FirstOrDefaultAsync(u=>u.Id == userId);
            if (user is null)
            {
                return new ServiceResponse<UserModel>
                {
                    Error = true,
                    Message = "User not found."
                };
            }
            return new ServiceResponse<UserModel> { Data = user, Error = false };
        }

        public async Task<ServiceResponse<UserModel>> GetUserByEmail(string email)
        {
            var response = new ServiceResponse<UserModel>();
            var user = await _storeContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
            if (user is null)
            {
                response.Error = true;
                response.Message = "No user found";
            }
            else
            {
                response.Error = false;
                response.Data = user;
            }
            return response;
        }

        //public async Task<ServiceResponse<List<ProductModel>>> GetUserOrderItems(int userId, int orderId)
        //{
        //    var response = new ServiceResponse<List<ProductModel>>();
            
        //    var user = await _storeContext.Users.Include(u=>u.Orders).ThenInclude(o => o.OrderDetails).FirstOrDefaultAsync(u=> u.Id == userId);
        //    if (user is null)
        //    {
        //        response.Error = true;
        //        response.Message = "No user found";
        //    }
        //    var order = user.Orders.FirstOrDefault(o=>o.Id == orderId);
        //    if (order is null)
        //    {
        //        response.Error = true;
        //        response.Message = "No order found";
        //    }
        //    var orderItems = order.OrderDetails.ToList();
        //    if (orderItems is null)
        //    {
        //        response.Error = true;
        //        response.Message = "No item found";
        //    }
        //    else
        //    {
        //        response.Error = false;
        //        response.Data = orderItems;
        //    }

        //    return response;
        //}
    }
}
