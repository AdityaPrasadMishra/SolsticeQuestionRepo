using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SolsticeQuestion.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SolsticeQuestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : Controller
    {
        private readonly ContactsDBContext _context;
        private const string filePath = "./Files/";
        public ContactsController(ContactsDBContext context)
        {
            _context = context;

            if (_context.ContactItems.Count() == 0)
            {
                // Create a new ContactsItem if collection is empty,
                // which means you can't delete all ContactItems.
                _context.ContactItems.Add(new ContactItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        // GET: api/Contacts
        //[Route("api/Contacts/GetContactItems")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactItem>>> GetContactItems()
        {
            return await _context.ContactItems.ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ContactItem>> GetContactItem(long id)
        {
            var contactItem = await _context.ContactItems.FindAsync(id);

            if (contactItem == null)
            {
                return NotFound();
            }

            return contactItem;
        }


        // GET: api/Contacts/byemailorphone?email=a&phone=b
        [Route("~/api/Contacts/byemailorphone")]
        [HttpGet("{byemailorphone}")]
        public async Task<ActionResult<IEnumerable<ContactItem>>> SearchContactItems(string email, string phone)
        {
            //string email = !String.IsNullOrEmpty(item1) ? item1 : "";
            //string phonenumber = !String.IsNullOrEmpty(item2) ? item2 : "";

            var contactItems = await _context.ContactItems.Where(con => con.Email == email
            || con.HomePhoneNumber == phone || con.WorkPhoneNumber == phone).ToListAsync();

            return contactItems;
        }


        // GET: api/Contacts/bystate?state=a
        [Route("~/api/Contacts/bystate")]
        [HttpGet("{bystate}")]
        public async Task<ActionResult<IEnumerable<ContactItem>>> SearchContactItemsForState(string state)
        {
            //string email = !String.IsNullOrEmpty(item1) ? item1 : "";
            //string phonenumber = !String.IsNullOrEmpty(item2) ? item2 : "";

            var contactItems = await _context.ContactItems.Where(con => con.State == state).ToListAsync();

            return contactItems;
        }

        // GET: api/Contacts/bycity?city=b
        [Route("~/api/Contacts/bycity")]
        [HttpGet("{bycity}")]
        public async Task<ActionResult<IEnumerable<ContactItem>>> SearchContactItemsForCity(string city)
        {
            //string email = !String.IsNullOrEmpty(item1) ? item1 : "";
            //string phonenumber = !String.IsNullOrEmpty(item2) ? item2 : "";

            var contactItems = await _context.ContactItems.Where(con => con.City == city).ToListAsync();

            return contactItems;
        }


        [HttpPost]
        public async Task<ActionResult<ContactItem>> PostContactItem([FromForm]ContactItem item,IFormFile profileImage)
        {
            if (profileImage != null && profileImage.Length > 0)
            {
                item.ProfileImage = handleFile(profileImage);
            }

            _context.ContactItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContactItem), new { id = item.Id }, item);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactItem( [FromForm]ContactItem item, long id, IFormFile profileImage)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            if (profileImage != null && profileImage.Length > 0)
            {
                item.ProfileImage = handleFile(profileImage);
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactItem(long id)
        {
            string fileName = "";
            var contactItem = await _context.ContactItems.FindAsync(id);
            if (contactItem.ProfileImage != null)
            {
                fileName = filePath + contactItem.ProfileImage;
            }

            if (contactItem == null)
            {
                return NotFound();
            }

            _context.ContactItems.Remove(contactItem);
            await _context.SaveChangesAsync();

            if (fileName  != "" && (System.IO.File.Exists(fileName)))
            {
                System.IO.File.Delete(fileName);
            }
            return NoContent();
        }

        private string handleFile(IFormFile profileImage)
        {
            
            Directory.CreateDirectory(filePath);
            string[] splarr = profileImage.FileName.Split(".");
            string extension = splarr[splarr.Length - 1];
            string newfilename = $"{Guid.NewGuid().ToString()}.{extension}";

                using (var stream = new FileStream(filePath + newfilename, FileMode.Create))
                {
                    profileImage.CopyTo(stream);
                }
            
            return newfilename;

        }
    }

}
