namespace TPHDotNetCore.PizzaApi.Queries
{
    public class PizzaQuery
    {
        public static string PizzaOrderQuery { get; } =
            @"select * from [dbo].[Tbl_PizzaOrder] po
                Inner join Tbl_Pizza p on p.PizzaId = po.PizzaId
                where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";


        public static string PizzaOrderDetailQuery { get; } =
            @"select * from [dbo].[Tbl_PizzaOrderDetail] pd
                Inner join Tbl_PizzaExtra pe on pe.PizzaExtraId = pd.PizzaExtraId
                where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";
    }
}
