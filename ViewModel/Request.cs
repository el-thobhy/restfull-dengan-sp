namespace ApiPointOfSales.ViewModel
{
    public class RequestSalesOrder
    {
        public string CustId { get; set; } = default!;
        public List<RequestSalesOrderDetail> OrderDetail { get; set; } = default!;
    }
    public class RequestSalesOrderDetail
    {
        public string ProductCode { get; set; }= default!;
        public int Qty { get; set; }
    }
    public class RequestSalesById
    {
        public string SalesOrderNo { get; set; } = default!;
    }
}
