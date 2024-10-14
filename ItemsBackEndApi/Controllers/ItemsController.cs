using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ItemsBackEndApi.Models;

// Define the ItemsController to handle HTTP requests related to items
[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    // In-memory list to store items
    private static List<Item> Items = new List<Item>
    {
        new Item { Id = 1, Name = "Item 1" },
        new Item { Id = 2, Name = "Item 2" }
    };

    // GET: api/items
    [HttpGet]
    public ActionResult<IEnumerable<Item>> GetItems()
    {
        // Return the list of items with a 200 OK status
        return Ok(Items);
    }

    // POST: api/items
    [HttpPost]
    public ActionResult AddItem(Item newItem)  // Use Item model for the request body
    {
        // Generate a new ID for the item based on existing items
        newItem.Id = Items.Count > 0 ? Items.Max(i => i.Id) + 1 : 1;
        // Add the new item to the list
        Items.Add(newItem);
        // Return 201 Created response with the newly added item
        return CreatedAtAction(nameof(GetItems), new { id = newItem.Id }, newItem);
    }

    // PUT: api/items/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateItem(int id, Item updatedItem)  // Use Item model for the request body
    {
        // Find the item by ID
        var item = Items.Find(i => i.Id == id);
        // If the item doesn't exist, return 404 Not Found
        if (item == null) return NotFound();

        // Update the item's properties with the new values
        item.Name = updatedItem.Name;
        // Return the updated item with a 200 OK status
        return Ok(item);
    }

    // DELETE: api/items/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteItem(int id)
    {
        // Find the item by ID
        var item = Items.Find(i => i.Id == id);
        // If the item doesn't exist, return 404 Not Found
        if (item == null) return NotFound();

        // Remove the item from the list
        Items.Remove(item);
        // Return 204 No Content status
        return NoContent();
    }
}
