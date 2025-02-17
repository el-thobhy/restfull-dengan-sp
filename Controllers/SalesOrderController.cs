﻿using ApiPointOfSales.DataModel;
using ApiPointOfSales.Repository;
using ApiPointOfSales.Services;
using ApiPointOfSales.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPointOfSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly ServicesOrder _servicesOrder;
        private readonly SalesOrderDetailRepository _repoOrderDetail;
        private readonly SalesOrderRepository _repoOrder;
        public SalesOrderController(IConfiguration configuration, DbTesAuriwanyasperContext context)
        {
            _repoOrder = new SalesOrderRepository(configuration["ConnectionStrings:DB_Conn"]);
            _repoOrderDetail = new SalesOrderDetailRepository(configuration["ConnectionStrings:DB_Conn"]);
            _servicesOrder = new ServicesOrder(configuration["ConnectionStrings:DB_Conn"], context,_repoOrderDetail, _repoOrder);
        }

        [HttpPost("insert")]
        public async Task<IActionResult> CreateOrder(RequestSalesOrder order)
        {
            Object output = await _servicesOrder.CreateSalesOrder(order);
            return ServicesOrder.isError ? BadRequest(output) : Ok(output);
        }
        [HttpGet("view")]
        public IActionResult ReadSalesOrder(string salesOrderCode)
        {
            Object output = _servicesOrder.ReadById(salesOrderCode);       
            return ServicesOrder.isError ? BadRequest(output) : Ok(output);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteSalesOrder(string salesOrderCode)
        {
            Object output = _servicesOrder.DeleteSalesOrder(salesOrderCode);
            return ServicesOrder.isError ? BadRequest(output) : Ok(output);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrder(GetOrUpdateOrder order)
        {
            Object output = await _servicesOrder.UpdateSalesOrder(order);
            return ServicesOrder.isError ? BadRequest(output) : Ok(output);
        }
    }
}
