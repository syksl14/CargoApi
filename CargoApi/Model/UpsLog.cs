namespace CargoApi.Model
{
    public class UpsLog
    {
        public String? Tarih { get; set; }
        public String? Bilgi { get; set; }
        public String? IslemYeri { get; set; }
        public UpsLog()
        {

        }

        public UpsLog(String? tarih, string? bilgi, string? islemYeri)
        {
            Tarih = tarih;
            Bilgi = bilgi;
            IslemYeri = islemYeri;
        }
    }
}
