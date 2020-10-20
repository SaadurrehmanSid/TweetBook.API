using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TweetBook.Contracts.V1;
using TweetBook.Data;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class PostRepo : IPostService
    {
        private AppDbContext _db;

        public PostRepo(AppDbContext db)
        {
            _db = db;
        }

       
        public async Task<List<Post>> GetAllAsync()
        {
            return await _db.Posts.ToListAsync();
        }

        public async Task<Post> GetByIdAsync(Guid postId)
        {
            return await _db.Posts.SingleOrDefaultAsync(x=>x.Id == postId);
        }

        public async Task<bool> UpdatePostAsync( [FromBody] Post postToUpdate)
        {
           
            _db.Posts.Update(postToUpdate);
            var updated = await  _db.SaveChangesAsync();
            if (updated > 0)
                return true;
            else
                return false;

        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(x=>x.Id == postId);

            if (post == null)
                return false;
            _db.Posts.Remove(post);
           var deleted = await _db.SaveChangesAsync();

            if (deleted > 0)
                return true;
            else
                return false;
           
        }

        public async Task<bool> CreatePostAsync([FromBody] Post post)
        {
            _db.Posts.Add(post);
           var created = await _db.SaveChangesAsync();
            if (created > 0)
                return true;
            else
                return false;
        }
    }
}
