namespace CargoApi.Model
{
    public class Data
    {
        public List<UpsLog>? Hareketler { get; set; }    
        public String? SonDurum { get;set; }

        public Data(List<UpsLog>? hareketler, string? sonDurum)
        {
            Hareketler = hareketler;
            SonDurum = sonDurum;
        }
    }
}
