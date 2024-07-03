namespace ApiPointOfSales.ViewModel
{
    public class ResponseSuccessCreateOrder
    {
        public string Status { get; set; } = default!;
        public string SalesOrderNo { get; set; } = default!;
        public string Message { get; set; } = default!;

    }
    public class ResponseOrder
    {
        public string Status { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
    public class ResponseGetOrUpdateOrder: RequestSalesOrder
    {
        public string SalesOrderNo { get; set; } = default!;
    }
}
