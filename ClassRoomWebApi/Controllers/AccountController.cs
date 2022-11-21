using ClassRoomWebApi.Entities;
using ClassRoomWebApi.Models;
using ClassRoomWebApi.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRoomWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{

    // Implemintation qilad IUserStore va databazaga saqlab , oqib beradi.
    private readonly UserManager<IdentityStudent> _userManager;
    // Token ochib cookiega yozib jonatadi
    private readonly SignInManager<IdentityStudent> _signInManager;



    public AccountController(UserManager<IdentityStudent> userManager, SignInManager<IdentityStudent> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
     


    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpDto signUpDto)
    {
        if (!ModelState.IsValid) 
            return BadRequest();

  /*      if (!await _userManager.Users.AnyAsync(user => user.UserName == signUpDto.UserName))
            return NotFound();*/

        if (signUpDto.Password != signUpDto.ConfirmPassword) 
            return BadRequest();


        // Using Mapster converting (Without custom)
        var student = signUpDto.Adapt<IdentityStudent>();

        // Save database
        await _userManager.CreateAsync(student, signUpDto.Password);

        // Token ochib cookiega yozib jonatadi
        await _signInManager.SignInAsync(student, isPersistent: true);

        return Ok();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInDto signInDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (!await _userManager.Users.AnyAsync(user => user.UserName == signInDto.UserName))
            return NotFound();

        var result = await _signInManager.PasswordSignInAsync(signInDto.UserName, signInDto.Password, isPersistent: true, false);

        if (!result.Succeeded)
            return BadRequest();

        return Ok();
    }

    [HttpGet("{username}")]
    [Authorize]
    public async Task<IActionResult> Profile(string username)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user.UserName != username)
            return NotFound();


        var userDto = user.Adapt<UserInfoDto>();

        return Ok(userDto);

    }

}
