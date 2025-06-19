using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rilevazioniPresenzaData.Models;
using rilevazioniPresenza.Reps.UserFiles;
using rilevazioniPresenze.DTOs;
using System.Security.Cryptography;
using System.Text;

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

            if (user == null)
                return NotFound();

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
                Username=user.Username
            };
            return Ok(detailsUser);
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
        public bool AddEmployer(DetailUserDTOs employerDTOs)
        {
            MD5 mD5 = MD5.Create();
            byte[] defaultBytes = Encoding.UTF8.GetBytes("password");
            byte[] hashBytes = MD5.HashData(defaultBytes);
            User employer = new User
            {
                Matricola = employerDTOs.Matricola,
                Badge = employerDTOs.Badge,
                Nominativo = employerDTOs.Nominativo,
                Sesso = employerDTOs.Sesso,
                Stato_Civile = employerDTOs.Stato_Civile,
                Data_Nascita = employerDTOs.Data_Nascita,
                Citta_Nascita = employerDTOs.Citta_Nascita,
                Provincia_Nascita = employerDTOs.Provincia_Nascita,
                Stato_Nascita = employerDTOs.Stato_Nascita,
                Indirizzo_Residenza = employerDTOs.Indirizzo_Residenza,
                Provincia_Residenza = employerDTOs.Provincia_Residenza,
                Stato_Residenza = employerDTOs.Stato_Residenza,
                Stato_Lavorativo = employerDTOs.Stato_Lavorativo,
                Codice_Fiscale = employerDTOs.Codice_Fiscale,
                Numero_Telefono = employerDTOs.Numero_Telefono,
                Mail = employerDTOs.Mail,
                Username = employerDTOs.Username,
                Password = Convert.ToHexString(hashBytes).ToLower()
            };
            bool add = _repo.AddEmp(employer);
            return add;
        }

        [HttpDelete("{key}")]
        public bool RemoveEmployer(string key)
        {
            bool remove = _repo.RemoveEmp(key);
            return remove;
        }

        [HttpPut]
        public bool UpdateEmployer(DetailUserDTOs employer)
        {
            bool update = _repo.UpdateEmp(employer);
            return update;
        }
    }
}
