using GenshinPray.Dao;
using GenshinPray.Models.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service
{
    public class AuthorizeService
    {
        private AuthorizeDAO authorizeDao;

        public AuthorizeService()
        {
            this.authorizeDao = new AuthorizeDAO();
        }

        public AuthorizePO GetAuthorize(string code)
        {
            return authorizeDao.GetAuthorize(code);
        }



    }
}
