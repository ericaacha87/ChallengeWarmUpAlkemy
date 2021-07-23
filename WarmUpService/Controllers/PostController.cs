using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarmUpService.DataAccessLayer;
using WarmUpService.Models;
using WarmUpService.DTO;
using AutoMapper;
using System.Net.Http;
using System.Net;

namespace WarmUpService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly WarmUpContext _context;

        private readonly IMapper _mapper;


        public PostController(WarmUpContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Post
      
        [HttpGet]
        public IEnumerable<GetAllDTO> GetAll()
        {
            ICollection<Post> posts = _context.Posts.OrderByDescending(x => x.FechaCreacion).ToList();
            List<GetAllDTO> postsDTO = new List<GetAllDTO>();
            foreach (Post post in posts)
            {
                GetAllDTO objGetPosts = _mapper.Map<GetAllDTO>(post);
                postsDTO.Add(objGetPosts);
            }
            return postsDTO;
        }


        /*  Deberá buscar un post.Si existe, devolver sus detalles, caso contrario devolver un mensaje
  de error con el código de estado HTTP que corresponda.*/
        // GET: api/Post/5
   
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost([FromRoute] int id, [FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Post
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePost createPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Post post = _mapper.Map<Post>(createPost);
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

           // return CreatedAtAction("GetPost", new { id = post.Id }, post);
            return Ok();
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}