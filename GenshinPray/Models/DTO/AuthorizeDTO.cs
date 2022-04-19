using GenshinPray.Models.PO;

namespace GenshinPray.Models.DTO
{
    public class AuthorizeDTO
    {
        public AuthorizePO AuthorizePO { get; set; }

        public int PrayTimesToday { get; set; }

        public AuthorizeDTO() { }

        public AuthorizeDTO(AuthorizePO AuthorizePO, int prayTimesToday)
        {
            this.AuthorizePO = AuthorizePO;
            this.PrayTimesToday = prayTimesToday;
        }


    }
}
