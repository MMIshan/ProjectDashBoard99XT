﻿using ProDashBoard.Data;
using ProDashBoard.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProDashBoard.Api
{
    public class ProjectAccountController : ApiController
    {
        private AccountRepository repo;

        public ProjectAccountController()
        {
            repo = new AccountRepository();
        }

        [HttpGet, Route("api/Account")]

        public List<Account> Get()
        {
            return repo.Get();
        }

        [HttpGet, Route("api/Account/{id}")]
        public Account Get(int id)
        {
            return repo.Get(id);
        }

        [HttpGet, Route("api/Account/getInacativeAccounts")]
        public List<Account> getInactiveAccounts() {
           return repo.getInactiveAccounts();
        }


        [HttpGet, Route("api/Account/getSpec/{accountid}")]
        public Spec getSpec(int accountid)
        {
            Spec spec = null;
            Debug.WriteLine("gdfgfd" + spec + "gfd");
            if (repo.GetSpec(accountid) != null)
            {

                spec = repo.GetSpec(accountid);
            }

            if (spec != null)
            {
                Debug.WriteLine("Entered");
            }
            else
            {
                Debug.WriteLine("NotEntered");
            }

            return spec;
        }

        [HttpPost, Route("api/Account/add")]
        public int add([FromBody] Account account) {
            return repo.add(account);
        }
    }
}
