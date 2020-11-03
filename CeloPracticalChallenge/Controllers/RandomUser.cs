using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CeloPracticalChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomUser : ControllerBase
    { 
        private readonly ILogger<RandomUser> _logger;
        public RandomUser(ILogger<RandomUser> logger)
        {
            _logger = logger;
        }

        private RandomUserGenerator _userGenerator = new RandomUserGenerator();

        [HttpGet]
        [Route("one")]
        public User Get()
        {
            return _userGenerator.CreateRandomUser();
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<User> GetAll()
        { 
            return Enumerable.Range(1, 5).Select(index => _userGenerator.CreateRandomUser())
            .ToArray();
        }

        [HttpGet]
        [Route("limit/{limitNumber}")]
        public IEnumerable<User> Get(int limitNumber)
        {
            return Enumerable.Range(1, limitNumber).Select(index => _userGenerator.CreateRandomUser())
            .ToArray();
        }

        [HttpGet]
        [Route("search/{name}")]
        public string Get(string name)
        {
            // search in the "database" for the name and return 
            return "hey " + name;
        }
    }
}
