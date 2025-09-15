using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controller.Interfaces;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Controller.Repository
{
    public class CommentRepository : ICommentRepository
    {

        private readonly ApplicationDBContext _context;
         public CommentRepository(ApplicationDBContext context)
        {
            _context = context;

        }
        public  Task<Comment> CraeteCommentAsync(Comment comment)
        {
              throw new NotImplementedException();
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
           
            await _context.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public Task<Comment?> DeleteCommentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            
          return await _context.Comments.FindAsync(id);
        }

        public Task<Comment?> UpdateCommentAsync(int id, Comment comment)
        {
            throw new NotImplementedException();
        }
    }
    }