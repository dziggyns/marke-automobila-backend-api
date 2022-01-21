using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ZavrsniMojaPriprema1.Interfaces;
using ZavrsniMojaPriprema1.Models;

namespace ZavrsniMojaPriprema1.Controllers
{
    public class StatistikaController : ApiController
    {
        IStatistikaRepository _repository { get; set; }

        public StatistikaController(IStatistikaRepository repository)
        {
            _repository = repository;
        }


        [ResponseType(typeof(IQueryable<AutomobilDTO>))]
        [Route("api/najslabiji")]
        public IHttpActionResult GetNajslabiji()
        {
            return Ok(_repository.PrikaziNajslabije().ProjectTo<AutomobilDTO>());
        }


        [ResponseType(typeof(IEnumerable<StatistikaDTO>))]
        [Route("api/statistika")]
        public IHttpActionResult GetStatistika()
        {
            return Ok(_repository.PrikaziStatistiku());
        }


        [ResponseType(typeof(IEnumerable<MarkeProsekDTO>))]
        [Route("api/markeprosek")]
        public IHttpActionResult GetMarkeProsek()
        {
            return Ok(_repository.PrikaziMarkeProsek());
        }


        [ResponseType(typeof(IEnumerable<TopDrzaveDTO>))]
        [Route("api/topdrzave")]
        public IHttpActionResult GetTopDrzave()
        {
            return Ok(_repository.PrikaziTopDrzave());
        }

    }
}
