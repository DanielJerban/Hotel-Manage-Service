using System;
using System.Collections.Generic;
using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using HMS.Model.Core.DTOs.Customer;

namespace HMS.Service.Persistance.Repositories
{
    public class CustomerRepo : Repository<Customer>, ICustomerRepo
    {
        public CustomerRepo(ApplicationDbContext context) : base(context)
        {
        }

        public List<GetCustomerDto> GetlFelowCustomers(Guid parentId)
        {
            var customers = context.Customers.Where(C => C.ParentId == parentId).ToList();

            List<GetCustomerDto> customerDtos = new List<GetCustomerDto>();

            foreach (var customer in customers)
            {
                customerDtos.Add(new GetCustomerDto()
                {
                    FirstName = customer.FirstName ?? "",
                    LastName = customer.LastName ?? "",
                    NationalNo = customer.NationalNo ?? "",
                    PassportNo = customer.PassportNo ?? "",
                    Id = customer.Id
                });
            }

            return customerDtos; 
        }

        public List<Customer> AllCustomersExceptHost(Guid hostId)
        {
            var allCustomers = context.Customers.ToList();
            var customers = new List<Customer>();

            foreach (var customer in allCustomers)
            {
                if (customer.Id == hostId)
                {
                    continue;
                }
                else
                {
                    customers.Add(customer);
                }
            }

            return customers;
        }

        public List<GetCustomerDto> MapList(List<Customer> customers)
        {
            List<GetCustomerDto> customerDtos = new List<GetCustomerDto>();

            foreach (var customer in customers)
            {
                customerDtos.Add(new GetCustomerDto()
                {
                    CreatedDate = customer.CreatedDate,
                    FirstName = customer.FirstName ?? "",
                    LastName = customer.LastName ?? "",
                    NationalNo = customer.NationalNo ?? "",
                    PassportNo = customer.PassportNo ?? "",
                    Id = customer.Id
                });
            }

            return customerDtos;
        }
    }
}