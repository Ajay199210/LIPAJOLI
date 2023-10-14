namespace Bibliotheque.MVC.Interfaces
{
    public interface IGenerateurCode
    {
        Task<string> GenererCode(string categorie);
    }
}
