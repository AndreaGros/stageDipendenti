using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rilevazioniPresenzaData.Models;
using rilevazioniPresenzaData.Reps.UserFiles;
using rilevazioniPresenze.DTOs;

namespace rilevazioniPresenze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{key}")]
        public IActionResult GetAllDetails([FromRoute] string key)
        {
            User? user = _repo.GetAllDetailsByKey(key);

            if (user != null)
            {

                DetailUserDTOs detailsUser = new DetailUserDTOs
                {
                    Matricola = user.Matricola,
                    Badge = user.Badge,
                    Nominativo = user.Nominativo,
                    Sesso = user.Sesso,
                    Stato_Civile = user.Stato_Civile,
                    Data_Nascita = user.Data_Nascita,
                    Citta_Nascita = user.Citta_Nascita,
                    Provincia_Nascita = user.Provincia_Nascita,
                    Stato_Nascita = user.Stato_Nascita,
                    Indirizzo_Residenza = user.Indirizzo_Residenza,
                    Provincia_Residenza = user.Provincia_Residenza,
                    Stato_Residenza = user.Stato_Residenza,
                    Codice_Fiscale = user.Codice_Fiscale,
                    Numero_Telefono = user.Numero_Telefono,
                    Stato_Lavorativo = user.Stato_Lavorativo,
                    Mail = user.Mail,
                };
                return Ok(detailsUser);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult GetGeneralDetails()
        {
            List<GeneralUserDTOs> usersDTOs = new();
            List<User> users = _repo.GetEmps();

            foreach (var user in users)
            {
                usersDTOs.Add(new GeneralUserDTOs
                {
                    Matricola = user.Matricola,
                    Badge = user.Badge,
                    Nominativo = user.Nominativo,
                    Citta_Nascita = user.Citta_Nascita,
                    Data_Nascita = user.Data_Nascita,
                    Indirizzo_Residenza = user.Indirizzo_Residenza,
                    Numero_Telefono = user.Numero_Telefono,
                    Stato_Lavorativo = user.Stato_Lavorativo
                });
            }

            return usersDTOs switch
            {
                null => NotFound(),
                _ => Ok(usersDTOs)
            };
        }

        [HttpPost]
        public bool AddEmployer(User employer)
        {
            bool add = _repo.AddEmp(employer);
            if (add)
                return true;
            return false;
        }

        [HttpDelete("{key}")]
        public bool RemoveEmployer(string key)
        {
            bool remove = _repo.RemoveEmp(key);
            if (remove)
                return true;
            return false;
        }

        [HttpPut]
        public bool UpdateEmployer(User employer)
        {
            bool update = _repo.UpdateEmp(employer);
            if (update)
                return true;
            return false;
        }
    }
}
