using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib
{
  public class Book
  {
    public string Title { get; set; }
    public List<Author> Authors { get; set; }
  }

  public class Author
  {
    public string Name { get; set; }
  }

  // when you are updating the Title property of the Book you will be doing it through the
  // Title property of the corresponding view model that will raise the PropertyChanged event,
  // which will trigger the UI update.
  //

  public class AuthorViewModel
  {
    public AuthorViewModel(string name)
    {
      Name = name;
    }

    public string Name { get; set; }
  }

  public class BookViewModel : INotifyPropertyChanged
  {
    private Book book;

    public Book Book
    {
      get { return book; }
    }

    public string Title
    {
      get { return Book.Title; }
      set
      {
        Book.Title = value;
        // NotifyPropertyChanged string parameter explicit "Title" is optional below
        // because of [CallerMemberName] attribute usage
        //
        NotifyPropertyChanged(); 
      }
    }

    public List<AuthorViewModel> Authors { get; set; }

    public BookViewModel(Book book)
    {
      this.book = book;

      var authorvms = new List<AuthorViewModel>();
      foreach (var a in book.Authors)
      {
        authorvms.Add(new AuthorViewModel(a.Name));
      }

      Authors = authorvms;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }

  public class BookProvider
  {
    private ObservableCollection<BookViewModel> _books = null;
    public BookProvider()
    {
      _books = GetBooks();
    }

    public ObservableCollection<BookViewModel> Books
    {
      get
      {
        return _books;
      }
    }

    public ObservableCollection<BookViewModel> GetBooks()
    {
      ObservableCollection<BookViewModel> books = new ObservableCollection<BookViewModel>();

      books.Add(new BookViewModel(new Book
      {
        Title = "You Gotta Have Wa",
        Authors = new List<Author> { new Author { Name = "Bob Horner" }, new Author { Name = "Ichiro Suzuki" } }
      }));

      books.Add(new BookViewModel(new Book
      {
        Title = "Mourt's Relation",
        Authors = new List<Author> { new Author { Name = "Edward Winslow" }, new Author { Name = "William Bradford" } }
      }));

      return books;
    }
  }
}
