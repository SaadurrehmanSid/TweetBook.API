using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
     public interface IPostService
    {
         Task<List<Post>> GetAllAsync();

       Task<Post> GetByIdAsync(Guid postId);

        Task<bool> UpdatePostAsync([FromBody] Post postToUpdate);
       Task<bool> DeletePostAsync(Guid postId);

        Task<bool> CreatePostAsync([FromBody] Post post);


    }
}
