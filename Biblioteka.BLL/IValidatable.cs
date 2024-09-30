using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Biblioteka.Entities;

namespace Biblioteka.BLL;

public interface IValidatable<T>
{
    void Validate(T entity);
}

public class BookValidator : IValidatable<Book>
{
    public void Validate(Book book)
    {
        if (string.IsNullOrWhiteSpace(book.Name))
        {
            throw new ArgumentException("Название книги не может быть пустым.", nameof(book.Name));
        }

        if (string.IsNullOrWhiteSpace(book.ISBN))
        {
            throw new ArgumentException("ISBN не может быть пустым.", nameof(book.ISBN));
        }

        if (string.IsNullOrWhiteSpace(book.Publisher))
        {
            throw new ArgumentException("Издатель не может быть пустым.", nameof(book.Publisher));
        }

        if (book.Avtors.Count == 0) 
        {
            throw new ArgumentException("Список авторов не может быть пустым.", nameof(book.Avtors));
        }
    }
}

public class AvtorValidator : IValidatable<Avtor>
{
    public void Validate(Avtor avtor)
    {
        {
            if (string.IsNullOrWhiteSpace(avtor.Firstname))
            {
                throw new ArgumentException("Firstname не может быть пустым.", nameof(avtor.Firstname));
            }

            if (string.IsNullOrWhiteSpace(avtor.Lastname))
            {
                throw new ArgumentException("Lastname не может быть пустым.", nameof(avtor.Lastname));
            }

            if (avtor.Firstname.Length > 50)
            {
                throw new ArgumentException("Firstname не может превышать 50 символов.", nameof(avtor.Firstname));
            }

            if (avtor.Lastname.Length > 50)
            {
                throw new ArgumentException("Lastname не может превышать 50 символов.", nameof(avtor.Lastname));
            }

            if (avtor.Patronymic != null && avtor.Patronymic.Length > 50)
            {
                throw new ArgumentException("Patronymic не может превышать 50 символов.", nameof(avtor.Patronymic));
            }
        }
    }

    public class PolzakValidator : IValidatable<Polzak>
    {
        public void Validate(Polzak polzak)
        {
            if (string.IsNullOrWhiteSpace(polzak.Username) || polzak.Username.Length > 50)
            {
                throw new ArgumentException("Имя обязательно и не должно превышать 50 символов.");
            }
            
            if (!string.IsNullOrWhiteSpace(polzak.Password))
            {
                if (polzak.Password.Length > 20)
                {
                    throw new ArgumentException("Пароль пользователя не может быть больше 20 символов.",
                        nameof(polzak.Password));
                }

                if (polzak.Password.Length < 6) 
                {
                    throw new ArgumentException("Пароль пользователя должен содержать не менее 6 символов.",
                        nameof(polzak.Password));
                }
            }
            else
            {
                throw new ArgumentException("Пароль не может быть пустым.", nameof(polzak.Password));
            }
            
            if (Regex.IsMatch(polzak.Password, @"^\D+$"))
            {
                throw new ArgumentException("Пароль не может состоять только из цифр.", nameof(polzak.Password));
            }
        }
    }
}    