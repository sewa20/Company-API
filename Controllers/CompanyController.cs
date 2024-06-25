using Practice1.CompanyInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice1.Data;
using Practice1.DTO;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Practice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private IDbConnection db; //ApplicationDbContext _db;
        public CompanyController(IConfiguration configuration)//ApplicationDbContext context) 
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));//_db = context;

        }

        [HttpGet]
        public List<Info> GetAll()
        {
            var sql = "SELECT * FROM Infos";
            return db.Query<Info>(sql).ToList();//return _db.Infos.ToList();
        }

        [HttpGet("GetInfoById")] 
        public ActionResult<Info> GetInfo(Int32 Id ) 
        {

            var sql = "SELECT * FROM Infos WHERE Id = @Id";
            var resp = db.Query<Info>(sql, new { @Id = Id }).FirstOrDefault();
            if(resp == null)
            {
                return BadRequest("Request not found!");
            }
            else
            {
                return Ok(resp);
            }
            
            /*
            if(Id == 0)
            {
                return BadRequest();
            }

            var InfoDetails = _db.Infos.FirstOrDefault(x => x.Id == Id);

            if(InfoDetails == null)
            {
                return NotFound();
            }
            return InfoDetails;
            */
        }

        [HttpPost]
        public ActionResult<Info> AddInformation([FromBody] InfoRequestDTO InfoDetails)//[FromBody] /*InfoRequestDTO*/ /*InfoDetails*/)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                //var sql = "INSERT INTO Infos (FullName, Title, Address) INSERTED.Id VALUES(@FullName, @Title, @Address);"
                //    + "SELECT CAST(SCOPE_IDENTITY() as int); ";
                
                var sql = "INSERT INTO Infos (FullName, Title, Address) OUTPUT INSERTED.Id VALUES(@FullName, @Title, @Address);"
                    ;
                var id = db.QuerySingle<int>(sql, new
                {
                    InfoDetails.FullName,
                    InfoDetails.Title,
                    InfoDetails.Address
                });
                //}).Single();
                //InfoDetails.Id = id;
                return Ok(new Info() { Address= InfoDetails.Address,
                Title = InfoDetails.Title, FullName = InfoDetails.FullName,
                Id=id});
                /*
                _db.Infos.Add(new Info { FullName = InfoDetails.FullName, Title = InfoDetails.Title, Address = InfoDetails.Address });
                _db.SaveChanges();
                */
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            //return Ok(InfoDetails);
        }

        [HttpPost("UpdateCompanyInfo")]
        public ActionResult<Info> UpdateInformation(Int32 Id, /*[FromBody]*/ Info InfoDetails)
        {
            try
            {
                var sql = "UPDATE Infos SET FullName = @FullName, Title = @Title, Address = @Address WHERE Id = @Id";


                db.Execute(sql, InfoDetails);
                return (InfoDetails);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            /*
            if(InfoDetails == null)
            {
                return BadRequest(InfoDetails);
            }

            var infoDetails = _db.Infos.FirstOrDefault(y => y.Id == Id);
            if (InfoDetails == null)
            {
                return NotFound();
            }

            try
            {
                //infoDetails.FullName = InfoDetails.FullName;
                //infoDetails.Title = InfoDetails.Title;
                //infoDetails.Address = InfoDetails.Address;
                _db.Infos.Add(new Info { FullName = InfoDetails.FullName, Title = InfoDetails.Title, Address = InfoDetails.Address });
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            
            //_db.SaveChanges();
            return Ok(InfoDetails);

            */
        }


        [HttpDelete]
        public void /*ActionResult<Info>*/ DeleteInformation (Int32 Id)
        {

            var sql = "DELETE FROM Infos WHERE Id = @Id";
            db.Execute(sql, new { @Id = Id });
            
            //or { Id}
            /*
            var info = _db.Infos.FirstOrDefault(x => x.Id == Id);
            if(info == null)
            {
                return NotFound();
            }
            _db.Remove(info);

            _db.SaveChanges();
            return NoContent();
            */
        }







    }
}

/*
 * var sql = "DELETE FROM Infos WHERE Id = @Id";
 * db.Execute(sql, new{ @Id = Id}); or {Id}
 */
