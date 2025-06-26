using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rilevazioniPresenza.Reps.UserFiles;
using rilevazioniPresenzaData.Models;
using rilevazioniPresenze.DTOs;
using System.Security.Claims;
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
        //[Authorize]
        public IActionResult GetAllDetails([FromRoute] string key)
        {
            User? user = _repo.GetAllDetailsByKey(key);

            if (user == null)
                return NotFound();

            ICollection<UserShiftDTOs> userShiftDTOs = [];

            foreach (var userShift in user.UserShifts)
            {
                userShiftDTOs.Add(new UserShiftDTOs
                {
                    Giorno = userShift.Giorno,
                    T1 = userShift.T1,
                    FT1 = userShift.FT1,
                    T2 = userShift.T2,
                    FT2 = userShift.FT2
                });
            }

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
                Username = user.Username,
                UserShifts = userShiftDTOs
            };
            return Ok(detailsUser);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetGeneralDetails(string? Stato_Lavorativo, string? Citta_Nascita, string? Provincia_Residenza)
        {
            List<GeneralUserDTOs> usersDTOs = new();
            var role = User.FindFirst(ClaimTypes.Role)!;

            FilterDTOs filters = new FilterDTOs
            {
                Stato_Lavorativo = Stato_Lavorativo,
                Citta_Nascita = Citta_Nascita,
                Provincia_Residenza = Provincia_Residenza
            };

            List<User> users = _repo.GetEmps(role.Value, filters);

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
        [Authorize]
        public bool AddEmployer(DetailUserDTOs employerDTOs)
        {
            //MD5 mD5 = MD5.Create();
            //byte[] defaultBytes = Encoding.UTF8.GetBytes("password");
            //byte[] hashBytes = MD5.HashData(defaultBytes);


            //User employer = new User
            //{
            //    Matricola = employerDTOs.Matricola,
            //    Badge = employerDTOs.Badge,
            //    Nominativo = employerDTOs.Nominativo,
            //    Sesso = employerDTOs.Sesso,
            //    Stato_Civile = employerDTOs.Stato_Civile,
            //    Data_Nascita = employerDTOs.Data_Nascita,
            //    Citta_Nascita = employerDTOs.Citta_Nascita,
            //    Provincia_Nascita = employerDTOs.Provincia_Nascita,
            //    Stato_Nascita = employerDTOs.Stato_Nascita,
            //    Indirizzo_Residenza = employerDTOs.Indirizzo_Residenza,
            //    Provincia_Residenza = employerDTOs.Provincia_Residenza,
            //    Stato_Residenza = employerDTOs.Stato_Residenza,
            //    Stato_Lavorativo = employerDTOs.Stato_Lavorativo,
            //    Codice_Fiscale = employerDTOs.Codice_Fiscale,
            //    Numero_Telefono = employerDTOs.Numero_Telefono,
            //    Mail = employerDTOs.Mail,
            //    Username = employerDTOs.Username,
            //    Password = Convert.ToHexString(hashBytes).ToLower()
            //};

            //bool add = _repo.AddEmp(employer, employerDTOs.UserShifts.ToList());

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
                Password = Convert.ToHexString(hashBytes).ToLower(),
                Admin=employerDTOs.Admin,
                UserShifts = employerDTOs.UserShifts.Select(u => new UserShift
                {
                    IdMatricola = employerDTOs.Matricola,
                    Giorno = u.Giorno,
                    T1 = u.T1,
                    FT1 = u.FT1,
                    T2 = u.T2,
                    FT2 = u.FT2
                }).ToList()
            };

            bool add = _repo.AddEmp(employer);
            return add;
        }

        [HttpDelete("{key}")]
        [Authorize(Roles = "admin")]
        public bool RemoveEmployer(string key)
        {
            bool remove = _repo.RemoveEmp(key);
            return remove;
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public bool UpdateEmployer(DetailUserDTOs employer)
        {
            bool update = _repo.UpdateEmp(employer);
            return update;
        }

        [HttpGet("username")]
        [Authorize]
        public IActionResult GetEmployer()
        {
            var usernameClaim = User.FindFirst(ClaimTypes.NameIdentifier)!;
            string username = usernameClaim.Value;
            User? user = _repo.GetUserByUsername(username);

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
                Username = user.Username,
            };
            return Ok(detailsUser);
        }

        [HttpGet("ids")]
        public IActionResult GetAllIds()
        {
            return Ok(_repo.GetIds());
        }

        [HttpGet("shifts/{key}")]
        public IActionResult GetShifts(string key)
        {
            return Ok(_repo.GetShiftsByKey(key));
        }
    }
}
