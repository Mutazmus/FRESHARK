using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controller.Interfaces;
using api.Data;
using api.Models;
using api.Models.Dtos.CommetsDto;
using api.Models.Mappers;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{

    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;
        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepository.GetAllCommentsAsync();

            var commentDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentDto);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommetDto CommentDto)
        {

             if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!await _stockRepository.StockExist(stockId))
            {
                return BadRequest("Stock Does not Exist");
            }

            var commentModel = CommentDto.ToCommentFromCreate(stockId);

            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.UpdateCommentAsync(id, updateCommentDto.ToCommentFromUpdate());

            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = await _commentRepository.DeleteCommentAsync(id);
            
            if (commentModel == null)
            {
                return NotFound("Comment does not exit");
            }

            return Ok(commentModel);
        }

    }
}