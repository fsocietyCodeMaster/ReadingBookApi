using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ReadingBookApi.Context;
using ReadingBookApi.Model;
using ReadingBookApi.Repository;
using System.Net;


namespace ReadingBookApi.Service
{
    public class BookService : IBook
    {
        private readonly BookDbContext _context;
        private readonly IMapper _map;

        public BookService(BookDbContext context,IMapper map)
        {
            _context = context;
            _map = map;
        }
        public async Task<ResponseVM> AddBook(BookDetailVM book)
        {
            if(book == null)
            {
                var error = new ResponseVM
                {
                    Message = "Something is wrong.",
                    IsSuccess = false,
                    Status = HttpStatusCode.BadRequest.ToString()
                };
                return error;
            }
            else
            {
                var bookMapped = _map.Map<T_Book>(book);
                _context.Add(bookMapped);
                await _context.SaveChangesAsync();

                var success = new ResponseVM
                {
                    Message = $"The Book {book.Title} successfully added.",
                    IsSuccess = true,
                    Status = HttpStatusCode.OK.ToString(),
                };
                return success;
            }

        }


        public async Task<ResponseVM> Get(Guid id, int page, int size)
        {
            var book =  await _context.t_Books.Include(c=>c.review_Ratings).FirstOrDefaultAsync(c => c.BookId == id);
            if(book != null)
            {
                var reviewPaginated = book.review_Ratings.Skip(((page - 1) * size)).Take(size).ToList();
                var bookVm = _map.Map<BookDetailVM>(book);
                bookVm.reviews = _map.Map<ICollection<ReviewVM>>(reviewPaginated);
                var success = new ResponseVM
                {
                    Message = $"The Book {book.Title} successfully retrieved .",
                    IsSuccess = true,
                    Status = HttpStatusCode.OK.ToString(),
                    Data = new {response = bookVm}
                };
                return success;
            }
            else
            {
                var error = new ResponseVM
                {
                    Message = "Nothing found.",
                    IsSuccess = false,
                    Status = HttpStatusCode.NotFound.ToString()
                };
                return error;
            }
        }

       
        
        public async Task<ResponseVM> Update(Guid id, BookDetailVM book)
        {
            if(book == null)
            {
                var error = new ResponseVM
                {
                    Message = "Nothing found.",
                    IsSuccess = false,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Data = new BookDetailVM { }
                };
                return error;

            }
            else
            {
                var bookUpdate = await _context.t_Books.FirstOrDefaultAsync(c => c.BookId == id);
                if(bookUpdate != null)
                {
                   var bookUpdated =_map.Map(book,bookUpdate);
                    await _context.SaveChangesAsync();

                    var success = new  ResponseVM
                    {
                        Message = $"The Book {bookUpdate.Title} successfully updated .",
                        IsSuccess = true,
                        Status = HttpStatusCode.OK.ToString(),
                        Data = new { response = _map.Map<BookDetailVM>(bookUpdated) }
                    };
                    return success;

                }
                else
                {
                    var error = new ResponseVM
                    {
                        Message = "Nothing found.",
                        IsSuccess = false,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Data = new BookDetailVM { }
                    };
                    return error;
                }


            }


        }



        public async Task<ResponseVM> Delete(Guid id)
        {
            var book = await _context.t_Books.FirstOrDefaultAsync(c => c.BookId == id);
            if (book == null)
            {
                var error = new ResponseVM
                {
                    Message = "Something is wrong.",
                    IsSuccess = false,
                    Status = HttpStatusCode.BadRequest.ToString()
                };
                return error;
            }
            else
            {
                _context.Remove(book);

                var success = new ResponseVM
                {
                    Message = $"The Book {book.Title} successfully deleted.",
                    IsSuccess = true,
                    Status = HttpStatusCode.OK.ToString()
                };
                return success;
            }
        }

        public async Task<ResponseVM> UpdatePartial(Guid id, JsonPatchDocument<BookDetailVM> book)
        {
            if (book == null)
            {
                var error = new ResponseVM
                {
                    Message = "Nothing found.",
                    IsSuccess = false,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Data = new BookDetailVM { }
                };
                return error;

            }
            else
            {
                
                var bookDb = await _context.t_Books.FirstOrDefaultAsync(c => c.BookId == id);
                if (bookDb != null)
                {

                    var bookMapped = _map.Map<BookDetailVM>(bookDb);

                    book.ApplyTo(bookMapped);

                    var bookPatched = _map.Map(bookMapped, bookDb);
                    await _context.SaveChangesAsync();


                    var success = new ResponseVM
                    {
                        Message = $"The Book {bookPatched.Title} successfully updated .",
                        IsSuccess = true,
                        Status = HttpStatusCode.OK.ToString(),
                        Data = new { response = _map.Map<BookDetailVM>(bookPatched) }
                    };
                    return success;

                }
                else
                {
                    var error = new ResponseVM
                    {
                        Message = "Nothing found.",
                        IsSuccess = false,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Data = new BookDetailVM { }
                    };
                    return error;
                }


            }
        }

        public async Task<ResponseVM> GetBySearch(string filter)
        {
            var books = await _context.t_Books.Where(c => c.Genre.Contains(filter) || c.Author.Contains(filter) || c.Title.Contains(filter)).Distinct().ToListAsync();
            if (books != null || books.Any())
            {
                var success = new ResponseVM
                {
                    Message = "Search successfully done .",
                    IsSuccess = true,
                    Status = HttpStatusCode.OK.ToString(),
                    Data = new { response = _map.Map<IEnumerable<BookDetailVM>>(books) }
                };
                return success;
            }
            else
            {
                var error = new ResponseVM
                {
                    Message = "Nothing found.",
                    IsSuccess = false,
                    Status = HttpStatusCode.NotFound.ToString()
                };
                return error;
            }

        }

        public async Task<ResponseVM> GetAllByPage(int page, int size)
        {
            var books = await _context.t_Books.Include(c => c.review_Ratings).Skip(((page - 1) * size)).Take(size).ToListAsync();
            if (books != null)
            {

                foreach (var review in books)
                {
                    double totalRate = 0;
                    var numberOfRating = 0;

                    foreach (var rate in review.review_Ratings)
                    {
                        totalRate += rate.Rating;
                        numberOfRating++;
                        
                    }
                    if (numberOfRating > 0)
                    {
                        var averageRating = totalRate / numberOfRating;
                        review.AverageRating = Math.Round(averageRating,1);
                     
                    }
                    else
                    {
                        review.AverageRating = 0;
                    }
                    
                }
                await _context.SaveChangesAsync();
                
                var success = new ResponseVM
                {
                    Message = "Books successfully retrieved .",
                    IsSuccess = true,
                    Status = HttpStatusCode.OK.ToString(),
                    Data = new { response = _map.Map<IEnumerable<BookSummaryVM>>(books) }
                };
                return success;
            }
            else
            {
                var error = new ResponseVM
                {
                    Message = "Nothing found.",
                    IsSuccess = false,
                    Status = HttpStatusCode.NotFound.ToString()
                };
                return error;
            }
        }

        public async Task<T_Book> GetBookId(Guid id)
        {
            return await _context.t_Books.FindAsync(id);
            
        }
    }
}
