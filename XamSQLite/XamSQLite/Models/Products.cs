using SQLite;

namespace XamSQLite.Models
{
    public class Products
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        
    }
}