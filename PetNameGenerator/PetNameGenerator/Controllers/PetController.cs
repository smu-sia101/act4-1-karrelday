using Microsoft.AspNetCore.Mvc;

namespace PetNameGenerator.Controllers
{
    [ApiController]
    [Route("api/petname")]
    public class PetcController : ControllerBase
    {
        private static readonly List<string> dog = new() { "Buddy", "Max", "Charlie", "Rocky", "Rex" };
        private static readonly List<string> cat = new() { "Whiskers", "Mittens", "Luna", "Simba", "Tiger" };
        private static readonly List<string> bird = new() { "Tweety", "Sky", "Chirpy", "Raven", "Sunny" };

        [HttpPost("generate")]
        public IActionResult GenerateName([FromBody] PetnameRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.AnimalType))
            {
                return BadRequest(new { error = "The 'AnimalType' field is required." });
            }

            List<string> names;
            switch (request.AnimalType.ToLower())
            {
                case "dog":
                    names = dog;
                    break;
                case "cat":
                    names = cat;
                    break;
                case "bird":
                    names = bird;
                    break;
                default:
                    return BadRequest(new { error = "Invalid animal type. Allowed values: dog, cat, bird." });
            }

            Random rnd = new Random();
            string generatedName = names[rnd.Next(names.Count)];

            if (request.TwoPart == true)
            {
                generatedName += names[rnd.Next(names.Count)];
            }

            return Ok(new { name = generatedName });
        }
    }

}

    public class PetnameRequest
    {
        public string AnimalType { get; set; }
        public bool? TwoPart { get; set; }
    }
