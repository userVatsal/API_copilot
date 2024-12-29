using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController :
    ControllerBase
{
    private static List<User> Users =
        new List<User>();

    // Create a new user
    [HttpPost]
    public IActionResult Create(
        User user
    )
    {
        user.Id =
            Users.Count + 1;

        if (
            string.IsNullOrWhiteSpace(user.FirstName) ||
            string.IsNullOrWhiteSpace(user.LastName) ||
            string.IsNullOrWhiteSpace(user.Email) ||
            string.IsNullOrWhiteSpace(user.Department)
        )
        {
            return BadRequest();
        }

        Users
            .Add(
                user
            );

        return CreatedAtAction(
            nameof(GetById),
            new { id = user.Id }, user
        );
    }

    // Retrieve all users
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Users);
    }

    // Retrieve a user by ID
    [HttpGet("{id}")]
    public IActionResult GetById(
        int id
    )
    {
        try
        {
            var user =
                Users
                    .FirstOrDefault(
                        u => u.Id == id
                    );

            if (
                user == null
            )
            {
                return NotFound();
            }

            return Ok(user);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    // Update a user
    [HttpPut("{id}")]
    public IActionResult Update(
        int id,
        User updatedUser
    )
    {
        try
        {
            var user =
                Users
                    .FirstOrDefault(
                        u => u.Id == id
                    );

            if (
                user == null
            )
            {
                return NotFound();
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.Department = updatedUser.Department;

            return NoContent();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    // Delete a user
    [HttpDelete("{id}")]
    public IActionResult Delete(
        int id
    )
    {
        try
        {
            var user =
                Users
                    .FirstOrDefault(
                            u => u.Id == id
                        );

            if (
                user == null
            )
            {
                return NotFound();
            }

            Users
                .Remove(
                    user
                );

            return NoContent();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}

