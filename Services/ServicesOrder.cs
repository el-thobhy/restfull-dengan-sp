using ApiPointOfSales.DataModel;
using ApiPointOfSales.Repository;
using ApiPointOfSales.ViewModel;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;

namespace ApiPointOfSales.Services
{
    public class ServicesOrder
    {
        private readonly SalesOrderRepository _salesOrderRepository;
        private readonly SalesOrderDetailRepository _salesOrderDetailRepository;
        private readonly DbTesAuriwanyasperContext _dbContext;
        private readonly string _connString;
        public static bool isError;
        public ServicesOrder(string connString,DbTesAuriwanyasperContext context ,SalesOrderDetailRepository salesOrderDetailRepository, SalesOrderRepository salesOrderRepository)
        {
            _connString = connString;
            _dbContext = context;
            _salesOrderRepository = salesOrderRepository;
            _salesOrderDetailRepository = salesOrderDetailRepository;
        }
        public async Task<Object> CreateSalesOrder(RequestSalesOrder request)
        {
            ResponseSuccessCreateOrder response = new ResponseSuccessCreateOrder();
            using(var conn = new SqlConnection(_connString))
            {
                await conn.OpenAsync();
                using(var trans = conn.BeginTransaction())
                {
                    try
                    {
                        Customer? customer = _dbContext.Customers
                            .Where(o => o.CustId == request.CustId)
                            .FirstOrDefault();
                        if (customer == null)
                        {
                            ResponseOrder responseError = new ResponseOrder();
                            responseError.Status = "failed";
                            responseError.Message = $"Customer dengan code {request.CustId} tidak ditemukan";
                            isError = true;
                            return responseError;
                        }
                        else
                        {
                            var salesOrder = new SalesOrder
                            {
                                OrderDate = DateTime.Now,
                                SalesOrderNo = GenerateSalesOrderNumber(),
                                CustCode = request.CustId
                            };

                            foreach (var orderDetail in request.OrderDetail)
                            {
                                Product? detail = _dbContext.Products
                                    .Where(o => o.ProductCode == orderDetail.ProductCode)
                                    .FirstOrDefault();

                                Price? price = _dbContext.Prices
                                    .Where(o => o.ProductCode == orderDetail.ProductCode)
                                    .FirstOrDefault();

                                if (detail == null)
                                {
                                    ResponseOrder responseError = new ResponseOrder();
                                    responseError.Status = "failed";
                                    responseError.Message = $"Product dengan code {orderDetail.ProductCode} tidak ditemukan";
                                    isError = true;

                                    return responseError;
                                }
                                else
                                {
                                    decimal priceNow = 0;

                                    //check harga
                                    if (salesOrder.OrderDate > price.PriceValidateFrom && salesOrder.OrderDate < price.PriceValidateTo)
                                    {
                                        priceNow = price.Price1 ?? 0;
                                    }
                                    else
                                    {
                                        priceNow = 0;
                                    }
                                    var salesOrderDetail = new SalesOrderDetail
                                    {
                                        SalesOrderNo = salesOrder.SalesOrderNo,
                                        ProductCode = orderDetail.ProductCode,
                                        Qty = orderDetail.Qty,
                                        Price = priceNow
                                    };
                                    isError = false;
                                    await _salesOrderDetailRepository.Create(conn ,salesOrderDetail, trans);
                                    response.Status = "success";
                                    response.SalesOrderNo = salesOrder.SalesOrderNo;
                                    response.Message = "Data has been insert successfully";
                                }
                            }
                            await _salesOrderRepository.Create(conn, salesOrder, trans);
                            trans.Commit();
                            return response;
                        }
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw new Exception(e.Message);
                    }
                }
                
            }

        }

        public Object ReadById(string salesOrderCode)
        {
            try
            {
                GetOrUpdateOrder result = new GetOrUpdateOrder();
                SalesOrder getSales = _salesOrderRepository.ReadById(salesOrderCode);
                if (getSales.SalesOrderNo.IsNullOrEmpty())
                {
                    ResponseOrder responseError = new ResponseOrder();
                    responseError.Status = "failed";
                    responseError.Message = $"No Order {salesOrderCode} tidak ditemukan";
                    isError = true;
                    return responseError;
                }
                else
                {
                    result.SalesOrderNo = getSales.SalesOrderNo;
                    result.CustId = getSales.CustCode;

                    var detail = _salesOrderDetailRepository.ReadById(salesOrderCode)
                        .Select(o=>new RequestSalesOrderDetail
                        {
                            ProductCode = o.ProductCode,
                            Qty = o.Qty
                        }).ToList();
                    result.OrderDetail = detail;
                    isError = false;
                    return result;
                }
            }
            catch (Exception)
            {
                isError = true;
                throw;
            }
           
        }
        public Object DeleteSalesOrder(string salesOrderCode)
        {
            try
            {
                GetOrUpdateOrder result = new GetOrUpdateOrder();
                bool del = _salesOrderRepository.Delete(salesOrderCode);
                bool detailDel = _salesOrderDetailRepository.Delete(salesOrderCode);
                if (!del && !detailDel)
                {
                    ResponseOrder responseError = new ResponseOrder();
                    responseError.Status = "failed";
                    responseError.Message = $"No Order {salesOrderCode} tidak ditemukan";
                    isError = true;
                    return responseError;
                }
                else
                {
                    ResponseOrder response = new ResponseOrder();
                    response.Status = "success";
                    response.Message = $"Data has been delete successfully";
                    isError = false;
                    return response;
                }
            }
            catch (Exception)
            {
                isError = true;
                throw;
            }
        }

