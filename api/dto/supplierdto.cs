namespace api.dto
{
    public class supplierdto
    {
        public int supplierid { get; set; }
        public string suppliername { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int p_count {  get; set; }
        public string[] products { get; set; }
    }
}
