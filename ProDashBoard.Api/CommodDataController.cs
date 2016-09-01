using ProDashBoard.Data;
using ProDashBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProDashBoard.Api
{
    public class CommodDataController : ApiController
    {
        private CommonDataRepository repo;

        public CommodDataController()
        {
            repo = new CommonDataRepository();
        }

        [HttpGet, Route("api/CommonData/{projectId}")]

        public CommonData Get(int projectId)
        {
            return repo.getSelectedProjectCommonData(projectId);
        }
    }
}
