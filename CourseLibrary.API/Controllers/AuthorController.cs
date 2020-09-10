using AutoMapper;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _autoMapper;

        public AuthorController(
            ICourseLibraryRepository courseLibraryRepository,
            IMapper autoMapper
            )
        {
            _courseLibraryRepository = courseLibraryRepository ?? 
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _autoMapper = autoMapper ??
                throw new ArgumentNullException(nameof(autoMapper));
        }

        [HttpGet()]
        [HttpHead]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();            

            return Ok(_autoMapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);            
            if(authorFromRepo != null)
            {
                return Ok(authorFromRepo);
            }
            return NotFound();            
        }

    }
}
