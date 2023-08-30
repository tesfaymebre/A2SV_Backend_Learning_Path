using System;
using System.Collections.Generic;

class Library
{
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Book> Books { get; } = new List<Book>();
    public List<MediaItem> MediaItems { get; } = new List<MediaItem>();

    public void AddBook(Book book)
    {
        Books.Add(book);
        Console.WriteLine($"Added book '{book.Title}' to the library.");
    }

    public void RemoveBook(Book book)
    {
        if (Books.Contains(book))
        {
            Books.Remove(book);
            Console.WriteLine($"Removed book '{book.Title}' from the library.");
        }
        else
        {
            Console.WriteLine($"Book '{book.Title}' is not in the library's collection.");
        }
    }

    public void AddMediaItem(MediaItem item)
    {
        MediaItems.Add(item);
        Console.WriteLine($"Added {item.MediaType} '{item.Title}' to the library.");
    }

    public void RemoveMediaItem(MediaItem item)
    {
        if (MediaItems.Contains(item))
        {
            MediaItems.Remove(item);
            Console.WriteLine($"Removed {item.MediaType} '{item.Title}' from the library.");
        }
        else
        {
            Console.WriteLine($"{item.MediaType} '{item.Title}' is not in the library's collection.");
        }
    }

    public void PrintCatalog()
    {
        Console.WriteLine($"Catalog for {Name} Library:");
        Console.WriteLine("Books:");
        foreach (Book book in Books)
        {
            Console.WriteLine($"{book.Title} by {book.Author}, ISBN: {book.ISBN}, Year: {book.PublicationYear}");
        }

        Console.WriteLine("\nMedia Items:");
        foreach (MediaItem item in MediaItems)
        {
            Console.WriteLine($"{item.MediaType} '{item.Title}', Duration: {item.Duration} minutes");
        }
    }

    public void Search(string searchTerm)
    {
        List<Book> matchingBooks = Books.Where(book =>
            book.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            book.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            book.ISBN.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
        ).ToList();

        List<MediaItem> matchingMediaItems = MediaItems.Where(item =>
            item.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            item.MediaType.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
        ).ToList();

        Console.WriteLine($"Search results for '{searchTerm}':");
        
        Console.WriteLine("\nMatching Books:");
        foreach (Book book in matchingBooks)
        {
            Console.WriteLine($"{book.Title} by {book.Author}, ISBN: {book.ISBN}, Year: {book.PublicationYear}");
        }

        Console.WriteLine("\nMatching Media Items:");
        foreach (MediaItem item in matchingMediaItems)
        {
            Console.WriteLine($"{item.MediaType} '{item.Title}', Duration: {item.Duration} minutes");
        }
    }

}

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }
}

class MediaItem
{
    public string Title { get; set; }
    public string MediaType { get; set; }
    public int Duration { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library { Name = "MyLibrary", Address = "123 Main St" };

        Book book1 = new Book { Title = "Book 1", Author = "Author 1", ISBN = "123456789", PublicationYear = 2022 };
        Book book2 = new Book { Title = "Book 2", Author = "Author 2", ISBN = "987654321", PublicationYear = 2021 };

        MediaItem dvd = new MediaItem { Title = "Movie 1", MediaType = "DVD", Duration = 120 };
        MediaItem cd = new MediaItem { Title = "Music Album", MediaType = "CD", Duration = 60 };

        library.AddBook(book1);
        library.AddBook(book2);
        library.AddMediaItem(dvd);
        library.AddMediaItem(cd);

        library.PrintCatalog();

        library.Search("Book 1");
        library.Search("DVD");
    }
}
