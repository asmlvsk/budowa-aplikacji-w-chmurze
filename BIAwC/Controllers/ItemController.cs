using BIAwC.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIAwC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly MongoDbContext _dataAccess;
        public ItemController(MongoDbContext dataAccess)
        {
            _dataAccess = dataAccess;
        }
        // GET: api/<TracksController>
        public IEnumerable<DbItem> GetItems()
        {
            return _dataAccess.GetAllItems();
        }

        // GET api/<TracksController>/5
        [HttpGet("{id}")]
        public async Task<DbItem> GetItem(string id)
        {
            return await _dataAccess.GetItemData(id) ?? new DbItem();
        }

        // POST api/<TracksController>
        [HttpPost]
        public void AddItem([FromBody] DbItem itemData)
        {
            _ = _dataAccess.AddItem(new DbItem
            {
                Id = itemData.Id,
                Description = itemData.Description,
                ImageUrl = itemData.ImageUrl,
                LastUpdate = itemData.LastUpdate,
                Title = itemData.Title,
                Url = itemData.Url,
                WebsiteUrl = itemData.WebsiteUrl
            });
        }

        // PUT api/<TracksController>/5
        [HttpPut]
        public void UpdItem([FromBody] DbItem itemData)
        {
            _dataAccess.UpdateItem(itemData);
        }

        // DELETE api/<TracksController>/5
        [HttpDelete("{id}")]
        public void DeleteItem(string id)
        {
            _ = _dataAccess.DeleteItem(id);
        }
    }
}
