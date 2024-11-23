namespace api.model
{
    public class supplier
    { 
        public int supplierid { get; set; }
        public string suppliername { get; set; }
        public string phone { get; set; }
        public string address {  get; set; }
        public ICollection<product> products { get; set; }
    }
}