        public async Task<Object> UpdateSalesOrder(GetOrUpdateOrder request)
        {
            ResponseSuccessCreateOrder response = new ResponseSuccessCreateOrder();
            using (var conn = new SqlConnection(_connString))
            {
                await conn.OpenAsync();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        Customer? customer = _dbContext.Customers
                            .Where(o => o.CustId == request.CustId)
                            .FirstOrDefault();
                        if (customer == null)
                        {
                            ResponseOrder responseError = new ResponseOrder();
                            responseError.Status = "failed";
                            responseError.Message = $"Customer dengan code {request.CustId} tidak ditemukan";
                            isError = true;
                            return responseError;
                        }

                        SalesOrder? order = _dbContext.SalesOrders
                            .Where(o => o.SalesOrderNo == request.SalesOrderNo)
                            .FirstOrDefault();
                        if (order == null)
                        {
                            ResponseOrder responseError = new ResponseOrder();
                            responseError.Status = "failed";
                            responseError.Message = $"No Order {request.SalesOrderNo} tidak ditemukan";
                            isError = true;
                            return responseError;
                        }
                        else
                        {
                            var salesOrder = new SalesOrder
                            {
                                OrderDate = order.OrderDate,
                                SalesOrderNo = request.SalesOrderNo,
                                CustCode = request.CustId
                            };

                            foreach (var orderDetail in request.OrderDetail)
                            {
                                Product? detail = _dbContext.Products
                                    .Where(o => o.ProductCode == orderDetail.ProductCode)
                                    .FirstOrDefault();

                                Price? price = _dbContext.Prices
                                    .Where(o => o.ProductCode == orderDetail.ProductCode)
                                    .FirstOrDefault();

                                if (detail == null)
                                {
                                    ResponseOrder responseError = new ResponseOrder();
                                    responseError.Status = "failed";
                                    responseError.Message = $"Product dengan code {orderDetail.ProductCode} tidak ditemukan";
                                    isError = true;

                                    return responseError;
                                }
                                else
                                {
                                    decimal priceNow = 0;

                                    if (salesOrder.OrderDate > price.PriceValidateFrom && salesOrder.OrderDate < price.PriceValidateTo)
                                    {
                                        priceNow = price.Price1 ?? 0;
                                    }
                                    else
                                    {
                                        priceNow = 0;
                                    }
                                    var salesOrderDetail = new SalesOrderDetail
                                    {
                                        SalesOrderNo = salesOrder.SalesOrderNo,
                                        ProductCode = orderDetail.ProductCode,
                                        Qty = orderDetail.Qty,
                                        Price = priceNow
                                    };
                                    isError = false;
                                    await _salesOrderDetailRepository.Update(conn, salesOrderDetail, trans);
                                    response.Status = "success";
                                    response.SalesOrderNo = salesOrder.SalesOrderNo;
                                    response.Message = "Data has been updated successfully";
                                }
                            }
                            await _salesOrderRepository.Update(conn, salesOrder, trans);
                            trans.Commit();
                            return response;
                        }
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw new Exception(e.Message);
                    }
                }

            }

        }

        private string GenerateSalesOrderNumber()
        {
            //SO001
            string newCode = "SO" + 000;
            try
            {
                var getCode = _dbContext.SalesOrders
                    .Where(o => o.SalesOrderNo.Contains(newCode))
                    .OrderByDescending(o => o.SalesOrderNo)
                    .FirstOrDefault();
                if(getCode!=null)
                {
                    string refer = getCode.SalesOrderNo.Substring(2);
                    int incr = int.Parse(refer) + 1;
                    newCode += incr.ToString("D2");
                }
                else
                {
                    newCode += "001";
                }
            }
            catch (Exception)
            {
                newCode = "";
                throw;
            }
            return newCode;
        }

    }
}
